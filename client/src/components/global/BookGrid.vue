<template>
  <div class="border-b border-gray-200 rounded-full BookGrid">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">ISSN</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Titre</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Date de parution</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(livre, index) in livres" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ livre.issn }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ livre.titre }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ livre.dateParution }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['livres'])
</script>