export interface CreateSnippetRequest {
  content: string
  iv: string
  burnAfterRead?: boolean
  expiresAt?: Date
}

interface CreateSnippetApiPayload {
  content: string
  iv: string
  burnAfterRead?: boolean
  expiresAt?: string
}

export const snippetsApi = {
  async create(payload: CreateSnippetRequest): Promise<string> {
    const apiPayload: CreateSnippetApiPayload = {
      ...payload,
      expiresAt: payload.expiresAt?.toISOString(), // Convert Date to UTC string
    }

    const response = await fetch(`api/snippet`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(apiPayload),
    })

    if (!response.ok) {
      throw new Error(`Failed to create snippet: ${response.statusText}`)
    }

    return response.text()
  },

  async get(id: string): Promise<{ content: string; iv: string }> {
    const response = await fetch(`api/snippet/${id}`)

    if (!response.ok) {
      throw new Error(`Failed to fetch snippet: ${response.statusText}`)
    }

    return response.json()
  },
}
