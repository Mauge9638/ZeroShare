# ZeroShare ✨

**ZeroShare** is a live, open-source encrypted snippet sharer that prioritizes privacy and security. Share text snippets, code, configurations, or sensitive notes with zero-knowledge encryption - even we can't read your data!

ZeroShare offers a **core open-source platform** that you can self-host, and **ZeroShare Cloud ([zeroshare.app](https://zeroshare.app))**, the official managed service offering additional convenience and premium features.

🌐 **Experience ZeroShare Cloud: [zeroshare.app](https://zeroshare.app)**

[![Live on zeroshare.app](https://img.shields.io/badge/status-live%20on%20zeroshare.app-brightgreen?style=for-the-badge)](https://zeroshare.app)
[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=for-the-badge)](https://opensource.org/licenses/Apache-2.0)
[![Open Source](https://img.shields.io/badge/open%20source-yes-blue?style=for-the-badge)](https://github.com/Mauge9638/ZeroShare)

## Table of Contents

- [ZeroShare ✨](#zeroshare-)
  - [Table of Contents](#table-of-contents)
  - [🌟 Why ZeroShare?](#-why-zeroshare)
  - [✅ Core Features (Live on zeroshare.app \& in OSS)](#-core-features-live-on-zeroshareapp--in-oss)
  - [🔒 Zero-Knowledge Encryption: The Foundation](#-zero-knowledge-encryption-the-foundation)
  - [🚀 Roadmap: Evolving ZeroShare](#-roadmap-evolving-zeroshare)
    - [Core Open-Source Enhancements](#core-open-source-enhancements)
    - [ZeroShare Cloud: Premium Services \& Features (Planned for zeroshare.app)](#zeroshare-cloud-premium-services--features-planned-for-zeroshareapp)
  - [🛠️ Tech Stack](#️-tech-stack)
  - [🏠 Self-Hosting the Open-Source Version](#-self-hosting-the-open-source-version)
  - [🤝 Contributing to Open Source](#-contributing-to-open-source)
  - [📄 License](#-license)

## 🌟 Why ZeroShare?

ZeroShare provides a secure and private alternative to public pastebins:

- **🔒 True Privacy:** Zero-knowledge encryption means your data is encrypted _in your browser_ before it ever reaches the servers. This applies to both the OSS version and ZeroShare Cloud.
- **🚀 No Friction (Core):** For basic sharing, no accounts are needed on [zeroshare.app](https://zeroshare.app)'s free tier or in the self-hosted version.
- **🏠 Your Control with Open Source:** The core platform is open-source (Apache 2.0 License) and designed for self-hosting if you prefer full control.
- **🛡️ Security First:** Even on ZeroShare Cloud, we cannot read your snippets. The decryption key never leaves your browser.
- **☁️ Convenience with ZeroShare Cloud:** [zeroshare.app](https://zeroshare.app) offers a managed, hassle-free experience with plans for additional features and capabilities.
- **✨ Community & Transparency:** The open-source nature allows anyone to inspect, verify, and contribute to the core technology.

## ✅ Core Features (Live on [zeroshare.app](https://zeroshare.app) & in OSS)

The foundational ZeroShare experience is live and fully functional at **[zeroshare.app](https://zeroshare.app)** and forms the basis of the open-source version:

- ✅ **Snippet Creation:** Simple web interface to paste and share text.
- ✅ **Zero-Knowledge Encryption:** Client-side encryption with AES-GCM.
- ✅ **Unique Link Generation:** Automatic creation of shareable links.
- ✅ **Secure Viewing:** Decrypt and view snippets via unique links.
- ✅ **Auto-Expiration:** Snippets auto-delete after a set duration.
- ✅ **Burn After Reading:** One-time view option for maximum security.
- ✅ **Responsive Design:** Works seamlessly on desktop and mobile.

## 🔒 Zero-Knowledge Encryption: The Foundation

The core security model ensures maximum privacy for all users:

1. **Encryption happens in your browser** using `window.crypto.subtle` (AES-GCM).
2. A **random encryption key** is generated locally and is _**never**_ sent to the servers.
3. The **decryption key travels in the URL fragment** (e.g., `zeroshare.app/view/{id}#DECRYPTION_KEY`), which browsers do not send to servers.
4. The **server stores only encrypted data** – we literally cannot read your snippets.
5. **Decryption happens locally in the viewer's browser** when they open the link.

## 🚀 Roadmap: Evolving ZeroShare

ZeroShare is continuously evolving! We are committed to enhancing the core open-source product and building out **ZeroShare Cloud** ([zeroshare.app](https://zeroshare.app)) with premium features and services.

### Core Open-Source Enhancements

_(These features will be part of the self-hostable OSS version and also available on ZeroShare Cloud's free/core offering. As the project is open source, **any feature implemented in the public repository will be available to self-hosters.**)_

- [ ] **Easy Self-Hosting:** Docker Compose setup with minimal configuration and comprehensive guides.
- [ ] **Syntax Highlighting:** For improved readability of code snippets.
- [ ] **User-Defined Encryption Keys:** Option to use custom passwords instead of randomly generated keys.
- [ ] **Basic API Access:** For individual users to create snippets programmatically (rate-limited).
- [ ] **Basic CLI Tool:** For quick snippet sharing from the command line.
- [ ] **Internationalization (i18n) & Localization (L10n):** Support for multiple languages.
- [ ] **Accessibility Improvements (a11y):** Ensuring the platform is usable by everyone.

### ZeroShare Cloud: Premium Services & Features (Planned for [zeroshare.app](https://zeroshare.app))

**ZeroShare Cloud ([zeroshare.app](https://zeroshare.app)) is the official managed service.** It offers the convenience of a ready-to-use platform with additional capabilities designed for individuals and teams needing more power, scale, and managed services. Subscriptions to ZeroShare Cloud will help fund the continued development and maintenance of the open-source core.

**The Open Core Philosophy:**

- **The open-source ZeroShare repository ([GitHub](https://github.com/Mauge9638/ZeroShare)) contains the full application code.** If a feature is developed and committed to this public repository, it will be available to anyone who clones, forks, or self-hosts the application.
- **Premium on ZeroShare Cloud** refers to:
  - **Managed Hosting & Convenience:** No need to set up or maintain servers, databases, or updates.
  - **Guaranteed Uptime & Scalability:** We handle the infrastructure to ensure ZeroShare Cloud is reliable and performant.
  - **Higher Resource Limits:** Generous allowances for snippet size, storage, API calls, etc., beyond typical self-hosted defaults or the free tier.
  - **Consolidated Team Management Features:** Streamlined interfaces and tools for team administration and billing.
  - **Dedicated Support:** Priority assistance for subscribers.
  - **Potentially, integrations or add-on services** that are separate from the core application code (e.g., advanced analytics dashboards if relevant and privacy-preserving).

**Planned Premium Capabilities on ZeroShare Cloud:**

**For Individuals (Pro Plans):**

- [ ] **User Accounts:** To manage snippets, preferences, and subscriptions on ZeroShare Cloud.
- [ ] **Increased Resource Limits on Cloud:** Larger max snippet size, more active snippets, longer/custom default expiration times.
- [ ] **Advanced Expiration Options on Cloud:** "Burn after X views" (e.g., 2, 5, 10 views).
- [ ] **Custom Snippet IDs / Vanity URLs on Cloud:** Create more memorable and branded links.
- [ ] **Enhanced Snippet Management Dashboard on Cloud:** Better organization and overview of your created snippets (metadata only).
- [ ] **Priority Support (Individual).**

**For Teams & Businesses (Workspace Plans):**

- [ ] **Team Workspaces on Cloud:** Centralized environment for team collaboration.
- [ ] **Secure Team Snippet Sharing & Drives on Cloud:** Mechanisms for teams to securely share and manage collections of snippets (still leveraging client-side encryption).
- [ ] **Role-Based Access Control (RBAC) on Cloud:** Define roles (e.g., admin, editor, viewer) for team members.
- [ ] **Team Member Management & Invitations on Cloud.**
- [ ] **Audit Logs (Metadata Only) on Cloud:** Track snippet creation, sharing, and access _within the team's workspace context_. Content remains zero-knowledge.
- [ ] **Encrypted File Sharing on Cloud:** Extend zero-knowledge principles to securely share small files (encrypted client-side).
  - _Cloud Aspects: Managed file size limits, team storage quotas, optimized delivery._
- [ ] **Advanced API Access & Webhooks on Cloud:** Higher rate limits, team-specific API endpoints, and notifications for team events.
- [ ] **Centralized Billing & Invoicing.**
- [ ] **Priority Team Support.**
- [ ] **(Future) Custom Branding Options for Workspaces on Cloud.**

We believe in a strong open-source core, with ZeroShare Cloud offering sustainable funding for the project's continued development and providing valuable, privacy-preserving services for those who need them.

## 🛠️ Tech Stack

- **Backend:** C# with ASP.NET Core
- **Frontend:** Vue 3 with TypeScript and Tailwind CSS
- **Database (for ZeroShare Cloud & default for OSS):** PostgreSQL (Neon for ZeroShare Cloud)
- **Deployment (OSS Future):** Docker & Docker Compose

## 🏠 Self-Hosting the Open-Source Version

**Self-hosting configuration is currently in development.** The goal is to make it easy for anyone to run their own instance of ZeroShare, with access to all features present in the open-source codebase.

The ZeroShare open-source repository ([GitHub](https://github.com/Mauge9638/ZeroShare)) contains the full application. By self-hosting, you get:

- **Full Control:** Over your data, configuration, and features.
- **All Core Features:** Access to every feature implemented in the public codebase.
- **No Service Fees:** You only manage your own infrastructure costs.

While ZeroShare is open-source under the Apache 2.0 license, a polished, easy-to-use self-hosting setup (e.g., with Docker Compose) is on the roadmap.

1. Watch this GitHub repository for updates.
2. Check the "Issues" tab for discussions and progress related to self-hosting.
3. Star the repo to show your interest!

```bash
git clone https://github.com/Mauge9638/ZeroShare.git
cd ZeroShare
# Stay tuned for detailed self-hosting instructions!
```

## 🤝 Contributing to Open Source

We welcome contributions to the open-source core of ZeroShare! Here's how you can help:

- **🐛 Report Bugs:** Found an issue on [zeroshare.app](https://zeroshare.app) or while trying to set up the code? Open an issue on GitHub.
- **💡 Suggest Features:** Have ideas for the core product or for ZeroShare Cloud? Share them by opening an issue.
- **📝 Documentation:** Help improve the README, guides, and comments.
- **🔧 Code Contributions:** Submit pull requests for bug fixes and features for the open-source components.

## 📄 License

This project is licensed under the Apache 2.0 License - see the [LICENSE](LICENSE) file for details.

---

**Ready to share securely? Try ZeroShare at [zeroshare.app](https://zeroshare.app) 🚀**
