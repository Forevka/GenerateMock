<template>
  <div id="app">
    <nav-bar :isLogged="isLogged()" />
    
    <router-view/>
  </div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import NavBar from '@/components/NavBar.vue';
import apiClient from '@/api/ApiClient';

@Component({
  components: {
    NavBar,
  },
})
export default class App extends Vue {
  public isLogged(): boolean {
    return apiClient.IsLogged;
  }

  private async mounted() {
    await apiClient.renewToken();
  }
}
</script>

<style lang="scss">
@import "tailwindcss/tailwind";
</style>

<style>
html, body {
  background: #fff;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
