<template>
    <div class="NewBook">
        <input type="text" placeholder="Issn" name="issn" required><br>
        <input type="text" placeholder="Titre" name="Titre" required><br>
        <input type="text" placeholder="Synopsis" name="synopsis" required><br>
        <input type="text" placeholder="Date de parution" name="dateParution" required><br>
        <input type="text" placeholder="Date d'acquisition" name="dateAcquisition" required><br>
        <input type="text" placeholder="Prix d'achat" name="prixAchat" required><br>
        <input type="text" placeholder="Prix d'emprunt" name="prixEmprunt" required><br>

        <h1 class="font-bold">Catégories</h1>
        <div v-for="(cat, index) in categories" :key="index" >
            <input type="checkbox" name="categories" :id="cat">
            <label class="ml-2" :for="cat">{{cat}}</label>
        </div>

        <h1 class="font-bold">Auteurs</h1>
        <div v-for="(auteur, index) in auteurs" :key="index" >
            <input type="checkbox" name="auteur" :id="auteur.id">
            <label class="ml-2" :for="auteur.id">{{auteur.nom + " " + auteur.prenom}}</label>
        </div>

        <button>Submit</button>
    </div>
</template>

<script setup>
import axios from "axios";
import { onBeforeMount, ref } from "vue";

const categories = ref([]);
const auteurs = ref([]);

const fetchCategories = async () => {
  axios.get('categories').then(res => {
    categories.value = res.data;
  }).catch(err => {
    console.log(err.response.data.status);
  })
}

const fetchAuteurs = async () => {
  axios.get('auteurs').then(res => {
    auteurs.value = res.data;
  }).catch(err => {
    console.log(err.response.data.status);
  })
}

onBeforeMount(() => {
  fetchCategories();
  fetchAuteurs();
})
</script>