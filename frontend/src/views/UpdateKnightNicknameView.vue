<script setup lang="ts">
import InputGroup from 'primevue/inputgroup';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import { ref } from 'vue'
import knightGateway from '@/gateways/KnightGateway'
import { useRoute, useRouter } from 'vue-router'
import { UpdateKnightNicknameDto } from '@/dto/UpdateKnightNicknameDto'

const dto = ref<UpdateKnightNicknameDto>(new UpdateKnightNicknameDto());

const router = useRouter()
const route = useRoute()

const create = async (evt: Event) => {
  evt.preventDefault()

  await knightGateway.updateNickname(route.params.id as string, dto.value);

  router.push('/knights')
}
</script>

<template>
  <form @submit="create" class="container">
    <h2>Main data:</h2>

    <InputGroup>
      <InputText :required="true" placeholder="Nickname" v-model="dto.nickname"></InputText>
    </InputGroup>

    <Button label="Update nickname" type="submit"/>
  </form>
</template>

<style>
.container {
  width: 90%;
  margin: auto;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.weapon-group {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
}
</style>