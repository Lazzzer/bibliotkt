<template>
    <div class="empruntsGrid">        
        <h1  class="font-bold">Emprunts</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
                    <th> Id</th>
					<th> Date de début</th>
					<th> Date d'échéance</th>
					<th> Date de rendu</th>
					<th> Etat</th>
					<th> idExemplaire</th>
					<th> idMembre</th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(emprunt, index) in emprunts" :key="index" class="text-center">
                    <td>{{emprunt.id}}</td>
                    <td>{{emprunt.dateDebut}}</td>
                    <td>{{emprunt.dateRetourPlanifie}}</td>
                    <td>{{emprunt.dateRendu}}</td>
                    <td>{{emprunt.etatUsure}}</td>
                    <td>{{emprunt.idExemplaire}}</td>
                    <td>{{emprunt.idMembre}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
        
    let emprunts = ref([]);

    const fetchEmprunts = async () => {
        axios.get('emprunts').then(res => {
            emprunts.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchEmprunts();
    })
</script>