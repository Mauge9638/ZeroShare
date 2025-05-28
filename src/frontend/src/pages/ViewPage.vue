<template>
  <div class="flex flex-col justify-center items-center p-6 space-y-4">
    <div v-if="error" class="p-4 bg-red-500 text-white rounded-md">
      {{ error }}
    </div>

    <template v-if="!isLoading && !decryptedText">
      <div class="flex flex-row items-center space-x-3">
        <label class="w-24" for="contentIdInput">Content id</label>
        <input id="contentIdInput" type="text" v-model="contentId" />
      </div>
      <div class="flex flex-row items-center space-x-3">
        <label class="w-24" for="keyInput">Key</label>
        <input id="keyInput" type="text" v-model="key" />
      </div>
      <button
        class="border-2 px-5 py-2 rounded-lg"
        :class="[
          contentId && key
            ? 'bg-slate-800 text-sky-600 border-slate-700 hover:text-sky-500 hover:bg-slate-700 hover:border-slate-800 cursor-pointer active:scale-80 transition-all duration-200 ease-in-out'
            : ' bg-slate-800/50 text-sky-600/50 border-slate-700/50 cursor-not-allowed',
        ]"
        type="button"
        @click="getSnippetContent"
        :disabled="!contentId || !key"
      >
        Get snippet
      </button>
    </template>
    <div v-if="isLoading" class="p-4 bg-slate-800 text-sky-600 rounded-md">
      Loading and decrypting snippet...
    </div>
    <template v-if="decryptedText">
      <pre class="whitespace-pre-wrap w-1/2 h-1/2 overflow-auto">{{ decryptedText }}</pre>
      <button
        class="bg-slate-800 text-sky-600 hover:text-sky-500 hover:bg-slate-700 border-2 border-slate-700 hover:border-slate-800 px-5 py-2 rounded-lg cursor-pointer active:scale-80 transition-all duration-200 ease-in-out"
        type="button"
        @click="copyDecryptedTextToClipboard"
      >
        Copy
      </button>
    </template>
  </div>
</template>

<script setup lang="ts">
import { snippetsApi } from '@/api/requests'
import { base64ToArrayBuffer, base64ToUint8Array } from '@/utils/toStringHelpers'
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const contentId = ref<string | null>(null)
const key = ref<string | null>(null)
const encryptedText = ref<string | null>(null)
const iv = ref<string | null>(null)
const decryptedText = ref<string | null>(null)
const isLoading = ref<boolean>(false)
const error = ref<string | null>(null)

onMounted(() => {
  contentId.value = route.params.id as string

  const urlFragment = window.location.hash
  if (urlFragment) {
    const fragment = urlFragment.substring(1) // Remove the '#' character
    key.value = decodeURIComponent(fragment)
  }

  if (contentId.value && key.value) {
    getSnippetContent()
  }
})
const getSnippetContent = async () => {
  if (!contentId.value || !key.value) {
    error.value = 'Missing snippet ID or encryption key.'
    return
  }

  isLoading.value = true
  error.value = null

  try {
    const response = await snippetsApi.get(contentId.value)
    encryptedText.value = response.content
    iv.value = response.iv

    // Decrypt using the key from URL fragment
    await decryptSnippet(response.content, response.iv, key.value)
  } catch (err) {
    console.error('Error fetching snippet:', err)
    error.value = 'Failed to fetch snippet. It may have expired or been deleted.'
  } finally {
    isLoading.value = false
  }
}

const decryptSnippet = async (encryptedContent: string, ivBase64: string, keyBase64: string) => {
  try {
    const subtleCrypto = window.crypto.subtle
    const decoder = new TextDecoder()

    // Convert base64 strings back to the required formats
    const encryptedArrayBuffer = base64ToArrayBuffer(encryptedContent)
    const ivUint8Array = base64ToUint8Array(ivBase64)

    // Reconstruct the key from the base64 string
    const keyJwk = {
      kty: 'oct',
      k: keyBase64,
      alg: 'A256GCM',
      use: 'enc',
    }

    // Import the key
    const cryptoKey = await subtleCrypto.importKey(
      'jwk',
      keyJwk,
      {
        name: 'AES-GCM',
        length: 256,
      },
      false,
      ['decrypt'],
    )

    // Decrypt the data
    const decryptedArrayBuffer = await subtleCrypto.decrypt(
      {
        name: 'AES-GCM',
        iv: ivUint8Array,
      },
      cryptoKey,
      encryptedArrayBuffer,
    )

    // Convert decrypted data back to text
    decryptedText.value = decoder.decode(decryptedArrayBuffer)
  } catch (err) {
    console.error('Decryption failed:', err)
    error.value = 'Failed to decrypt snippet. The link may be corrupted or invalid.'
  }
}

const copyDecryptedTextToClipboard = async () => {
  if (decryptedText.value) {
    try {
      await navigator.clipboard.writeText(decryptedText.value)
      console.log('Decrypted text copied to clipboard:', decryptedText.value)
    } catch (err) {
      console.error('Failed to copy shareable link:', err)
    }
  }
}
</script>

<style scoped></style>
