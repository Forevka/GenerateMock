<template>
  <div class="flex flex-col pt-16 home-view h-screen">
    <div class="presentation-view">
      <div class="px-4 py-10">
        <div class="relative w-full md:max-w-2xl md:mx-auto text-center">
          <h1
            class="font-bold text-purple-800 text-xl sm:text-2xl md:text-5xl leading-tight mb-6"
          >
            Json server
          </h1>

          <p class="text-gray-600 md:text-xl md:px-18">
            Fake mocked API servers for free use!
          </p>
        </div>
      </div>
      <div class="h-half-screen">
        <div class="flex justify-center space-x-16 m-4">
          <browser-presentation :urlHost="'github.com'" :urlQuery="'/user/repo/branch/db.json'" :code="gCode"/>
          <arrow class="self-center" />
          <browser-presentation :urlHost="'mysite.com'" :urlQuery="'/user/repo/v1/db/posts'" :code="rCode"/>
        </div>
        <div class="flex justify-center get-started-button">
          <button 
            class="w-48 h-8 self-center group relative flex justify-center py-2 px-4 border border-transparent text-sm leading-5 font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition duration-150 ease-in-out p-2 mt-8"
          >Get started!</button>
        </div>
      </div>
    </div>
    <home-faq/>
    <home-stat/>
    <home-footer/>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import BrowserPresentation from '@/components/BrowserPresentation.vue';
import HomeFaq from '@/components/HomeFAQ.vue';
import HomeStat from '@/components/HomeStatistic.vue';
import HomeFooter from '@/components/HomeFooter.vue';
import Arrow from '@/components/Arrow.vue';
import apiClient from '../api/ApiClient';

@Component({
  components: {
    BrowserPresentation,
    Arrow,
    HomeFaq,
    HomeStat,
    HomeFooter,
  },
})
export default class Home extends Vue {
  private gCode: string = `{
  "posts": [
    {
      "id": 1,
      "title": "hello"
    }
  ],
  "profile": {
    "name": "typicode"
  }
}`;
  private rCode: string = `{
  "id": 1,
  "title": "hello"
}`;

  private async created() {
    // await apiClient.getMe();
  }
}
</script>

<style lang="scss" scoped>

.get-started-button {
  button {
    padding: 0.3rem;
  }
}

</style>