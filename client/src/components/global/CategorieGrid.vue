<template>
  <div class="mb-10 border-b border-gray-200 rounded-full">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Nom</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(categorie, index) in categories" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ categorie }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['categories'])
</script>