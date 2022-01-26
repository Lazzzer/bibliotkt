
<template>
  <div>
    <label for="email" class="block text-sm font-medium text-gray-700">Titre du livre</label>
    <div class="mt-1">
      <input
        v-model="title"
        class="block w-full p-2 border-2 rounded-md shadow-sm border-sky-900 focus:ring-sky-700 focus:border-sky-700 sm:text-sm"
        placeholder="Seigneur des anneaux"
      />

      <button
        @click="fetchLivresByTitle"
        class="p-2 my-4 text-sm text-white rounded-md bg-sky-700"
      >Appliquer</button>

      <BookGrid v-if="livres.length > 0" :livres="livres" />
    </div>
  </div>
</template>

<script setup>

import axios from "axios";
import { ref } from "vue";
import BookGrid from "../global/BookGrid.vue";

const title = ref("");
let livres = ref([]);


const fetchLivresByTitle = async () => {
  axios.get('livresByTitle', { params: { titre: title.value } }).then(res => {
    livres.value = res.data;
    console.log(livres)
  }).catch(err => {
    console.log(err.response.data);
  })
}

</script>