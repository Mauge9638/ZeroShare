export const arrayBufferToBase64 = (buffer: ArrayBuffer): string => {
  let binary = ''
  const bytes = new Uint8Array(buffer)
  const len = bytes.byteLength
  for (let i = 0; i < len; i++) {
    binary += String.fromCharCode(bytes[i])
  }
  return window.btoa(binary)
}

// Helper function to convert Uint8Array to Base64 string (for IV)
export const uint8ArrayToBase64 = (u8Array: Uint8Array): string => {
  let binary = ''
  const len = u8Array.byteLength
  for (let i = 0; i < len; i++) {
    binary += String.fromCharCode(u8Array[i])
  }
  return window.btoa(binary)
}
