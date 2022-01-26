<template>
  <div class="mb-10 border-b border-gray-200 rounded-full BookGrid">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Id</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Nom</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Adresse mail</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Adresse</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(maison, index) in maisons" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{maison.id}}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{maison.nom}}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{maison.email}}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{maison.rue + " " + maison.noRue + ", " + maison.npa + " " + maison.localite + ", " + maison.pays}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['maisons'])
</script>