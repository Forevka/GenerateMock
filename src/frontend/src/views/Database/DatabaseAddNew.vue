<template>
    <form class="mt-1 mb-4 flex items-left justify-between flex-col">
        <fieldset :disabled="locked">
            <div class="mb-4">
                <label class="block text-gray-700 text-sm font-bold mb-2" for="username">
                    Database file path
                </label>
                <input 
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                    id="label" type="text" 
                    placeholder="Db with kitties">
            </div>
            <div class="mb-4">
                <label class="block text-gray-700 text-sm font-bold mb-2" for="username">
                    Database branch
                </label>
                <input 
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                    id="label" type="text" 
                    placeholder="master">
            </div>
            <div class="mb-4">
                <label class="block text-gray-700 text-sm font-bold mb-2" for="username">
                    Database label
                </label>
                <input 
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                    id="label" type="text" 
                    placeholder="Db with kitties">
            </div>
        </fieldset>
    </form>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { IRepository, IRepositoryDatabase } from '@/models/responses/IRepository';

@Component({
    components: {},
})
export default class RepositoryAddNew extends Vue {
    @Prop() private repo!: IRepository;
    @Prop() private locked!: boolean;
}
</script>