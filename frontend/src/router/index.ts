import { createRouter, createMemoryHistory } from 'vue-router'
import KnightsView from '../views/KnightsView.vue'
import CreateKnightsView from '../views/CreateKnightView.vue'
import UpdateKnightNicknameView from '../views/UpdateKnightNicknameView.vue'

export const router = createRouter({
  history: createMemoryHistory(import.meta.env.BASE_URL),

  routes: [
    {
      path: '/',
      name: '/',
      component: KnightsView
    },
    {
      path: '/knights',
      name: '/knights',
      component: CreateKnightsView
    },
    {
      path: '/knights/:id',
      name: '/knights/:id',
      component: UpdateKnightNicknameView
    }
  ]
})
