<script setup>
import { useHead } from '@vueuse/head'
import Navbar from './components/global/Navbar.vue'
    useHead({
      title: "Bibliotkt",
      link: [
        {
          rel: "stylesheet",
          href: "https://rsms.me/inter/inter.css"
        }
      ]
    });
</script>

<template>
  <div class="bg-blue-400">
    <Navbar />
    <router-view />
  </div>
</template>