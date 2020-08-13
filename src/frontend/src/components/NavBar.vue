<template>
  <nav class="w-screen fixed bg-gray-800 z-10">
    <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
      <div class="relative flex items-center justify-between h-16">
        <div class="absolute inset-y-0 left-0 flex items-center sm:hidden">
          <!-- Mobile menu button-->
          <button
            v-on:click="isBarOpen = !isBarOpen"
            class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:bg-gray-700 focus:text-white transition duration-150 ease-in-out"
            aria-label="Main menu"
            aria-expanded="false"
          >
            <!-- Icon when menu is closed. -->
            <!-- Menu open: "hidden", Menu closed: "block" -->
            <svg
              class="block h-6 w-6"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M4 6h16M4 12h16M4 18h16"
              />
            </svg>
            <!-- Icon when menu is open. -->
            <!-- Menu open: "block", Menu closed: "hidden" -->
            <svg
              class="hidden h-6 w-6"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M6 18L18 6M6 6l12 12"
              />
            </svg>
          </button>
        </div>
        <div
          class="p-48 flex-1 flex items-center justify-center sm:items-stretch sm:justify-start"
        >
          <!--<div class="flex-shrink-0">
            <img class="block lg:hidden h-8 w-auto" src="https://tailwindui.com/img/logos/workflow-mark-on-dark.svg" alt="Workflow logo">
          <img class="hidden lg:block h-8 w-auto" src="https://tailwindui.com/img/logos/workflow-logo-on-dark.svg" alt="Workflow logo">
          </div>-->
          <div :class="{hidden: true, 'sm:hidden': isBarOpen, 'sm:block': !isBarOpen, 'sm:ml-6': true}">
            <div class="flex">
              <router-link
                to="/"
                class="hover:underline ml-4 px-3 py-2 rounded-md text-sm font-medium leading-5 text-gray-300 hover:text-white hover:bg-gray-700 focus:outline-none focus:text-white transition duration-150 ease-in-out"
                >Home</router-link
              >
              <router-link
                to="/about"
                class="hover:underline ml-4 px-3 py-2 rounded-md text-sm font-medium leading-5 text-gray-300 hover:text-white hover:bg-gray-700 focus:outline-none focus:text-white transition duration-150 ease-in-out"
                >About</router-link
              >
            </div>
          </div>
        </div>
        <div
          class="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0"
        >
          <!-- Profile dropdown -->
          <div
            id="user-menu-wrapper"
            v-if="isLogged"
            class="ml-3 relative"
          >
            <div>
              <button
                class="p-48 flex text-sm border-2 border-transparent rounded-full focus:outline-none focus:border-white transition duration-150 ease-in-out"
                id="user-menu"
                aria-label="User menu"
                aria-haspopup="true"
              >
                <img
                  id="user-menu-img"
                  class="h-8 w-8 rounded-full"
                  src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
                  alt=""
                />
              </button>
            </div>
            <!--
            Profile dropdown panel, show/hide based on dropdown state.

            Entering: "transition ease-out duration-100"
              From: "transform opacity-0 scale-95"
              To: "transform opacity-100 scale-100"
            Leaving: "transition ease-in duration-75"
              From: "transform opacity-100 scale-100"
              To: "transform opacity-0 scale-95"
          -->
            <div
              v-if="isMenuOpen"
              :class="{
                'origin-top-right': true,
                absolute: true,
                'right-0': true,
                'mt-2': true,
                'w-48': true,
                'rounded-md': true,
                'shadow-lg': true,
              }"
            >
              <div
                :class="{
                  'py-1': true,
                  'rounded-md': true,
                  'bg-white': true,
                  'shadow-xs': true,
                  'transition ease-out duration-100': isMenuOpen,
                  'transition ease-in duration-75': !isMenuOpen
                }"
                role="menu"
                aria-orientation="vertical"
                aria-labelledby="user-menu"
              >
                <a
                  id="repo"
                  href="#"
                  class="block px-4 py-2 text-sm leading-5 text-gray-700 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 transition duration-150 ease-in-out"
                  role="menuitem"
                  >Repositories</a
                >
                <!--<a href="#" class="block px-4 py-2 text-sm leading-5 text-gray-700 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 transition duration-150 ease-in-out" role="menuitem">Settings</a>-->
                <a
                  id="sign"
                  href="#"
                  class="block px-4 py-2 text-sm leading-5 text-gray-700 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 transition duration-150 ease-in-out"
                  role="menuitem"
                  >Sign out</a
                >
              </div>
            </div>
          </div>
          <div class="p-48" v-else>
            <router-link 
              to="/login"
              class="bg-white hover:bg-gray-300 text-gray-800 font-semibold py-2 px-4 border border-gray-400 rounded shadow">
              Sign in
            </router-link>
            <router-link 
              to="/register"
              class="m-2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 border border-blue-700 rounded">
              Sign up
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <!--
    Mobile menu, toggle classes based on menu state.

    Menu open: "block", Menu closed: "hidden"
  -->
    <div 
    :class="{hidden: !isBarOpen, 'sm:hidden': !isBarOpen, 'sm:block': isBarOpen}">
      <div class="px-2 pt-2 pb-3">
        <router-link
          to="/"
          class="hover:underline ml-4 px-3 py-2 rounded-md text-sm font-medium leading-5 text-gray-300 hover:text-white hover:bg-gray-700 focus:outline-none focus:text-white transition duration-150 ease-in-out"
          >Home</router-link
        >
        <router-link
          to="/about"
          class="hover:underline ml-4 px-3 py-2 rounded-md text-sm font-medium leading-5 text-gray-300 hover:text-white hover:bg-gray-700 focus:outline-none focus:text-white transition duration-150 ease-in-out"
          >About</router-link
        >
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({})
export default class NavBar extends Vue {
  @Prop() private isLogged!: boolean;

  private isMenuOpen: boolean = false;
  private isBarOpen: boolean = false;

  private async mounted() {
    window.document.addEventListener('click', (ev: Event) => {
      if (ev.srcElement) {
        if ((ev.srcElement as HTMLBaseElement).id.includes('user-menu')) {
          this.isMenuOpen = !this.isMenuOpen;
          return;
        }

        this.isMenuOpen = false;
      }
    });
  }
}
</script>
