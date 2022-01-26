<template>
    <div class="CategorieGrid">        
        <h1  class="font-bold">Catégories</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
					<th> Nom </th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(cat, index) in categories" :key="index">
                    <td class="text-center">{{cat}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { onMounted, ref } from "vue";
        
    let categories = ref([]);

    const fetchCat = async () => {
        axios.get('categories').then(res => {
            categories.value = res.data;
        }).catch(err => {
            console.log(err.response.data);
        })
    }

    onMounted(() => {
        fetchCat();
    })
</script>