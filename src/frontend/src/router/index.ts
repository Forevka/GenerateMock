import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '@/views/Home.vue';
import apiClient from '@/api/ApiClient';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home,
  },
  {
    path: '/about',
    name: 'about',
    component: () => import(/* webpackChunkName: "about" */ '@/views/About.vue'),
  },
  {
    path: '/login',
    name: 'login',
    component: () => import(/* webpackChunkName: "login" */ '@/views/Login.vue'),
  },
  {
    path: '/register',
    name: 'register',
    component: () => import(/* webpackChunkName: "Register" */ '@/views/Register.vue'),
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: () => import(/* webpackChunkName: "Dashboard" */ '@/views/Dashboard.vue'),
    children: [
      {
        path: '', // /dashboard
        component: () => import(/* webpackChunkName: "Display" */ '@/views/Dashboard/RepositoriesDisplay.vue'),
        props: {
          mainLabel: 'My repositories',
        },
      },
      {
        path: 'add', // /dashboard
        component: () => import(/* webpackChunkName: "AddNew" */ '@/views/Dashboard/RepositoryAddNew.vue'),
        props: {
          mainLabel: 'New repository',
        },
      },
    ],
  },
  {
    path: '/explore',
    name: 'explore',
    component: () => import(/* webpackChunkName: "Dashboard" */ '@/views/Dashboard/RepositoriesDisplay.vue'),
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
