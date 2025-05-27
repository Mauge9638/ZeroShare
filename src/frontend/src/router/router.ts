import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory('/'),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: () => import('@/pages/HomePage.vue'),
    },
    {
      path: '/view/:id',
      name: 'View',
      component: () => import('@/pages/ViewPage.vue'),
    },
    {
      path: '/create',
      name: 'Create',
      component: () => import('@/pages/CreatePage.vue'),
    },
    {
      path: '/about',
      name: 'About',
      component: () => import('@/pages/AboutPage.vue'),
    },
  ],
})

export default router
