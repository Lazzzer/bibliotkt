<template>
  <label class="block text-lg font-bold text-sky-700">Nom d'auteur</label>
  <input
    v-model="nomAuteur"
    class="block w-full p-2 mt-1 border-2 rounded-md shadow-sm border-sky-900 focus:ring-sky-700 focus:border-sky-700 sm:text-sm"
    placeholder="Exemple: Victor Hugo"
  />

  <label for="langue" class="block mt-1 text-lg font-bold text-sky-700">Langue</label>
  <select
    v-model="selectedLangue"
    id="{{ index }}"
    name="{{ langue }}"
    class="block w-full p-2 mt-1 border-2 rounded-md shadow-sm border-sky-900 focus:ring-sky-700 focus:border-sky-700 sm:text-sm"
  >
    <option :value="-1">Toutes les langues</option>
    <option v-for="(langue, index) in langues" :key="index" :value="index">{{ langue }}</option>
  </select>

  <label class="block mt-4 text-lg font-bold text-sky-700">Catégories</label>
  <div>
    <span
      v-for="(categorie, index) in categories"
      :key="index"
      @click="selectedCategorie.includes(categories[index]) ? removeCategorie(index) : addCategorie(index)"
      :class="[selectedCategorie.includes(categories[index]) ? 'bg-sky-700 text-white' : 'bg-sky-200 text-gray-800', 'cursor-pointer inline-flex items-center px-3 py-0.5 rounded-md mr-1 mt-2 text-sm font-medium hover:bg-opacity-75']"
    >{{ categorie }}</span>
  </div>

  <button
    @click="fetchLivresWithFilters"
    class="p-2 my-4 text-sm text-white rounded-md bg-sky-700 hover:bg-opacity-75"
  >Appliquer</button>

    <button
    @click="resetFilters"
    class="p-2 my-4 ml-4 text-sm text-white bg-gray-600 rounded-md hover:bg-opacity-75"
  >Réinitialiser</button>

  <BookGrid v-if="livres.length > 0 && !fetchErr" :livres="livres" />
  <div v-if="fetchErr" class="italic">Pas de résultats</div>
</template>

<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";
import BookGrid from "../global/BookGrid.vue";

// Résultat de la recherche
const fetchErr = ref(false);
const livres = ref([]);

const fetchLivresWithFilters = async () => {
  let queryString = "";
  let connector = "?";

  if (nomAuteur.value !== "") {
    queryString += `${connector}nomAuteur=${nomAuteur.value}`;
    connector = "&";
  }

  if (selectedLangue.value !== -1) {
    queryString += `${connector}langue=${selectedLangue.value}`
    connector = "&";
  }

  if (selectedCategorie.value.length > 0) {
    selectedCategorie.value.forEach(categorie => {
      queryString += `${connector}nomCategories=${categorie}`
      connector = "&";
    });
  }
  console.log(queryString)
  axios.get('livresByFilters' + queryString).then(res => {
    livres.value = res.data;
    fetchErr.value = false;
  }).catch(err => {
    console.log(err.response.data);
    if (err.response.data.status === 404 || err.response.data.status === 400) {
      fetchErr.value = true;
    }
  })
}

const resetFilters = () => {
  livres.value = [];
  fetchErr.value = false;
  selectedCategorie.value = [];
  selectedLangue.value = -1;
  nomAuteur.value = "";
}

// Auteurs
const nomAuteur = ref("");

// Langues
const langues = ref(['Français', 'Anglais', 'Allemand', 'Italien', 'Espagnol'])
const selectedLangue = ref(-1);


// Catégories
const categories = ref([]);
const selectedCategorie = ref([]);

const addCategorie = (index) => {
  selectedCategorie.value.push(categories.value[index]);
}

const removeCategorie = (index) => {
  selectedCategorie.value = selectedCategorie.value.filter(item => item !== categories.value[index])
}

const fetchCategories = async () => {
  axios.get('categories').then(res => {
    categories.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

// Fetch des catégories lors du mount du component dans le DOM
onMounted(() => {
  fetchCategories();
})
</script>