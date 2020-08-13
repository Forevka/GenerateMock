<template>
    <sidebar/>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import Sidebar from '@/components/Dashboard/Sidebar.vue';

@Component({
    components: {
        Sidebar,
    }
})
export default class Dashboard extends Vue {
    private async mounted() {
        
    }
}
</script>