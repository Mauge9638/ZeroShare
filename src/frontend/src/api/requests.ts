import type { CreateSnippetRequest } from '@/types/requestTypes'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL

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
      expiresAt: payload.expiresAt?.toISOString(),
    }

    const response = await fetch(`${API_BASE_URL}/snippet`, {
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
    const response = await fetch(`${API_BASE_URL}/snippet/${id}`)

    if (!response.ok) {
      throw new Error(`Failed to fetch snippet: ${response.statusText}`)
    }

    return response.json()
  },
}
