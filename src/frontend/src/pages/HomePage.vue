<template>
  <div class="grid md:grid-cols-3 sm:grid-cols-1 gap-4 justify-center items-center">
    <RouterLink
      class="rounded-lg max-h-96 overflow-ellipsis text-center transition-all duration-200 ease-in-out hover:scale-101 cursor-pointer col-span-1 w-full h-full bg-slate-800 border-2 border-slate-700 hover:border-slate-500 hover:bg-slate-700 flex flex-col items-center space-y-4 justify-center"
      to="/view"
    >
      <div class="text-5xl text-slate-50">View</div>
      <p>View a created text/snippet</p>
    </RouterLink>
    <RouterLink
      class="rounded-lg max-h-96 overflow-ellipsis text-center transition-all duration-200 ease-in-out hover:scale-101 cursor-pointer col-span-1 w-full h-full bg-slate-800 border-2 border-slate-700 hover:border-slate-500 hover:bg-slate-700 flex flex-col items-center space-y-4 justify-center"
      to="/create"
    >
      <div class="text-5xl text-slate-50">Create</div>
      <p>Create a new private text/snippet you want to share</p>
    </RouterLink>
    <RouterLink
      class="rounded-lg max-h-96 overflow-ellipsis text-center transition-all duration-200 ease-in-out hover:scale-101 cursor-pointer col-span-1 w-full h-full bg-slate-800 border-2 border-slate-700 hover:border-slate-500 hover:bg-slate-700 flex flex-col items-center space-y-4 justify-center"
      to="/about"
    >
      <div class="text-5xl text-slate-50">About</div>
      <p class="">Read what this is about and how your texts/snippets are 100% private</p>
    </RouterLink>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped></style>
