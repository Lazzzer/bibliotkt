<template>
    <div class="MaisonEditionGrid">        
        <h1 class="font-bold">Maisons d'éditions</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
                    <th> Id</th>
					<th> Nom</th>
                    <th> Adresse mail</th>
                    <th> Adresse</th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(maison, index) in maisons" :key="index" class="text-center">
                    <td>{{maison.id}}</td>
                    <td>{{maison.nom}}</td>
                    <td>{{maison.email}}</td>
                    <td>{{maison.rue + " " + maison.noRue + ", " + maison.npa + " " + maison.localite + ", " + maison.pays}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
        
    let maisons = ref([]);

    const fetchMaisonsEditions = async () => {
        axios.get('maisonsEdition').then(res => {
            maisons.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchMaisonsEditions();
    })
</script>