<template>
  <div class="mb-10 border-b border-gray-200 rounded-full">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">ISSN</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Id maison d'édition</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Type</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Langue</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(edition, index) in editions" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ edition.issnLivre }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ edition.idMaisonEdition }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ edition.type }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ edition.langue }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['editions'])
</script>