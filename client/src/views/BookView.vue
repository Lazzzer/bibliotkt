<template>
  <div class="px-4 mx-auto mt-10 max-w-7xl sm:px-6 lg:px-8">
    <div class="max-w-4xl mx-auto">
      <BookDetail :issn="route.params.issn" :key="route.params.issn" />
    </div>
  </div>
</template>

<script setup>
import { useRoute } from 'vue-router';
import BookDetail from '../components/global/BookDetail.vue';

const route = useRoute();
</script>