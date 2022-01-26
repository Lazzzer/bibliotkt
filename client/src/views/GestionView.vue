<template>
  <div class="gestion">
    <MemberGrid /><br>
    <CategorieGrid /><br>
    <MaisonEditionGrid /><br>
    <AuteurGrid /><br>
    <EmpruntGrid />
    <BookDetail />
  </div>
</template>

<script setup>
import { onBeforeMount } from "vue";
import { useRouter } from "vue-router";
import { useEmployeStore } from "../stores/employe";
import MemberGrid from "../components/global/memberGrid.vue";
import CategorieGrid from "../components/global/CategorieGrid.vue";
import MaisonEditionGrid from "../components/global/MaisonEditionGrid.vue";
import AuteurGrid from "../components/global/AuteurGrid.vue";
import EmpruntGrid from "../components/global/EmpruntGrid.vue";
import BookDetail from "../components/global/BookDetail.vue";


const router = useRouter();
const employeStore = useEmployeStore();

onBeforeMount(() => {
  if (!employeStore.isLogged)
    router.push('/login');
})

</script>