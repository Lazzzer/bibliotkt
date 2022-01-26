<template>
  <div>
    Hello from book page {{ route.params.issn }}
  </div>
  <!-- <BookDetail /> -->
</template>

<script setup>
import { useRoute } from 'vue-router';
// import BookDetail from '../components/global/BookDetail.vue';

const route = useRoute();

</script>