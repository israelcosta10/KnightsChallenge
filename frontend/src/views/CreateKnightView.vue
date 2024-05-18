<script setup lang="ts">
import InputGroup from 'primevue/inputgroup';
import InputText from 'primevue/inputtext';
import Calendar from 'primevue/calendar';
import InputNumber from 'primevue/inputnumber';
import ToggleButton from 'primevue/togglebutton';
import Dropdown from 'primevue/dropdown';
import Button from 'primevue/button';
import { ref } from 'vue'
import { CreateKnightDto } from '@/dto/CreateKnightDto'
import knightGateway from '@/gateways/KnightGateway'
import { useRouter } from 'vue-router'

const dto = ref<CreateKnightDto>(new CreateKnightDto());

const attributes = ref<{ id: string, name: string }[]>([
  { id: "strength", name: "Strength" },
  { id: "dexterity", name: "Dexterity" },
  { id: "constitution", name: "Constitution" },
  { id: "intelligence", name: "Intelligence" },
  { id: "charisma", name: "Charisma" }
])

const router = useRouter()

const create = async (evt: Event) => {
  evt.preventDefault()

  await knightGateway.create(dto.value);

  router.push('/knights')
}
</script>

<template>
  <form @submit="create" class="container">
    <h2>Main data:</h2>

    <InputGroup>
      <InputText :required="true" placeholder="Name" v-model="dto.name"></InputText>
    </InputGroup>

    <InputGroup>
      <InputText :required="true" placeholder="Nickname" v-model="dto.nickname"></InputText>
    </InputGroup>

    <InputGroup>
      <Dropdown :required="true" :options="attributes" optionValue="id" optionLabel="name" placeholder="Select a main attribute" v-model="dto.keyAttribute" />
    </InputGroup>

    <Calendar :required="true" placeholder="Birthday" v-model="dto.birthday" />

    <h2>Weapons:</h2>

    <div class="container">
      <div class="weapon-group" v-for="(weapon, index) in dto.weapons">
        <InputGroup>
          <InputText :required="true" placeholder="Name" v-model="weapon.name"></InputText>
        </InputGroup>

        <InputGroup>
          <InputNumber :required="true" placeholder="Mod" v-model="weapon.mod" />
        </InputGroup>

        <InputGroup>
          <Dropdown :required="true" :options="attributes" optionValue="id" optionLabel="name"  placeholder="Select a main attribute" v-model="weapon.attr" />
        </InputGroup>

        <InputGroup>
          <ToggleButton :required="true" onLabel="Equipped" offLabel="Not equipped" v-model="weapon.equipped" />
        </InputGroup>

        <Button label="Delete" @click="() => dto.removeWeapon(index)" />
      </div>

      <Button label="Add Weapon" @click="() => dto.addEmptyWeapon()" />
    </div>

    <h2>Attributes:</h2>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.strength" placeholder="Strength" />
    </InputGroup>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.dexterity" placeholder="Dexterity" />
    </InputGroup>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.constitution" placeholder="Constitution" />
    </InputGroup>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.intelligence" placeholder="Intelligence" />
    </InputGroup>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.wisdom" placeholder="Wisdom" />
    </InputGroup>

    <InputGroup>
      <InputNumber :required="true" v-model="dto.attributes.charisma" placeholder="Charisma" />
    </InputGroup>

    <Button label="Create" type="submit"/>
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