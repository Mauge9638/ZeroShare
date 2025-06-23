export interface CreateSnippetRequest {
  content: string
  iv: string
  burnAfterRead?: boolean
  expiresAt?: Date
}
