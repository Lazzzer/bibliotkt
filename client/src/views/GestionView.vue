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

<script>
import { defineComponent } from "vue";
import MemberGrid from "../components/global/memberGrid.vue";
import CategorieGrid from "../components/global/CategorieGrid.vue";
import MaisonEditionGrid from "../components/global/MaisonEditionGrid.vue";
import AuteurGrid from "../components/global/AuteurGrid.vue";
import EmpruntGrid from "../components/global/EmpruntGrid.vue";
import BookDetail from "../components/global/BookDetail.vue";

export default defineComponent({
  
  components: { MemberGrid, CategorieGrid, MaisonEditionGrid, AuteurGrid, EmpruntGrid, BookDetail }
})
</script>