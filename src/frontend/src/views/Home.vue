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
  <deckgo-highlight-code>
    <code slot="code">console.log('Hello World');</code>
  </deckgo-highlight-code>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue';


@Component({
  components: {
    HelloWorld,
  },
})
export default class Home extends Vue {
}
</script>
