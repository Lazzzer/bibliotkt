<template>
  <label class="block text-lg font-bold text-sky-700">Titre du livre</label>
  <input
    v-model="title"
    class="block w-full p-2 mt-1 border-2 rounded-md shadow-sm border-sky-900 focus:ring-sky-700 focus:border-sky-700 sm:text-sm"
    placeholder="Exemple: Seigneur des anneaux"
  />
  <button
    @click="fetchLivresByTitle"
    class="p-2 my-4 text-sm text-white rounded-md bg-sky-700 hover:bg-opacity-75"
  >Appliquer</button>

  <BookGrid v-if="livres.length > 0 && !fetchErr" :livres="livres" />
  <div v-if="fetchErr" class="italic">Pas de résultats</div>
</template>

<script setup>

import axios from "axios";
import { ref } from "vue";
import BookGrid from "../global/BookGrid.vue";

const title = ref("");
const fetchErr = ref(false);
const livres = ref([]);


const fetchLivresByTitle = async () => {
  axios.get('livresByTitle', { params: { titre: title.value } }).then(res => {
    livres.value = res.data;
    fetchErr.value = false;
  }).catch(err => {
    console.log(err.response.data.status);
    if (err.response.data.status === 404 || err.response.data.status === 400) {
      fetchErr.value = true;
    }
  })
}
</script>