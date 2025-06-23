<template>
  <div
    class="flex flex-col justify-center items-center gap-4"
    :class="isLoading ? 'cursor-progress' : ''"
  >
    <h2 class="text-slate-50 text-2xl">Enter or paste your text below</h2>
    <div class="text-slate-50 text-sm">
      (Snippets/texts are deleted after {{ INACTIVITY_RETENTION_DAYS }} days of inactivity)
    </div>
    <textarea class="w-1/2 h-1/2" name="createTextArea" id="createTextArea" v-model="textToShare" />
    <div class="flex items-center ps-4 border border-slate-700 rounded-lg p-4 space-x-2">
      <input
        type="checkbox"
        id="burnAfterRead"
        name="burnAfterRead"
        v-model="burnAfterRead"
        class="cursor-pointer"
      />
      <label class="cursor-pointer" for="burnAfterRead">Delete the snippet after first read</label>
    </div>
    <div class="flex flex-row items-center ps-4 border border-slate-700 rounded-lg p-4 space-x-2">
      <label class="cursor-pointer min-w-fit" for="expiresAtDate">Expires at</label>
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
      class="border-2 px-5 py-2 rounded-lg"
      :class="[
        canCreateSnippet
          ? 'bg-slate-800 text-sky-600 border-slate-700 hover:text-sky-500 hover:bg-slate-700 hover:border-slate-800 cursor-pointer active:scale-80 transition-all duration-200 ease-in-out'
          : ' bg-slate-800/50 text-sky-600/50 border-slate-700/50 cursor-not-allowed',
      ]"
      type="submit"
      @click="processSnippet"
      :disabled="!canCreateSnippet"
    >
      Create
    </button>
    <div v-if="shareableLink" class="flex flex-col w-1/2 space-y-2 text-center">
      Share the url below with anyone to read your snippet.
      <div class="p-4 bg-slate-700 rounded-md flex flex-row justify-between items-start">
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
    </div>
    <div v-if="errorText" class="p-4 bg-red-500 font-bold rounded-md w-1/2">{{ errorText }}</div>
    <div class="text-sm text-slate-400">
      Original: {{ currentTextSize.formatted }} | Encrypted size:
      {{ estimatedEncryptedSize.formatted }} / {{ MAX_SIZE_KB }} KB
    </div>
  </div>
</template>

<script setup lang="ts">
import { arrayBufferToBase64, uint8ArrayToBase64 } from '@/utils/toStringHelpers'
import { computed, ref, watch } from 'vue'
import VueDatePicker from '@vuepic/vue-datepicker'
import { snippetsApi } from '@/api/requests'
import type { CreateSnippetRequest } from '@/types/requestTypes'

const INACTIVITY_RETENTION_DAYS = import.meta.env.VITE_INACTIVITY_RETENTION_DAYS

const textToShare = ref<string>('')
const burnAfterRead = ref<boolean>(false)
const expiresAtDate = ref<Date | null>(null)
const encryptedTextBase64 = ref<string>('')

const errorText = ref<string | null>(null)
const shareableLink = ref<string | null>(null)
const isLoading = ref<boolean>(false)

const MAX_SIZE_KB = 64

watch(
  () => [burnAfterRead.value, expiresAtDate.value],
  () => {
    if (burnAfterRead.value && expiresAtDate.value) {
      errorText.value =
        'You can only select one option: "Delete snippet after first read" or "Expires at".'
    } else {
      errorText.value = null
    }
  },
)

const canCreateSnippet = computed(() => {
  checkIfExpiresAtIsValid()
  return textToShare.value.trim().length > 0 && !errorText.value
})

const checkIfExpiresAtIsValid = () => {
  const currentTime = new Date().getTime()
  const expiresAtTime = expiresAtDate.value?.getTime() || 0
  if (!expiresAtDate.value) {
    errorText.value = null
    return
  }
  if (expiresAtTime - currentTime < 60000) {
    errorText.value = 'The selected date must be at least 1 minute in the future.'
  } else if (expiresAtTime < currentTime) {
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

const processSnippet = async () => {
  isLoading.value = true
  if (!textToShare.value.trim()) {
    return
  }
  try {
    const crypto = window.crypto
    const subtleCrypto = window.crypto.subtle
    const text = textToShare.value
    const encoder = new TextEncoder()
    const dataToEncrypt = encoder.encode(text)

    const iv = crypto.getRandomValues(new Uint8Array(24))

    const generatedKey = await subtleCrypto.generateKey(
      {
        name: 'AES-GCM',
        length: 256,
      },
      true,
      ['encrypt', 'decrypt'],
    )

    const encryptedArrayBuffer = await subtleCrypto.encrypt(
      {
        name: 'AES-GCM',
        iv: iv,
      },
      generatedKey,
      dataToEncrypt,
    )

    const exportedKeyJwk = await subtleCrypto.exportKey('jwk', generatedKey)
    if (!exportedKeyJwk.k) {
      throw new Error('Failed to export key')
    }
    const key = exportedKeyJwk.k

    encryptedTextBase64.value = arrayBufferToBase64(encryptedArrayBuffer)
    const ivBase64 = uint8ArrayToBase64(iv)

    const createObject: CreateSnippetRequest = {
      content: encryptedTextBase64.value,
      iv: ivBase64,
      burnAfterRead: burnAfterRead.value,
    }
    if (expiresAtDate.value) {
      createObject.expiresAt = expiresAtDate.value
    }

    const snippetId = await snippetsApi.create(createObject)

    shareableLink.value = `${window.location.origin}/view/${snippetId}#${key}`

    textToShare.value = ''
    errorText.value = null
  } catch (error) {
    console.error('Encryption failed:', error)
    errorText.value =
      'Error during encryption. Try again or check console and create an issue on GitHub.'
  } finally {
    isLoading.value = false
  }
}

const getTextSizeInBytes = (text: string): number => {
  return new TextEncoder().encode(text).length
}

const currentTextSize = computed(() => {
  const sizeInBytes = getTextSizeInBytes(textToShare.value)
  const sizeInKB = sizeInBytes / 1024
  return {
    bytes: sizeInBytes,
    kb: sizeInKB,
    formatted: sizeInKB < 1 ? `${sizeInBytes} bytes` : `${sizeInKB.toFixed(1)} KB`,
  }
})

const estimatedEncryptedSize = computed(() => {
  if (!textToShare.value) return { formatted: '0 bytes' }

  const estimatedEncrypted = getTextSizeInBytes(encryptedTextBase64.value)
  const estimatedKB = estimatedEncrypted / 1024

  return {
    bytes: estimatedEncrypted,
    formatted: estimatedKB < 1 ? `${estimatedEncrypted} bytes` : `${estimatedKB.toFixed(1)} KB`,
  }
})
</script>
