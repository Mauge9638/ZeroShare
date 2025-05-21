# ZeroShare ✨ (Under Development)

**ZeroShare** is an upcoming self-hostable, open-source encrypted snippet sharer. The goal is to enable users to quickly share text snippets, code, configurations, or sensitive notes with a strong emphasis on privacy and control. It's being built with a C#/.NET backend and a Vue3/TypeScript frontend.

[![Work in Progress](https://img.shields.io/badge/status-work%20in%20progress-yellow?style=for-the-badge)](https://github.com/YOUR_USERNAME/ZeroShare)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)

<!-- Add your GitHub repo link for the WIP badge if you wish -->

## ⚠️ Current Status

**This project is in its early stages of development.** Many features described below are planned and not yet implemented. The primary focus currently is on laying the foundational architecture. There are no stable releases or complete build/run instructions at this time.

## Table of Contents

- [ZeroShare ✨ (Under Development)](#zeroshare--under-development)
  - [⚠️ Current Status](#️-current-status)
  - [Table of Contents](#table-of-contents)
  - [🌟 Vision: Why ZeroShare?](#-vision-why-zeroshare)
  - [🎯 Planned Features](#-planned-features)
    - [Core Functionality (MVP - Minimum Viable Product Goals)](#core-functionality-mvp---minimum-viable-product-goals)
    - [Key Differentiator: Encryption](#key-differentiator-encryption)
    - [Potential Future Enhancements](#potential-future-enhancements)
  - [🛠️ Intended Tech Stack](#️-intended-tech-stack)
  - [🌱 Following Progress \& Getting Involved](#-following-progress--getting-involved)
  - [🤝 Contributing Ideas](#-contributing-ideas)
  - [📄 License](#-license)

## 🌟 Vision: Why ZeroShare?

The aim for ZeroShare is to provide a secure and private alternative to public pastebins, focusing on:

- **🔒 Privacy & Control:**
  - **Zero-Knowledge Encryption (Client-Side):** The core design principle. Snippets will be encrypted _in the user's browser_ before being sent to the server. The server will only store encrypted data, and the decryption key will remain with the user (via the URL fragment), ensuring that even the server administrator cannot read the snippets.
  - **Self-Hosting:** Users will be able to host ZeroShare on their own infrastructure, keeping their data entirely within their control.
  - **No Account Needed (by default):** For quick, frictionless sharing.
- **✨ Customization & Openness:**
  - As an open-source project, the community will be able to inspect, modify, and extend ZeroShare.
- **🏢 Addressing a Need:** Provide a trustworthy tool for developers, teams, and privacy-conscious individuals.

## 🎯 Planned Features

### Core Functionality (MVP - Minimum Viable Product Goals)

- **Snippet Creation:** A simple web interface to paste text.
- **Unique Link Generation:** Automatic creation of a shareable link for each snippet.
- **Snippet Viewing:** Displaying the snippet content via the unique link.
- **Expiration:** Allowing snippets to auto-delete after a set duration.

### Key Differentiator: Encryption

- **Client-Side Encryption (Zero-Knowledge):** This is foundational.
  1.  Text is encrypted in the browser using `window.crypto.subtle` (e.g., AES-GCM).
  2.  A randomly generated encryption key is combined with the snippet's unique ID in the shareable link's fragment (e.g., `your.zeroshare.app/view/{id}#DECRYPTION_KEY`).
  3.  The server receives and stores only the _encrypted_ text. It never sees the decryption key.
  4.  When viewing, the browser fetches the encrypted text, and JavaScript uses the key from the URL fragment to decrypt and display the content locally.

### Potential Future Enhancements

- Syntax Highlighting
- "Burn After Reading" / One-Time View
- API for programmatic access
- Password protection (as an additional layer)

## 🛠️ Intended Tech Stack

- **Backend:** C# with ASP.NET Core
- **Frontend:** Vue 3 with TypeScript
- **Database:** PostgreSQL
- **Deployment (Future):** Docker & Docker Compose

## 🌱 Following Progress & Getting Involved

This project is currently under active development. You can:

1.  **Clone the repository** to explore the codebase and see its progress:

    ```bash
    git clone https://github.com/Mauge9638/ZeroShare.git
    cd ZeroShare
    ```

    (Note: Build and run instructions are not yet available as the project is not in a runnable state.)

2.  **Watch this repository** on GitHub to get notified of updates.
3.  Check the **Issues** tab for planned work or to report bugs as the project matures.

## 🤝 Contributing Ideas

While full code contributions might be premature, ideas, suggestions, and discussions are welcome! Feel free to:

- Open an **Issue** to discuss potential features or improvements.
- Participate in existing discussions.

As the project stabilizes, more formal contribution guidelines will be provided.

## 📄 License

This project is intended to be licensed under the MIT License.

---
