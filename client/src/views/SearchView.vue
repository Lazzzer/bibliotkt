<template>
  <div class="px-4 mx-auto mt-10 max-w-7xl sm:px-6 lg:px-8">
    <div class="max-w-4xl mx-auto">
      <h1 class="mb-10 text-2xl font-bold text-sky-800">Recherche de livres</h1>

      <div class="mb-10">
        <button
          @click="changeAdvanced(false)"
          :class="[advanced ? 'bg-gray-400' : 'bg-sky-700']"
          class="p-2 mr-8 text-white rounded-md hover:bg-opacity-75"
        >Recherche simple</button>

        <button
          @click="changeAdvanced(true)"
          :class="[!advanced ? 'bg-gray-400' : 'bg-sky-700']"
          class="p-2 text-white rounded-md hover:bg-opacity-75"
        >Recherche avancée</button>
      </div>

      <SimpleSearch v-if="!advanced" />
      <AdvancedSearch v-else />
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import SimpleSearch from '../components/search/SimpleSearch.vue';
import AdvancedSearch from '../components/search/AdvancedSearch.vue';

const advanced = ref(false);

const changeAdvanced = (value => {
  advanced.value = value;
});

</script>