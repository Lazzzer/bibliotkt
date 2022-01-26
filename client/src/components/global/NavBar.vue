<template>
  <header class="bg-sky-900">
    <nav class="px-4 mx-auto max-w-7xl sm:px-6 lg:px-8" aria-label="Top">
      <div class="flex items-center justify-between w-full py-6 border-b border-blue-500">
        <div class="flex items-center">
          <h1 class="text-2xl italic font-medium text-white">BiblioTkt</h1>
          <div class="block ml-10 space-x-8">
            <router-link to="/" class="text-base font-medium text-white hover:text-blue-50">Accueil</router-link>
            <router-link
              to="/search"
              class="text-base font-medium text-white hover:text-blue-50"
            >Recherche</router-link>
            <router-link
              v-if="employeStore.isLogged"
              to="/gestion"
              class="text-base font-medium text-white hover:text-blue-50"
            >Gestion</router-link>
          </div>
        </div>
        <div v-if="employeStore.isLogged" lass="ml-10 space-x-4">
          <div
            @click="employeStore.disconnect"
            class="inline-block px-6 py-2 text-base font-medium text-white border border-transparent rounded-md cursor-pointer bg-sky-700 hover:bg-opacity-75"
          >Se déconnecter</div>
        </div>
        <div v-else class="ml-10 space-x-4">
          <router-link
            to="/login"
            class="inline-block px-6 py-2 text-base font-medium text-white border border-transparent rounded-md bg-sky-700 hover:bg-opacity-75"
          >Se connecter</router-link>
        </div>
      </div>
    </nav>
  </header>
</template>

<script setup>
import { useEmployeStore } from '../../stores/employe';
const employeStore = useEmployeStore();
</script>