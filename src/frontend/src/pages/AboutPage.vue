<template>
  <div class="text-slate-300 p-2">
    <h1 class="text-3xl font-bold text-slate-50 pb-6">About ZeroShare</h1>

    <p class="pt-4">
      ZeroShare is a self-hostable, open-source encrypted snippet sharer, currently under
      development. The goal is to enable users to quickly share text snippets, code, configurations,
      or sensitive notes with a strong emphasis on privacy and control.
    </p>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">The Vision</h2>
    <p class="pt-2">
      The aim for ZeroShare is to provide a secure and private alternative to public pastebins,
      focusing on:
    </p>
    <ul class="list-disc list-inside pl-4 pt-2 space-y-1">
      <li>
        <strong>Privacy & Control:</strong> Through zero-knowledge, client-side encryption and the
        ability to self-host.
      </li>
      <li><strong>No Account Needed (by default):</strong> For quick, frictionless sharing.</li>
      <li>
        <strong>Customization & Openness:</strong> As an open-source project, the community can
        inspect, modify, and extend ZeroShare.
      </li>
    </ul>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">
      Key Feature: Zero-Knowledge Encryption
    </h2>
    <p class="pt-2">This is a foundational principle of ZeroShare. Here's how it works:</p>
    <ol class="list-decimal list-inside pl-4 pt-2 space-y-1">
      <li>Text is encrypted in your browser using the Web Crypto API (AES-GCM).</li>
      <li>
        A randomly generated encryption key is part of the shareable link's fragment (the part after
        the '#').
      </li>
      <li>
        The server receives and stores only the <strong>encrypted</strong> text. It never sees the
        decryption key.
      </li>
      <li>
        When viewing a snippet, your browser fetches the encrypted text, and JavaScript uses the key
        from the URL fragment to decrypt and display the content locally.
      </li>
    </ol>
    <p class="pt-2">
      This ensures that even the server administrator cannot read the content of your shared
      snippets.
    </p>

    <h2 class="text-2xl font-semibold text-slate-50 pt-6 pb-3">Development Status</h2>
    <p class="pt-2">
      Please note: ZeroShare is in its early stages of development. Many features are planned and
      not yet implemented.
    </p>

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
      repository.
    </p>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped></style>
