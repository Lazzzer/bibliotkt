<template>
    <div class="BookGrid">        
        <h1  class="font-bold">Livres</h1><br>
        <table class="table-fixed">
			<thead>
				<tr class="text-center">
					<th> Livre</th>
					<th> Date de parution</th>
				</tr>
            
			</thead>
            <tbody>
                <tr v-for="(livre, index) in livres" :key="index" class="text-center">
                    <td>{{livre.titre}}</td>
                    <td>{{livre.dateParution}}</td>
                </tr>
            </tbody>
		</table>
    </div>
</template>

<script setup>
defineProps(['livres'])
</script>