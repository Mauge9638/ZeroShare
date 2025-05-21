<template>
  <div>The view page</div>
  <div>{{ urlFragmentHash }}</div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { ref, onMounted } from 'vue'

const urlFragmentHash = ref<string | null>(null)

onMounted(() => {
  const urlFragment = window.location.hash
  if (urlFragment) {
    const fragment = urlFragment.substring(1) // Remove the '#' character
    urlFragmentHash.value = decodeURIComponent(fragment)
  }
})
</script>

<style scoped></style>
