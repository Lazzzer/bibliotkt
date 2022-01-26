<template>
    <div class="AuteursGrid">        
        <h1  class="font-bold">Auteurs</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
                    <th> Id</th>
					<th> Nom</th>
					<th> Prénom</th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(auteur, index) in auteurs" :key="index" class="text-center">
                    <td>{{auteur.id}}</td>
                    <td>{{auteur.nom}}</td>
                    <td>{{auteur.prenom}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
        
    let auteurs = ref([]);

    const fetchAuteurs = async () => {
        axios.get('auteurs').then(res => {
            auteurs.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchAuteurs();
    })
</script>