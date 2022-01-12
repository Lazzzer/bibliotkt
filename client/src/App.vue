<template>
  <div class="bg-blue-400">
    <Navbar />
    <router-view />
  </div>
</template>

<script>
import { defineComponent } from 'vue'
import { useHead } from '@vueuse/head'
import Navbar from './components/global/Navbar.vue'

export default defineComponent({
  setup() {
    useHead({
      title: "Bibliotkt",
      link: [
        {
          rel: "stylesheet",
          href: "https://rsms.me/inter/inter.css"
        }
      ]
    });
  },
  components: { Navbar }
})
</script>