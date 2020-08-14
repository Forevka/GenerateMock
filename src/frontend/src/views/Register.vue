<template>
<div class="pt-16 font-family-karla h-screen">
<div class="w-full bg-gray-300 flex flex-wrap">
    <!-- Register Section -->
    <div class="w-full md:w-1/2 flex flex-col">

        <div class="flex justify-center md:justify-start pt-12 md:pl-12 md:-mb-12">
            <a href="#" class="bg-black text-white font-bold text-xl p-4">Logo</a>
        </div>

        <div class="flex flex-col justify-center md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32">
            <p class="mt-6 text-center text-3xl text-gray-900">Join Us.</p>
            <div v-if="isError" class="bg-red-100 border-t-4 border-red-500 rounded-b text-red-900 px-4 py-3 shadow-md" role="alert">
                <div class="flex justify-center">
                    <div>
                        <p v-html="errorBadgeText" class="font-bold text-left"></p>
                    </div>
                </div>
            </div>
            <div v-if="isWarning" class="bg-orange-100 border-t-4 border-orange-500 rounded-b text-orange-900 px-4 py-3 shadow-md" role="alert">
                <div class="flex justify-center">
                    <div>
                        <p v-html="warningBadgeText" class="font-bold text-left"></p>
                    </div>
                </div>
            </div>
            <form class="flex flex-col pt-3 md:pt-8" onsubmit="event.preventDefault();">
                <div class="flex flex-col pt-4">
                    <label for="login" class="text-lg">Login</label>
                    <input autocomplete="username" v-model="userLogin" required type="login" id="login" placeholder="your@login" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline">
                </div>

                <div class="flex flex-col pt-4">
                    <label for="password" class="text-lg">Password</label>
                    <input autocomplete="new-password" v-model="userPassword" required type="password" id="password" placeholder="Password"
                    v-on:click="isPasswordNotSame = false" 
                    :class="{
                        'border-red-500': isPasswordNotSame, 
                        'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline': true
                        }">
                </div>

                <div class="flex flex-col pt-4">
                    <label for="confirm-password" class="text-lg">Confirm Password</label>
                    <input autocomplete="new-password" v-model="userPasswordConfirm" required type="password" id="confirm-password" placeholder="Password" 
                    v-on:click="isPasswordNotSame = false" 
                    :class="{
                        'border-red-500': isPasswordNotSame, 
                        'shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline': true
                        }">
                </div>

                <button 
                    type="submit"
                    v-on:click="register"
                    class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm leading-5 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition duration-150 ease-in-out p-2 mt-8"
                    >Register</button>
            </form>
            <div class="text-center pt-12 pb-12">
                <p>Already have an account? <router-link to="/login" class="underline font-semibold">Log in here.</router-link></p>
            </div>
        </div>

    </div>

    <!-- Image Section -->
    <div class="w-1/2 shadow-2xl">
        <img class="object-cover w-full h-screen hidden md:block" src="https://images.unsplash.com/photo-1546285290-d98760f95d73?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&q=80">
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
export default class Register extends Vue {
    private userLogin: string = '';
    private userPassword: string = '';
    private userPasswordConfirm: string = '';
    private errorBadgeText: string = '';
    private warningBadgeText: string = '';

    private isError: boolean = false;
    private isWarning: boolean = false;
    private isPasswordNotSame: boolean = true;

    private async created() {
        if (apiClient.IsLogged) {
            this.$router.push('/dashboard');
        }
    }

    private async register() {
        if (this.userLogin === '' || this.userPassword === '' || this.userPasswordConfirm === '') {return; }

        if (this.userPassword !== this.userPasswordConfirm) {
            this.isWarning = true;
            this.warningBadgeText = 'Passwords must be the same'
            this.isPasswordNotSame = true;
            return;
        }
        this.isWarning = false;
        this.isPasswordNotSame = false;

        await apiClient.registerUser(this.userLogin, this.userPassword).then((x: any) => {
            localStorage.setItem('login', x.data.login);
            
            apiClient.updateToken(x.data.access_token);
            apiClient.IsLogged = true;

            this.$router.push('/dashboard');
        }).catch((err: any) => {
            this.isError = true;
            this.errorBadgeText = err.response.data.Message.replace(/(\r\n|\n|\r)/gm, '<br />');
        });
    }
}
</script>