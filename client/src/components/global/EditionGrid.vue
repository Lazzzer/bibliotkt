<template>
  <div class="mb-10 border-b border-gray-200 rounded-full">
    <table class="min-w-full border-2 divide-y divide-gray-200 table-fixed">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">ISSN</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Nom maison d'édition</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Type</th>
          <th class="px-6 py-3 text-xs font-bold tracking-wider text-left uppercase text-sky-700">Langue</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="(edition, index) in editions" :key="index" class="cursor-pointer hover:bg-gray-200">
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ edition.issn }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ mapMaisonToId(edition.idMaison) }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ getType(edition.type) }}</td>
          <td class="px-6 py-2 text-sm italic text-gray-700 whitespace-nowrap">{{ getLangue(edition.langue) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
defineProps(['editions'])
import axios from "axios";
import { onBeforeMount, ref } from "vue";

const maisons = ref([])

const getLangue = (langue) => {
  switch (langue) {
    case 0:
      return 'Français';
    case 1:
      return 'Anglais';
    case 2:
      return 'Allemand';
    case 3:
      return 'Italien';
    case 4:
      return 'Espagnol';
    default:
      break;
  }
}

const getType = (type) => {
  switch (type) {
    case 0:
      return 'Résumé';
    case 1:
      return 'Standard';
    case 2:
      return 'Poche';
    case 3:
      return 'Article';
    default:
      break;
  }
}

const mapMaisonToId = (id) => {
  return maisons.value.find(maison => maison.id === id).nom
}

const fetchMaisonsEditions = async () => {
  axios.get('maisonsEdition').then(res => {
    maisons.value = res.data;
  }).catch(err => {
    console.log(err.response.data);
  })
}

onBeforeMount(() => {
  fetchMaisonsEditions();
})


</script>