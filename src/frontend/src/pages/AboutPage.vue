<template>
  <div class="text-slate-300 p-2">
    <h1 class="text-3xl font-bold text-slate-50 pb-6">About ZeroShare ✨</h1>

    <p class="pt-4">
      ZeroShare is a live, open-source encrypted snippet sharer that prioritizes privacy and
      security. Share text snippets, code, configurations, or sensitive notes with zero-knowledge
      encryption - even we can't read your data!
    </p>

    <p class="pt-4">
      ZeroShare offers a <strong>core open-source platform</strong> that you can self-host, and
      <strong>ZeroShare Cloud</strong>, the official managed service offering additional convenience
      and premium features.
    </p>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">Why ZeroShare?</h2>
    <p class="pt-2">ZeroShare provides a secure and private alternative to public pastebins:</p>
    <ul class="list-disc list-inside pl-4 pt-2 space-y-1">
      <li>
        <strong>🔒 True Privacy:</strong> Zero-knowledge encryption means your data is encrypted in
        your browser before it ever reaches the servers.
      </li>
      <li><strong>🚀 No Friction:</strong> For basic sharing, no accounts are needed.</li>
      <li>
        <strong>🏠 Your Control:</strong> The core platform is open-source and designed for
        self-hosting if you prefer full control.
      </li>
      <li>
        <strong>🛡️ Security First:</strong> We cannot read your snippets. The decryption key never
        leaves your browser.
      </li>
      <li>
        <strong>✨ Community & Transparency:</strong> The open-source nature allows anyone to
        inspect, verify, and contribute to the core technology.
      </li>
    </ul>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">
      🔒 Zero-Knowledge Encryption: The Foundation
    </h2>
    <p class="pt-2">The core security model ensures maximum privacy for all users:</p>
    <ol class="list-decimal list-inside pl-4 pt-2 space-y-1">
      <li>
        <strong>Encryption happens in your browser</strong> using
        <code class="bg-slate-700 px-1 rounded">window.crypto.subtle</code> (AES-GCM).
      </li>
      <li>
        A <strong>random encryption key</strong> is generated locally and is never sent to the
        servers.
      </li>
      <li>
        The <strong>decryption key travels in the URL fragment</strong> (the part after the '#'),
        which browsers do not send to servers.
      </li>
      <li>
        The <strong>server stores only encrypted data</strong> – we literally cannot read your
        snippets.
      </li>
      <li>
        <strong>Decryption happens locally in the viewer's browser</strong> when they open the link.
      </li>
    </ol>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">✅ Core Features</h2>
    <p class="pt-2">ZeroShare is live and fully functional with these features:</p>
    <ul class="list-disc list-inside pl-4 pt-2 space-y-1">
      <li><strong>Snippet Creation:</strong> Simple web interface to paste and share text.</li>
      <li><strong>Zero-Knowledge Encryption:</strong> Client-side encryption with AES-GCM.</li>
      <li><strong>Unique Link Generation:</strong> Automatic creation of shareable links.</li>
      <li><strong>Secure Viewing:</strong> Decrypt and view snippets via unique links.</li>
      <li><strong>Auto-Expiration:</strong> Snippets auto-delete after a set duration.</li>
      <li><strong>Burn After Reading:</strong> One-time view option for maximum security.</li>
      <li><strong>Responsive Design:</strong> Works seamlessly on desktop and mobile.</li>
    </ul>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">🚀 What's Coming Next</h2>
    <p class="pt-2">
      ZeroShare is continuously evolving! We're committed to enhancing the core open-source product
      with features like:
    </p>
    <ul class="list-disc list-inside pl-4 pt-2 space-y-1">
      <li>Easy self-hosting with Docker Compose</li>
      <li>Syntax highlighting for code snippets</li>
      <li>User-defined encryption keys</li>
      <li>Basic API access and CLI tool</li>
      <li>Internationalization and accessibility improvements</li>
      <li>Features for teams to easily share internally</li>
    </ul>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">🛠️ Tech Stack</h2>
    <ul class="list-disc list-inside pl-4 pt-2 space-y-1">
      <li><strong>Backend:</strong> C# with ASP.NET Core</li>
      <li><strong>Frontend:</strong> Vue 3 with TypeScript and Tailwind CSS</li>
      <li><strong>Database:</strong> PostgreSQL</li>
      <li><strong>Deployment:</strong> Docker & Docker Compose (planned for self-hosting)</li>
    </ul>

    <p class="pt-6">
      The source code for ZeroShare is available on
      <a
        href="https://github.com/Mauge9638/ZeroShare"
        target="_blank"
        class="text-slate-50 hover:text-slate-50 underline"
        >GitHub</a
      >
      and licensed under the Apache 2.0 License.
    </p>
    <p class="pt-4">
      If you have any questions, ideas, or feedback, please feel free to reach out via the GitHub
      repository or contribute to the open-source project!
    </p>
  </div>
</template>
