export const arrayBufferToBase64 = (buffer: ArrayBuffer): string => {
  let binary = ''
  const bytes = new Uint8Array(buffer)
  const len = bytes.byteLength
  for (let i = 0; i < len; i++) {
    binary += String.fromCharCode(bytes[i])
  }
  return window.btoa(binary)
}

export const base64ToArrayBuffer = (base64: string): ArrayBuffer => {
  const binaryString = window.atob(base64)
  const len = binaryString.length
  const bytes = new Uint8Array(len)
  for (let i = 0; i < len; i++) {
    bytes[i] = binaryString.charCodeAt(i)
  }
  return bytes.buffer
}

export const uint8ArrayToBase64 = (u8Array: Uint8Array): string => {
  let binary = ''
  const len = u8Array.byteLength
  for (let i = 0; i < len; i++) {
    binary += String.fromCharCode(u8Array[i])
  }
  return window.btoa(binary)
}

export const base64ToUint8Array = (base64: string): Uint8Array => {
  const binaryString = window.atob(base64)
  const len = binaryString.length
  const u8Array = new Uint8Array(len)
  for (let i = 0; i < len; i++) {
    u8Array[i] = binaryString.charCodeAt(i)
  }
  return u8Array
}
