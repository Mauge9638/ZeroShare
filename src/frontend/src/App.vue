<template>
  <main class="flex h-screen w-full flex-col bg-slate-900 text-slate-200">
    <div class="flex flex-row space-x-6 bg-slate-800 p-2 justify-center">
      <RouterLink class="cursor-pointer" to="/">Home</RouterLink>
      <RouterLink class="cursor-pointer" to="/view">View</RouterLink>
      <RouterLink class="cursor-pointer" to="/create">Create</RouterLink>
      <RouterLink class="cursor-pointer" to="/about">About</RouterLink>
    </div>
    <RouterView class="min-w-fit flex-1 overflow-auto p-4" :key="$route.fullPath" />
  </main>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<style scoped></style>
