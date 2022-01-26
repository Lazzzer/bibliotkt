<template>
  <div class="about">
    <BookGrid :livres="livres" />
  </div>
</template>

<script setup>
import axios from "axios";
import { onMounted, ref } from "vue";
import BookGrid from "@/components/global/BookGrid.vue";
    
let livres = ref([]);

const fetchLivres = async () => {
    axios.get('livres').then(res => {
        livres.value = res.data;
    }).catch(err => {
        console.log(err.response.data);
    })
}

onMounted(() => {
    fetchLivres();
})

</script>