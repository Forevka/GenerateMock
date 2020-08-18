<template>
<div
    class="text-left flex-1 bg-gray-200 dark:bg-gray-900 overflow-y-auto transition
    duration-500 ease-in-out">
    <div
        class="px-24 py-12 text-gray-700 dark:text-gray-500 transition
        duration-500 ease-in-out">
        <h2 class="text-4xl font-medium capitalize">{{mainLabel}}</h2>
        <form>
        <fieldset :disabled="isAddedNewRepo">
            <div class="mt-1 mb-4 flex items-left justify-between flex-col">
                <div class="mb-4">
                    <label 
                        class="block text-gray-700 text-sm font-bold mb-2"
                        for="username">
                        Username
                    </label>
                    <input
                        v-model="newRepoForm.userName"
                        :class="{
                            'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline': true, 
                            'border-red-400': newRepoValidation.isUsernameValid,
                        }" 
                        id="username" type="text" placeholder="linus">
                    <label 
                        v-if="newRepoValidation.isUsernameValid"
                        class="block text-gray-700 text-s ml-2 text-xs text-red-500"
                        for="username">
                        Please provide username 
                    </label>
                </div>
                <div class="mb-4">
                    <label 
                        class="block text-gray-700 text-sm font-bold mb-2" 
                        for="repoName">
                        Repository name
                    </label>
                    <input 
                        v-model="newRepoForm.repoName"
                        :class="{
                            'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline': true, 
                            'border-red-400': newRepoValidation.isReponameValid,
                        }" 
                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                        id="repoName" type="text" placeholder="FilmsDB">
                    <label 
                        v-if="newRepoValidation.isReponameValid"
                        class="block text-gray-700 text-s ml-2 text-xs text-red-500"
                        for="username">
                        Please provide repository name 
                    </label>
                </div>
                <div class="mb-4">
                    <label 
                        class="block text-gray-700 text-sm font-bold mb-2"
                        for="repoLabel">
                        Repository label 
                    </label>
                    <input 
                        class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                        id="repoLabel" type="text" placeholder="My first mocked api">
                </div>
            </div>
            <div class="mt-1 mb-4">
                <button 
                    v-on:click="addNewRepo" type="button" 
                    :class="{
                        'inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition ease-in-out duration-150': !isAddedNewRepo,
                        'inline-flex items-center px-4 py-2 border border-transparent text-base leading-6 font-medium rounded-md text-white bg-indigo-300': isAddedNewRepo, 
                        'cursor-not-allowed': isAddingNewRepo || isAddedNewRepo}"
                    :disabled="isAddingNewRepo || isAddedNewRepo">
                <svg v-if="isAddingNewRepo" class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                    <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                    <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                </svg>
                    Add repository
                </button>
            </div>
        </fieldset>
        </form>
        <div
            class="border dark:border-gray-700 transition duration-500
            ease-in-out">
        </div>
        <database-add-new class="pt-2" :locked="!isAddedNewRepo"/>
    </div>

</div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { IRepository, IRepositoryDatabase } from '@/models/responses/IRepository';
import DatabaseAddNew from '@/views/Database/DatabaseAddNew.vue';
import { ICreateRepository } from '@/models/requests/ICreateRepository';
import RepositoryService from '@/api/RepositoryService';

@Component({
    components: {
        DatabaseAddNew,
    },
})
export default class RepositoryAddNew extends Vue {
    @Prop() private mainLabel!: string;
    private isAddingNewRepo: boolean = false;
    private isAddedNewRepo: boolean = false;

    private repoService = new RepositoryService();

    private newRepoValidation = {
        isUsernameValid: false,
        isReponameValid: false,
    }

    private newRepoForm: ICreateRepository = {
        userName: '',
        repoName: '',
        label: '',
    }

    private async addNewRepo(): Promise<void> {
        console.log(this.newRepoForm)
        this.newRepoValidation.isUsernameValid = false;
        this.newRepoValidation.isReponameValid = false;

        if (this.newRepoForm.userName === '') {
            this.newRepoValidation.isUsernameValid = true;
        }

        if (this.newRepoForm.repoName === '') {
            this.newRepoValidation.isReponameValid = true;
        }

        if (this.newRepoValidation.isUsernameValid || this.newRepoValidation.isReponameValid) {
            return;
        }

        this.isAddingNewRepo = true;
        /*setTimeout(() => {
            this.isAddingNewRepo = false;
            this.isAddedNewRepo = true;
        }, 1000);*/
        await this.repoService.createRepo(this.newRepoForm).then(x => {
            this.isAddingNewRepo = false;
            this.isAddedNewRepo = true;
        })
    }
}
</script>