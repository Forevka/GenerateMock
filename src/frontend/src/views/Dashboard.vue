<template>
    <div class="dashboard-view flex flex-row">
        <sidebar/>
        <!--<repositories-display 
            mainLabel="My repositories"
            class="mt-12"/>-->
        <router-view class="mt-12"></router-view>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Sidebar from '@/components/Dashboard/Sidebar.vue';
import apiClient from '../api/ApiClient';

@Component({
    components: {
        Sidebar,
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