<template>
  <div class="px-4 mx-auto mt-10 max-w-7xl sm:px-6 lg:px-8">
    <h1 class="mb-10 text-4xl font-bold text-center text-sky-800">Bienvenue sur BiblioTkt !</h1>
    <div class="max-w-4xl mx-auto">
      <h1 class="mb-4 text-2xl font-bold text-sky-800">Livres du catalogue</h1>
      <BookGrid :livres="livres" />
    </div>
  </div>
</template>

<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";
import BookGrid from "@/components/global/BookGrid.vue";

const livres = ref([]);

const fetchLivres = async () => {
  axios.get('livres').then(res => {
    livres.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

onMounted(() => {
  fetchLivres();
})

</script>