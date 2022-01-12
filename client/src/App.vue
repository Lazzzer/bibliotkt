<template>
<div class="bg-blue-400">
  <Navbar />
  <router-view />
</div>
</template>

<script>
import { defineComponent, computed, reactive } from 'vue'
import { useHead } from '@vueuse/head'
import Navbar from './components/global/Navbar.vue'

export default defineComponent({
    setup() {
        const siteData = reactive({
            title: `Bibliotkt`,
            description: `Beatiful library web application`,
        });
        useHead({
            // Can be static or computed
            title: computed(() => siteData.title),
            meta: [
                {
                    name: `description`,
                    content: computed(() => siteData.description),
                },
            ],
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