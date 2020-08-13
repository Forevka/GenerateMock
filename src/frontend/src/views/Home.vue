<template>
  <div class="home-view">
    <div class="px-4 py-10">
    <div class="relative w-full md:max-w-2xl md:mx-auto text-center">
      <h1
        class="font-bold text-gray-700 text-xl sm:text-2xl md:text-5xl leading-tight mb-6"
      >
        Json server
      </h1>

      <p class="text-gray-600 md:text-xl md:px-18">
        Fake mocked API servers for free use!
      </p>
    </div>
  </div>
  <div class="flex justify-center space-x-16 m-4">
    <browser-presentation :urlHost="'github.com'" :urlQuery="'/user/repo/branch/db.json'" :code="gCode"/>
    <arrow class="self-center" />
    <browser-presentation :urlHost="'mysite.com'" :urlQuery="'/user/repo/v1/db/posts'" :code="rCode"/>
  </div>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import BrowserPresentation from '@/components/BrowserPresentation.vue';
import Arrow from '@/components/Arrow.vue';

@Component({
  components: {
    BrowserPresentation,
    Arrow,
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
}`
  private rCode: string = `{
  "id": 1,
  "title": "hello"
}`
}
</script>

<style lang="scss" scoped>

</style>