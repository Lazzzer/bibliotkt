<template>
  <div class="mb-10 border-b border-gray-200 rounded-full BookGrid">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">ID</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Nom</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Prénom</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Date d'inscription</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Adresse</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr
          v-for="(membre, index) in membres"
          :key="index"
          class="cursor-pointer hover:bg-gray-200"
        >
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ membre.id }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ membre.nom }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ membre.prenom }}</td>
          <td
            class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap"
          >{{ membre.dateCreationCompte }}</td>
          <td
            class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap"
          >{{ membre.rue + " " + membre.noRue + ", " + membre.npa + " " + membre.localite }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['membres'])



</script>