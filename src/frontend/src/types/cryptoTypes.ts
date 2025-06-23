export interface EncryptedTextWithKey {
  content: string
  iv: string
  key: string
}

export interface EncryptedTextWithPassword {
  content: string
  iv: string
  salt: string
}

export interface EncryptedTextWithKeyAndPassword {
  content: string
  iv: string
  key: string
  salt: string
}
