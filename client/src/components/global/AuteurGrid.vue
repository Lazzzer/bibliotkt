<template>
  <div class="mb-10 border-b border-gray-200 rounded-full BookGrid">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Id</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Nom</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Prénom</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(auteur, index) in auteurs" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ auteur.id }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ auteur.nom }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ auteur.prenom }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['auteurs'])
</script>