<template>
    <div class="MemberGrid">        
        <h1  class="font-bold">Membres</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
                    <th> Id</th>
					<th> Nom</th>
					<th> Prénom</th>
                    <th> Date d'inscription</th>
                    <th> Adresse</th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(membre, index) in membres" :key="index" class="text-center">
                    <td>{{membre.id}}</td>
                    <td>{{membre.nom}}</td>
                    <td>{{membre.prenom}}</td>
                    <td>{{membre.dateCreationCompte}}</td>
                    <td>{{membre.rue + " " + membre.noRue + ", " + membre.npa + " " + membre.localite}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
        
    let membres = ref([]);

    const fetchMembres = async () => {
        axios.get('membres').then(res => {
            membres.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchMembres();
    })
</script>