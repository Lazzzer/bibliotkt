<template>
    <div class="BookDetail">        
        <p class="font-bold">Titre: </p><p>{{livre.titre}}</p><br>
        <p class="font-bold">Genre: </p><p v-for="(cat, index) in livre.categories" :key="index" >{{cat.nom}} </p><br>
        <p class="font-bold">Auteur: </p><p v-for="(auteur, index) in livre.auteurs" :key="index" >{{auteur.nom}} </p><br>
        <p class="font-bold">Synopsis: </p><p>{{livre.synopsis}}</p><br>
        <p class="font-bold">Date de parution: </p><p>{{livre.dateParution}}</p><br>
        <p class="font-bold">Date d'acquisition: </p><p>{{livre.dateAcquisition}}</p><br>
        <p class="font-bold">Prix d'achat: </p><p>{{livre.prixAchat}} CHF</p><br>
        <p class="font-bold">Prix d'emprunt: </p><p>{{livre.prixEmprunt}} CHF</p><br><br>

        <h1 class="font-bold">Recommendations</h1><br>
        <BookGrid :livres="recommendations"/>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
    import BookGrid from "./BookGrid.vue";
        
    let livre = ref([]);
    let recommendations = ref([]);

    const fetchLivre = async () => {
        axios.get('livre/12345678').then(res => {
            livre.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    const fetchRecommendations = async (issn, auteur) => {
        console.log
        axios.get('livre/recommendations',  {params: { issn: issn, auteur: auteur}}).then(res => {
            recommendations.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }
    onMounted(() => {
        fetchLivre();
        fetchRecommendations(livre.value.issn, livre.value.auteurs[0]);
    })
</script>