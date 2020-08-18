<template>
    <form class="mt-1 mb-4 flex items-left justify-between flex-col">
        <h2 class="text-4xl font-medium capitalize">{{mainLabel}}</h2>
        <fieldset :disabled="locked">
            <div class="mb-4">
                <label 
                    class="block text-gray-700 text-sm font-bold mb-2" 
                    for="filePath">
                    Database file path
                </label>
                <input 
                    v-model="newDbForm.dbFilePath"
                    :class="{
                        'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline': true, 
                        'border-red-400': newDbValidation.isDbFilePathValid,
                    }" 
                    id="filePath" type="text" 
                    placeholder="db.json">
                <label 
                    v-if="newDbValidation.isDbFilePathValid"
                    class="block text-gray-700 text-s ml-2 text-xs text-red-500"
                    for="username">
                    Please provide username 
                </label>
            </div>
            <div class="mb-4">
                <label 
                    class="block text-gray-700 text-sm font-bold mb-2"
                    for="fileBranch">
                    Database branch
                </label>
                <input 
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                    id="fileBranch" type="text" 
                    placeholder="master">
            </div>
            <div class="mb-4">
                <label 
                    class="block text-gray-700 text-sm font-bold mb-2"
                    for="dbLabel">
                    Database label
                </label>
                <input 
                    class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                    id="dbLabel" type="text" 
                    placeholder="Db with kitties">
            </div>
        </fieldset>
        <div class="mt-4 mb-4">
        <button 
            v-on:click="addNewDb" type="button" 
            :class="{
                'inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition ease-in-out duration-150': !locked, 
                'inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium rounded-md text-white bg-indigo-300': locked,
                'cursor-not-allowed': isAddingDb || locked}"
            :disabled="isAddingDb || locked">
            <svg v-if="isAddingDb" class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Add database
        </button>
        </div>
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

    private isAddingDb: boolean = false;

    private mainLabel: string = 'Add database';

    private newDbValidation = {
        isDbFilePathValid: false,
    }

    private newDbForm = {
        dbFilePath: '',
        branch: '',
        dbLabel: '',
    }

    private addNewDb(): void {
        this.newDbValidation.isDbFilePathValid = false;

        if (this.newDbForm.dbFilePath === '') {
            this.newDbValidation.isDbFilePathValid = true;
        }

        if (this.newDbValidation.isDbFilePathValid) {
            return;
        }

        this.isAddingDb = true;
        setTimeout(() => {
            this.isAddingDb = false;
        }, 1000);
    }
}
</script>