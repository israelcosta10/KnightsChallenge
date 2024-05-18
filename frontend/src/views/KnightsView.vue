<script setup lang="ts">
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import { ref } from 'vue'
import { KnightViewDto } from '@/dto/KnightViewDto'
import knightGateway from '@/gateways/KnightGateway'
import { watch } from 'vue'

type Option = 'heroes' | 'knights'

const selectedList = ref<Option>('knights')

const knights = ref<KnightViewDto[]>([])

const listKnights = async () => {
  if (selectedList.value === 'knights') {
    knights.value = await knightGateway.listKnights()
  }

  if (selectedList.value === 'heroes') {
    knights.value = await knightGateway.listHeroes()
  }
}

watch(selectedList, listKnights)

listKnights()

const deleteKnight = async (knightId: string) => {
  await knightGateway.delete(knightId)

  await listKnights()
}
</script>

<template>
  <div class="button-container">
    <RouterLink to="/knights">
      <Button label="Create Knight"></Button>
    </RouterLink>
  </div>

  <div class="button-container">
    <Button
      v-if="selectedList === 'knights'"
      label="List Heroes"
      @click="selectedList = 'heroes'"
    ></Button>
    <Button
      v-if="selectedList === 'heroes'"
      label="List Knights"
      @click="selectedList = 'knights'"
    ></Button>
  </div>

  <DataTable :value="knights" table-style="">
    <Column field="name" header="Name" :sortable="true"></Column>

    <Column field="age" header="Age" :sortable="true"></Column>

    <Column field="weapons" header="Weapons" :sortable="true"></Column>

    <Column field="attribute" header="Attribute" :sortable="true"></Column>

    <Column field="attack" header="Attack" :sortable="true"></Column>

    <Column field="exp" header="Exp" :sortable="true"></Column>

    <Column v-if="selectedList === 'knights'" header="Actions">
      <template #body="slotProps">
        <div class="icons-container">
          <RouterLink :to="`/knights/${slotProps.data.id}`">
            <i class="pi pi-pencil"></i>
          </RouterLink>

          <i class="pi pi-trash" @click="() => deleteKnight(slotProps.data.id)"></i>
        </div>
      </template>
    </Column>
  </DataTable>
</template>

<style>
.button-container {
  display: flex;
  justify-content: end;
  padding: 1rem 0;
}

.icons-container {
  display: flex;
  gap: 0.5rem;

  i {
    cursor: pointer;
  }
}
</style>
