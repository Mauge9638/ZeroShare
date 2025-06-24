import type {
  EncryptedTextWithKey,
  EncryptedTextWithKeyAndPassword,
  EncryptedTextWithPassword,
} from '@/types/cryptoTypes'
import { arrayBufferToBase64, uint8ArrayToBase64 } from '@/utils/toStringHelpers'

export const encryptTextWithRandomKey = async (text: string): Promise<EncryptedTextWithKey> => {
  const crypto = window.crypto
  const subtleCrypto = window.crypto.subtle
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

  const encryptedTextBase64 = arrayBufferToBase64(encryptedArrayBuffer)
  const ivBase64 = uint8ArrayToBase64(iv)

  const createObject = {
    content: encryptedTextBase64,
    iv: ivBase64,
    key: key,
  }

  return createObject
}

export const encryptTextWithPassword = async (
  text: string,
  password: string,
  iv?: Uint8Array,
): Promise<EncryptedTextWithPassword> => {
  const crypto = window.crypto
  const subtleCrypto = window.crypto.subtle
  const encoder = new TextEncoder()
  const dataToEncrypt = encoder.encode(text)

  const salt = crypto.getRandomValues(new Uint8Array(16))
  const ivToUse = iv ?? crypto.getRandomValues(new Uint8Array(12))

  const passwordKey = await subtleCrypto.importKey(
    'raw',
    encoder.encode(password),
    'PBKDF2',
    false,
    ['deriveKey'],
  )

  const derivedKey = await subtleCrypto.deriveKey(
    {
      name: 'PBKDF2',
      salt: salt,
      iterations: 500000,
      hash: 'SHA-256',
    },
    passwordKey,
    { name: 'AES-GCM', length: 256 },
    false,
    ['encrypt', 'decrypt'],
  )

  const encryptedArrayBuffer = await subtleCrypto.encrypt(
    {
      name: 'AES-GCM',
      iv: ivToUse,
    },
    derivedKey,
    dataToEncrypt,
  )

  return {
    content: arrayBufferToBase64(encryptedArrayBuffer),
    iv: uint8ArrayToBase64(ivToUse),
    salt: uint8ArrayToBase64(salt),
  }
}

export const encryptTextWithKeyAndPassword = async (
  text: string,
  password: string,
): Promise<EncryptedTextWithKeyAndPassword> => {
  const encryptedWithKey = await encryptTextWithRandomKey(text)
  const encryptedWithPassword = await encryptTextWithPassword(encryptedWithKey.content, password)

  return {
    content: encryptedWithPassword.content,
    iv: encryptedWithPassword.iv,
    key: encryptedWithKey.key,
    salt: encryptedWithPassword.salt,
  }
}
