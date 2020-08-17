<template>
    <div class="dashboard-view flex flex-row">
        <sidebar/>
        <repositories-display 
            mainLabel="My repositories"
            class="mt-12"/>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Sidebar from '@/components/Dashboard/Sidebar.vue';
import RepositoriesDisplay from '@/components/Dashboard/RepositoriesDisplay.vue';
import apiClient from '../api/ApiClient';

@Component({
    components: {
        Sidebar,
        RepositoriesDisplay,
    }
})
export default class Dashboard extends Vue {
    private async mounted() {
        if (apiClient.IsLogged === false) {
            this.$router.push('/')
        }
    }
}
</script>