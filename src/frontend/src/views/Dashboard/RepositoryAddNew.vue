<template>
<div
    class="text-left flex-1 bg-gray-200 dark:bg-gray-900 overflow-y-auto transition
    duration-500 ease-in-out">
    <div
        class="px-24 py-12 text-gray-700 dark:text-gray-500 transition
        duration-500 ease-in-out">
        <h2 class="text-4xl font-medium capitalize">{{mainLabel}}</h2>
    </div>
    
</div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { IRepository, IRepositoryDatabase } from 'models/responses/IRepository';

@Component({
    components: {}
})
export default class RepositoryAddNew extends Vue {
    @Prop() private mainLabel!: string;
}
</script>