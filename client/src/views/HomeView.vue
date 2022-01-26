<template>
  <div class="about">
    <BookGrid />
  </div>
</template>

<script>
import BookGrid from "@/components/global/BookGrid.vue";
import { defineComponent } from "vue";
  export default defineComponent({
  
  components: { BookGrid }
})
</script>