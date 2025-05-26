<template>
  <div
    class="flex flex-col justify-center items-center gap-4"
    :class="isLoading ? 'cursor-progress' : ''"
  >
    <h2 class="text-slate-50 text-2xl">Enter or paste your text below</h2>
    <textarea class="w-1/2 h-1/2" name="createTextArea" id="createTextArea" v-model="textToShare" />
    <div class="flex items-center ps-4 border border-slate-700 rounded-lg p-4 space-x-2">
      <input
        type="checkbox"
        id="burnAfterRead"
        name="burnAfterRead"
        v-model="burnAfterRead"
        class="cursor-pointer"
      />
      <label class="cursor-pointer" for="burnAfterRead">Delete the text after first read</label>
    </div>
    <div class="flex flex-row items-center ps-4 border border-slate-700 rounded-lg p-4 space-x-2">
      <label class="cursor-pointer" for="expiresAtDate">Expires at</label>
      <!-- <input
        id="expiresAtDate"
        type="date"
        v-model="expiresAtDate"
        @change="checkIfExpiresAtIsValid"
      /> -->
      <VueDatePicker
        id="expiresAtDate"
        :min-date="new Date()"
        is-24
        time-picker-inline
        hide-input-icon
        v-model="expiresAtDate"
      >
        <template #trigger>
          <input
            id="expiresAtDate"
            type="text"
            :value="expiresAtDate?.toISOString().slice(0, 16).replace('T', ' ') || ''"
          />
        </template>
      </VueDatePicker>
      <button
        class="p-2 rounded-lg"
        :class="[
          expiresAtDate
            ? 'bg-red-500 text-slate-200 hover:bg-red-400 hover:text-slate-100 cursor-pointer active:scale-80 transition-all duration-200 ease-in-out border-2 border-slate-700'
            : 'bg-red-500/20 text-slate-200/20 cursor-not-allowed border-2 border-slate-700/0',
        ]"
        @click="expiresAtDate = null"
      >
        Reset
      </button>
    </div>

    <button
      class="bg-slate-800 text-sky-600 hover:text-sky-500 hover:bg-slate-700 border-2 border-slate-700 hover:border-slate-800 px-5 py-2 rounded-lg cursor-pointer active:scale-80 transition-all duration-200 ease-in-out"
      type="submit"
      @click="processText"
      :disabled="textToShare.trim().length <= 0"
    >
      Create
    </button>
    <div
      v-if="shareableLink"
      class="p-4 bg-slate-700 rounded-md w-1/2 flex flex-row justify-between items-start"
    >
      <div class="text-clip overflow-auto wrap-anywhere max-h-52">
        {{ shareableLink }}
      </div>
      <button
        class="bg-slate-800 text-sky-600 hover:text-sky-500 hover:bg-slate-700 border-2 border-slate-700 hover:border-slate-800 px-5 py-2 rounded-lg cursor-pointer active:scale-80 transition-all duration-200 ease-in-out"
        type="button"
        @click="copyShareableLinkToClipboard"
      >
        Copy
      </button>
    </div>
    <div v-if="errorText" class="p-4 bg-red-500 font-bold rounded-md w-1/2">{{ errorText }}</div>
  </div>
</template>

<script setup lang="ts">
import { arrayBufferToBase64, uint8ArrayToBase64 } from '@/utils/toStringHelpers'
import { ref, watch } from 'vue'
import VueDatePicker from '@vuepic/vue-datepicker'

const textToShare = ref<string>('')
const burnAfterRead = ref<boolean>(false)
const expiresAtDate = ref<Date | null>(null)

const errorText = ref<string | null>(null)
const userKeyOutput = ref<string | null>(null)
const serverPayload = ref<{ ciphertext: string; iv: string } | null>(null)
const shareableLink = ref<string | null>(null)
const isLoading = ref<boolean>(false)

watch(
  () => [burnAfterRead.value, expiresAtDate.value],
  () => {
    if (burnAfterRead.value && expiresAtDate.value) {
      errorText.value =
        'You can only select one option: "Delete text after first read" or "Expires at".'
    } else {
      errorText.value = null
    }
  },
)

const checkIfExpiresAtIsValid = () => {
  console.log('checkIfExpiresAtIsValid', expiresAtDate.value)
  const today = new Date()
  if (!expiresAtDate.value) {
    errorText.value = null
    return
  }
  if (expiresAtDate.value < today) {
    errorText.value = 'The selected date is in the past. Please select a future date.'
  } else {
    errorText.value = null
  }
}

const copyShareableLinkToClipboard = async () => {
  if (shareableLink.value) {
    try {
      await navigator.clipboard.writeText(shareableLink.value)
      console.log('Shareable link copied to clipboard:', shareableLink.value)
    } catch (err) {
      console.error('Failed to copy shareable link:', err)
    }
  }
}

const processText = async () => {
  isLoading.value = true
  if (!textToShare.value.trim()) {
    userKeyOutput.value = null
    serverPayload.value = null
    return
  }

  try {
    const crypto = window.crypto
    const subtleCrypto = window.crypto.subtle
    const text = textToShare.value
    const encoder = new TextEncoder()
    const dataToEncrypt = encoder.encode(text)

    // 1. Generate IV
    const iv = crypto.getRandomValues(new Uint8Array(24)) // AES-GCM recommended IV size is 12 bytes

    // 2. Generate Key
    const generatedKey = await subtleCrypto.generateKey(
      {
        name: 'AES-GCM',
        length: 256,
      },
      true, // exportable
      ['encrypt', 'decrypt'],
    )

    // 3. Encrypt the data
    const encryptedArrayBuffer = await subtleCrypto.encrypt(
      {
        name: 'AES-GCM',
        iv: iv,
      },
      generatedKey,
      dataToEncrypt,
    )

    // 4. Export the key for the user
    const exportedKeyJwk = await subtleCrypto.exportKey('jwk', generatedKey)
    if (!exportedKeyJwk.k) {
      throw new Error('Failed to export key')
    }
    userKeyOutput.value = exportedKeyJwk.k

    // 5. Prepare data for the server
    const encryptedTextBase64 = arrayBufferToBase64(encryptedArrayBuffer)
    const ivBase64 = uint8ArrayToBase64(iv) // Convert IV to Base64

    serverPayload.value = {
      ciphertext: encryptedTextBase64,
      iv: ivBase64, // Include IV with the ciphertext
    }
    shareableLink.value = 'https://example.com/share/' + encryptedTextBase64

    // For demonstration, you might log it:
    console.log('User Key (JWK):', userKeyOutput.value)
    console.log('Server Payload:', serverPayload.value)
  } catch (error) {
    console.error('Encryption failed:', error)
    errorText.value =
      'Error during encryption. Try again or check console and create an issue on GitHub.'
    userKeyOutput.value = null
    serverPayload.value = null
  } finally {
    isLoading.value = false
  }
}
</script>

<style scoped></style>
