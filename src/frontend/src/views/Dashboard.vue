<template>
    <sidebar/>
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
        const me = await apiClient.getMe();
        console.log(me)
        if (!me) {
            this.$router.push('/login')
        }
    }
}
</script>