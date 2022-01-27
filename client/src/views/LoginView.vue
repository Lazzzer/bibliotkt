<template>
  <div class="px-4 mx-auto mt-10 max-w-7xl sm:px-6 lg:px-8">
    <div class="max-w-4xl mx-auto">
      <h1 class="mb-10 text-2xl font-bold text-sky-800">Portail de connexion</h1>
      <div class="mt-8 space-y-6">
        <div class="-space-y-px rounded-md shadow-sm">
          <div>
            <input
              v-model="login"
              name="login"
              type="login"
              autocomplete="login"
              required
              class="relative block w-full px-3 py-2 text-gray-900 placeholder-gray-500 border border-gray-300 rounded-none appearance-none rounded-t-md focus:outline-none focus:ring-sky-500 focus:border-sky-500 focus:z-10"
              placeholder="Pseudonyme"
            />
          </div>
          <div>
            <label for="password" class="sr-only">Password</label>
            <input
              v-model="password"
              name="password"
              type="password"
              required
              class="relative block w-full px-3 py-2 text-gray-900 placeholder-gray-500 border border-gray-300 rounded-none appearance-none rounded-b-md focus:outline-none focus:ring-sky-500 focus:border-sky-500 focus:z-10"
              placeholder="Mot de passe"
            />
          </div>
        </div>

        <div>
          <button
            @click="tryLogin"
            type="submit"
            class="relative flex justify-center w-full px-4 py-2 text-sm font-medium text-white border border-transparent rounded-md bg-sky-700 group hover:bg-sky-900 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-sky-500"
          >
            Se connecter
          </button>
        </div>
      </div>

      <h1 v-if="failed" class="mt-4 italic text-center text-rose-600">Les identifiants entrés sont incorrects.</h1>
    </div>
  </div>
</template>

<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useEmployeStore } from "../stores/employe";

const router = useRouter();
const employeStore = useEmployeStore();

const failed = ref(false);

const login = ref("");
const password = ref("");

const tryLogin = async () => {
  axios.post(`employe/login?login=${login.value}&password=${password.value}`).then(() => {
    employeStore.connect();
    router.push('/');
  }).catch(err => {
    console.log(err)
    failed.value = true;
  })
}

onMounted(() => {
  if (employeStore.isLogged)
    router.push('/');
})

</script>