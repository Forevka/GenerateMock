<template>
<div class="pt-16 font-family-karla h-screen">
    <div class="bg-gray-300 w-full flex flex-wrap">
        <!-- Login Section -->
        <div class="w-full md:w-1/2 flex flex-col">
            <div class="flex justify-center md:justify-start pt-12 md:pl-12 md:-mb-24">
                <a href="#" class="bg-black text-white font-bold text-xl p-4">Logo</a>
            </div>
            <div class="flex flex-col justify-start md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32">
                <p class="mt-6 text-center text-3xl text-gray-900">Welcome.</p>
                <div v-if="isError" class="bg-red-100 border-t-4 border-red-500 rounded-b text-red-900 px-4 py-3 shadow-md" role="alert">
                    <div class="flex justify-center">
                        <div>
                            <p class="font-bold text-center">{{errorBadgeText}}</p>
                            <p class="text-sm">Make sure your password and login are correct.</p>
                        </div>
                    </div>
                </div>
                <form class="flex flex-col pt-3 md:pt-8" onsubmit="event.preventDefault();">
                    <div class="flex flex-col pt-4">
                        <label for="email" class="text-lg">Login</label>
                        <input autocomplete="username" v-model="userLogin" id="login" placeholder="your@login" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline">
                    </div>
    
                    <div class="flex flex-col pt-4">
                        <label for="password" class="text-lg">Password</label>
                        <input autocomplete="current-password" v-model="userPassword" type="password" id="password" placeholder="Password" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline">
                    </div>
    
                    <button 
                        v-on:click="login"
                        class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm leading-5 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition duration-150 ease-in-out p-2 mt-8"
                    >Log In</button>
                </form>
                <div class="text-center pt-12 pb-12">
                    <p>Don't have an account? <router-link to="/register" class="underline font-semibold">Register here.</router-link></p>
                </div>
            </div>
        </div>
        <!-- Image Section -->
        <div class="w-1/2 shadow-2xl">
            <img class="object-cover w-full h-screen hidden md:block" src="https://images.unsplash.com/photo-1538291323976-37dcaafccb12?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&q=80">
        </div>
    </div>

</div>
</template>

<style lang="scss" scoped>
.h-screen {
    height: calc(100vh - 4rem);
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import apiClient from '../api/ApiClient';

@Component({})
export default class Login extends Vue {
    private userLogin: string = '';
    private userPassword: string = '';
    private errorBadgeText: string = '';

    private isError: boolean = false;

    private async created() {
        if (apiClient.IsLogged) {
            this.$router.push('/dashboard');
        }
    }

    private async login() {
        await apiClient.fetchToken(this.userLogin, this.userPassword).then((x: any) => {
            localStorage.setItem('login', x.data.login);
            apiClient.updateToken(x.data.access_token);
            apiClient.IsLogged = true;
            this.$router.push('/dashboard');
        }).catch((err: any) => {
            this.isError = true;
            this.errorBadgeText = err.response.data.Message;
        });
    }
}
</script>