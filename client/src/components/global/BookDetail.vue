<template>
  <div class="BookDetail">
    <div class="mb-4">
      <h2 class="text-2xl font-bold text-sky-700">{{ livre.titre }}</h2>
    </div>
    <div class="mb-4">
      <h2 class="font-bold text-sky-700">ISSN:</h2>
      <p>{{ livre.issn }}</p>
    </div>
    <div class="mb-4">
      <h2 class="font-bold text-sky-700">Catégories:</h2>
      <p v-for="(cat, index) in livre.categories" :key="index">{{ cat.nom }}</p>
    </div>

    <div class="mb-4">
      <h2 class="font-bold text-sky-700">Auteurs:</h2>
      <p v-for="(auteur, index) in livre.auteurs" :key="index">{{ auteur.nom }}</p>
    </div>

    <div class="mb-4">
      <h2 class="font-bold text-sky-700">Synopsis:</h2>
      <p>{{ livre.synopsis }}</p>
    </div>

    <div class="mb-4">
      <h2 class="font-bold text-sky-700">Date de parution:</h2>
      <p>{{ livre.dateParution }}</p>
    </div>

    <div v-if="employeStore.isLogged" class="mb-4">
      <h2 class="font-bold text-sky-700">Date d'acquisition:</h2>
      <p>{{ livre.dateAcquisition }}</p>
    </div>

    <div v-if="employeStore.isLogged" class="mb-4">
      <h2 class="font-bold text-sky-700">Prix d'achat:</h2>
      <p>{{ livre.prixAchat }} CHF</p>
    </div>

    <div v-if="employeStore.isLogged" class="mb-4">
      <h2 class="font-bold text-sky-700">Prix d'emprunt:</h2>
      <p>{{ livre.prixEmprunt }} CHF</p>
    </div>

    <div v-if="employeStore.isLogged" class="mb-4">
      <h2 class="font-bold text-sky-700">Nombre total d'exemplaires:</h2>
      <p>{{ nbExemplaires }}</p>
    </div>


    <h1 class="mb-4 text-xl font-bold text-sky-700">Recommandations</h1>
    <BookGrid :livres="recommandations" />
    
    <div v-if="employeStore.isLogged">
      <h1 class="mb-4 text-xl font-bold text-sky-700">Editions</h1>
      <EditionGrid :editions="editions"/>
    </div>

    <div class="mb-20" v-if="employeStore.isLogged">
      <h1 class="mb-4 text-xl font-bold text-sky-700">Emprunts</h1>
      <EmpruntGrid :emprunts="emprunts" v-if="emprunts.length > 0" />
      <h2 v-else class="italic">Pas d'emprunts</h2>
    </div>

  </div>
</template>

<script setup>
import BookGrid from "./BookGrid.vue";
import axios from "axios";
import { onBeforeMount, ref } from "vue";
import { useEmployeStore } from '../../stores/employe';
import EmpruntGrid from "./EmpruntGrid.vue";
import EditionGrid from "./EditionGrid.vue";
const employeStore = useEmployeStore();

const livre = ref([]);
const recommandations = ref([]);
const editions = ref([]);

const emprunts = ref([]);
const nbExemplaires = ref(0);

const props = defineProps(['issn']);

const fetchLivre = async () => {
  axios.get(`livre/${props.issn}`).then(res => {
    livre.value = res.data;
    fetchRecommandations();
    fetchEditions();
    fetchEmprunts();
    fetchNbExemplaires();
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchEmprunts = async () => {
  axios.get(`emprunts/livre/${livre.value.issn}`).then(res => {
    emprunts.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchRecommandations = async () => {

  let queryString = "";
  let connector = "&";

  if (livre.value.auteurs.length !== "") {
    queryString += `${connector}nomAuteur=${livre.value.auteurs[0].nom}`;
    connector = "&";
  }

  if (livre.value.categories.length > 0) {
    livre.value.categories.forEach(categorie => {
      queryString += `${connector}nomCategories=${categorie.nom}`
      connector = "&";
    });
  }

  axios.get(`livre/recommandations?issn=${livre.value.issn}${queryString}`).then(res => {
    recommandations.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchEditions = async () => {
  axios.get(`editions/${livre.value.issn}`).then(res => {
    editions.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchNbExemplaires = async () => {
  axios.get(`nbExemplaires/${livre.value.issn}`).then(res => {
    nbExemplaires.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

onBeforeMount(() => {
  fetchLivre();
})

</script>