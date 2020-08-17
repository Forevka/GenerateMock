<template>
<div
    class="text-left flex-1 bg-gray-200 dark:bg-gray-900 overflow-y-auto transition
    duration-500 ease-in-out">
    <div
        class="px-24 py-12 text-gray-700 dark:text-gray-500 transition
        duration-500 ease-in-out">
        <h2 class="text-4xl font-medium capitalize">{{mainLabel}}</h2>
        <div class="mt-1 mb-4 flex items-center justify-between">
            <span class="text-sm">
                Full count
                <strong>{{repositories.length}}</strong>
            </span>

            <div class="flex items-center select-none">
                <form class="w-full max-w-sm">
                    <div class="flex items-center border-b border-blue-500 py-2">
                        <span class="hover:text-pink-500 cursor-pointer mr-3">
                            <svg viewBox="0 0 512 512" class="h-5 w-5 fill-current">
                                <path
                                    d="M505 442.7L405.3
                                    343c-4.5-4.5-10.6-7-17-7H372c27.6-35.3 44-79.7
                                    44-128C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1
                                    208 208 208c48.3 0 92.7-16.4 128-44v16.3c0 6.4
                                    2.5 12.5 7 17l99.7 99.7c9.4 9.4 24.6 9.4 33.9
                                    0l28.3-28.3c9.4-9.4 9.4-24.6.1-34zM208 336c-70.7
                                    0-128-57.2-128-128 0-70.7 57.2-128 128-128 70.7 0
                                    128 57.2 128 128 0 70.7-57.2 128-128 128z"></path>
                            </svg>
                        </span>
                        <input 
                            class="appearance-none bg-transparent border-none w-full text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none" 
                            type="text" placeholder="BooksApi" aria-label="repo-name"
                            v-model="search">
                        <button class="flex-shrink-0 bg-blue-500 hover:bg-blue-700 border-blue-500 hover:border-blue-700 text-sm border-4 text-white py-1 px-2 rounded" type="button">
                            Search
                        </button>
                    </div>
                </form>
            </div>

            <!--<button
                class="flex items-center focus:outline-none border
                rounded-full py-2 px-6 leading-none border-gray-500
                select-none hover:text-pink-600 hover:bg-pink-300">
                <svg class="h-5 w-5 fill-current mr-1" viewBox="0 0 24 24">
                    <path
                        d="M12 1L8 5h3v9h2V5h3m2 18H6a2 2 0 01-2-2V9a2 2 0
                        012-2h3v2H6v12h12V9h-3V7h3a2 2 0 012 2v12a2 2 0 01-2
                        2z"></path>
                </svg>
                <span>Export</span>
            </button>-->

            <!--<div class="flex items-center select-none">
                <span>Filter</span>
                <button
                    class="ml-3 bg-gray-400 dark:bg-gray-600
                    dark:text-gray-400 rounded-full p-2 focus:outline-none
                    hover:text-green-500 hover:bg-green-300 transition
                    duration-200 ease-in-out">
                    <svg class="h-5 w-5 fill-current" viewBox="0 0 24 24">
                        <path
                            d="M14 12v7.88c.04.3-.06.62-.29.83a.996.996 0
                            01-1.41 0l-2.01-2.01a.989.989 0
                            01-.29-.83V12h-.03L4.21 4.62a1 1 0
                            01.17-1.4c.19-.14.4-.22.62-.22h14c.22 0
                            .43.08.62.22a1 1 0 01.17 1.4L14.03 12H14z"></path>
                    </svg>
                </button>
                <button
                    disabled
                    class="ml-2 bg-gray-400 dark:bg-gray-600
                    dark:text-gray-400 rounded-full p-2 focus:outline-none
                    hover:text-green-500 hover:bg-green-300 transition
                    duration-200 ease-in-out">
                    <svg class="h-5 w-5 fill-current" viewBox="0 0 24 24">
                        <path
                            d="M18 21l-4-4h3V7h-3l4-4 4 4h-3v10h3M2
                            19v-2h10v2M2 13v-2h7v2M2 7V5h4v2H2z"></path>
                    </svg>
                </button>
                <button
                    class="ml-2 bg-gray-400 dark:bg-gray-600
                    dark:text-gray-400 rounded-full p-2 focus:outline-none
                    hover:text-green-500 hover:bg-green-300 transition
                    duration-200 ease-in-out">
                    <svg class="h-5 w-5 fill-current" viewBox="0 0 24 24">
                        <path
                            d="M12 4a4 4 0 014 4 4 4 0 01-4 4 4 4 0 01-4-4 4
                            4 0 014-4m0 10c4.42 0 8 1.79 8 4v2H4v-2c0-2.21
                            3.58-4 8-4z"></path>
                    </svg>
                </button>
            </div>-->
        </div>

        <div
            class="border dark:border-gray-700 transition duration-500
            ease-in-out"></div>
        <div class="flex flex-col mt-2">
            <repository-card 
                v-for="repo in filteredRepositories" 
                :key="repo.repositoryId" 
                :repo="repo"
                :ownerName="myName()"/>
        </div>
    </div>
</div>
</template>


<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import RepositoryCard from '@/components/Dashboard/RepositoryCard.vue';
import { IRepository, IRepositoryDatabase } from '@/models/responses/IRepository';

@Component({
    components: {
        RepositoryCard,
    }
})
export default class RepositoriesDisplay extends Vue {
    @Prop() private mainLabel!: string;

    private search: string = '';

    private repositories: IRepository[] = [
        {
            'repositoryId': 'a935176e-460e-41a0-a268-a37439602ff1',
            'repositoryName': 'demo',
            'ownerId': 'ae8d52a3-3ec2-48c6-9268-9f63dda271ec',
            'repositoryDatabase': [
                {
                    'repositoryId': 'a935176e-460e-41a0-a268-a37439602ff1',
                    'databaseId': '3480ef2d-8052-4154-bdc9-0b5dc28c8147',
                    'databaseFilePath': 'db.json',
                    'databaseLoadTime': new Date('2020-08-10T20:04:39.510178'),
                    'databaseVersion': 1,
                    'databaseApiUrl': 'forevka/demo/v1/db'
                }
            ]
        },
        {
            'repositoryId': 'a935176e-460e-41a0-a268-a37439602ff2',
            'repositoryName': 'demoTest',
            'ownerId': 'ae8d52a3-3ec2-48c6-9268-9f63dda271ec',
            'repositoryDatabase': [
                {
                    'repositoryId': 'a935176e-460e-41a0-a268-a37439602ff1',
                    'databaseId': '3480ef2d-8052-4154-bdc9-0b5dc28c8147',
                    'databaseFilePath': 'db.json',
                    'databaseLoadTime': new Date('2020-07-10T20:04:39.510178'),
                    'databaseVersion': 1,
                    'databaseApiUrl': 'forevka/demo/v1/db'
                },
                {
                    'repositoryId': 'a935176e-460e-41a0-a268-a37439602ff1',
                    'databaseId': '3480ef2d-8052-4154-bdc9-0b5dc28c8147',
                    'databaseFilePath': 'db.json',
                    'databaseLoadTime': new Date('2020-07-10T20:04:39.510178'),
                    'databaseVersion': 2,
                    'databaseApiUrl': 'forevka/demo/v2/db'
                }
            ]
        },
    ]

    private get filteredRepositories(): IRepository[] {
        return this.repositories.filter((x: IRepository) =>
            x.repositoryName.toLowerCase().includes(this.search.toLowerCase()));
    }

    private created(): void {
        this.prepareData();
    }

    private prepareData(): void {
        for (let i = 0; i < this.repositories.length; i++) {
            this.sortDb(this.repositories[i]);
        }
    }

    private myName(): string|null {
        return localStorage.getItem('login');
    }

    private sortDb(repo: IRepository): void {
        const sortedDb: IRepositoryDatabase[] = repo.repositoryDatabase.sort((n1, n2) => {
            if (n1.databaseVersion > n2.databaseVersion) {
                return -1;
            }

            if (n1.databaseVersion < n2.databaseVersion) {
                return 1;
            }

            return 0;
        });

        repo.repositoryDatabase = sortedDb;
    }
}
</script>