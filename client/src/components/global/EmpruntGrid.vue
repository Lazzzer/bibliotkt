<template>
  <div class="mb-10 border-b border-gray-200 rounded-full BookGrid">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">ID</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Date de début</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Date d'échéance</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Date de rendu</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >Etat</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >idExemplaire</th>
          <th
            class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700"
          >idMembre</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(emprunt, index) in emprunts" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ emprunt.id }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ emprunt.dateDebut }}</td>
          <td :class="[compareDates(emprunt.dateRetourPlanifie, emprunt.dateRendu) ? 'text-red-500 font-bold' : 'text-gray-700' ,'px-6 py-2 text-sm  whitespace-nowrap']">{{ emprunt.dateRetourPlanifie }}</td>
          <td :class="[compareDates(emprunt.dateRetourPlanifie, emprunt.dateRendu) ? 'text-red-500 font-bold' : 'text-gray-700' ,'px-6 py-2 text-sm  whitespace-nowrap']">{{ emprunt.dateRendu }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ emprunt.etatUsure }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ emprunt.idExemplaire }}</td>
          <td class="px-6 py-2 text-sm text-gray-700 whitespace-nowrap">{{ emprunt.idMembre }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['emprunts'])

const compareDates = (planned, ending) => {
  const datePlanned = new Date(planned);

  console.log(Date.now() > datePlanned, ending)

  if (ending === null)
    return Date.now() > datePlanned;

  const dateEnd = new Date(ending);

  return dateEnd > datePlanned;
}

</script>