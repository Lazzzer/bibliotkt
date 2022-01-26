<template>
  <div class="px-4 mx-auto mt-10 max-w-7xl sm:px-6 lg:px-8">
    <div class="max-w-4xl mx-auto">
      <h1 class="mb-4 text-2xl font-bold text-sky-800">Portail de gestion</h1>

      <div class="mb-10">
        <button
          @click="setToggledValue('Membres')"
          :class="[toggledValue === 'Membres' ? 'bg-sky-700' : 'bg-gray-400']"
          class="p-2 mr-4 text-white rounded-md hover:bg-opacity-75"
        >Membres</button>

        <button
          @click="setToggledValue('Catégories')"
          :class="[toggledValue === 'Catégories' ? 'bg-sky-700' : 'bg-gray-400']"
          class="p-2 mr-4 text-white rounded-md hover:bg-opacity-75"
        >Catégories</button>
       
        <button
          @click="setToggledValue('Maisons')"
          :class="[toggledValue === 'Maisons' ? 'bg-sky-700' : 'bg-gray-400']"
          class="p-2 mr-4 text-white rounded-md hover:bg-opacity-75"
        >Maisons d'édition</button>
        
        <button
          @click="setToggledValue('Auteurs')"
          :class="[toggledValue === 'Auteurs' ? 'bg-sky-700' : 'bg-gray-400']"
          class="p-2 mr-4 text-white rounded-md hover:bg-opacity-75"
        >Auteurs</button>
        
        <button
          @click="setToggledValue('Emprunts')"
          :class="[toggledValue === 'Emprunts' ? 'bg-sky-700' : 'bg-gray-400']"
          class="p-2 mr-4 text-white rounded-md hover:bg-opacity-75"
        >Emprunts</button>
      </div>

      <MemberGrid v-if="toggledValue === 'Membres'" :membres="membres" />
      <CategorieGrid v-if="toggledValue === 'Catégories'" :categories="categories" />
      <MaisonEditionGrid v-if="toggledValue === 'Maisons'" :maisons="maisons" />
      <AuteurGrid v-if="toggledValue === 'Auteurs'" :auteurs="auteurs" />
      <EmpruntGrid v-if="toggledValue === 'Emprunts'" :emprunts="emprunts" />
      <!-- <BookDetail /> -->
    </div>
  </div>
</template>

<script setup>
import MemberGrid from "../components/global/memberGrid.vue";
import CategorieGrid from "../components/global/CategorieGrid.vue";
import MaisonEditionGrid from "../components/global/MaisonEditionGrid.vue";
import AuteurGrid from "../components/global/AuteurGrid.vue";
import EmpruntGrid from "../components/global/EmpruntGrid.vue";
import BookDetail from "../components/global/BookDetail.vue";
import axios from "axios";
import { onBeforeMount, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useEmployeStore } from "../stores/employe";


const router = useRouter();
const employeStore = useEmployeStore();

const toggledValue = ref('Membres');

const setToggledValue = (value => {
  toggledValue.value = value;
});

const membres = ref([]);
const categories = ref([]);
const maisons = ref([]);
const emprunts = ref([]);
const auteurs = ref([]);


const fetchMembres = async () => {
  axios.get('membres').then(res => {
    membres.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchCat = async () => {
  axios.get('categories').then(res => {
    categories.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchEmprunts = async () => {
  axios.get('emprunts').then(res => {
    emprunts.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchMaisonsEditions = async () => {
  axios.get('maisonsEdition').then(res => {
    maisons.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

const fetchAuteurs = async () => {
  axios.get('auteurs').then(res => {
    auteurs.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

onBeforeMount(() => {
  if (!employeStore.isLogged)
    router.push('/login');
})

onMounted(() => {
  fetchMembres();
  fetchCat();
  fetchEmprunts();
  fetchMaisonsEditions();
  fetchAuteurs();
})

</script>