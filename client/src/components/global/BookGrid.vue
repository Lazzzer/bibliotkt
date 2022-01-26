<template>
    <div class="BookGrid">
        <table class="table table-bordered">
			<thead>
				<tr>
					<th> Livre</th>
					<th> Auteur</th>
					<th> Date de parution</th>
				</tr>

                
                <tr v-for="livre in livres">
                    <td>oui</td>
                </tr>

                <!--<tr v-for="livre in livres">
                    <td v-for="data in livre">{{data}}</td>
                </tr>-->
			</thead>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";

        
    let livres = ref([]);

    const fetchLivres = async () => {
        axios.get('livres').then(res => {
            livres = res.data;
            console.log(livres)
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchLivres();
    })
</script>