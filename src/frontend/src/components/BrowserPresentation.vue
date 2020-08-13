<template>
    <div class="terminal-window">
    <header>
        <div class="button green"></div>
        <div class="button yellow"></div>
        <div class="button red"></div>
    </header>
    <section class="url">
        <div class="host"><b>{{urlHost}}</b>{{urlQuery}}</div>
    </section>
    <section class="body" id="browserbody">
        <pre><code class="language-javascript">{{code}}</code></pre>
    </section>
</div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class BrowserPresentation extends Vue {
    @Prop() private code!: string;
    @Prop() private urlHost!: string;
    @Prop() private urlQuery!: string;

    async mounted() {
      // @ts-ignore
      Prism.highlightAll()
    }
}
</script>

<style lang="scss" scoped>

.terminal-window {
  text-align: left;
  width: 32rem;
  height: 22.5rem;
  border-radius: .625rem;
}

.terminal-window header {
  background: #282a36;
  height: 1.875rem;
  border-radius: .5rem .5rem 0 0;
  padding-left: .625rem;
}

.terminal-window .url {
  font-family: Roboto, "Helvetica Neue", Helvetica, Arial, sans-serif;
  font-size: 16px;
  background: #282a36;    
  height: auto;
  padding: 5px 10px;
  -webkit-box-direction: normal;
  color: #282a36;
  box-sizing: inherit;
}
.host {
  border-radius: .5rem .5rem .5rem .5rem;
  width: auto;
  padding: 5px 10px 5px 10px;
  background: #f7f7f7;
}

.body {
  background: #282a36;
  height: 18.5rem;
  border-radius: .0rem .0rem .5rem .5rem;
  padding-left: .625rem;
}


.terminal-window header .button {
  width: .75rem;
  height: .75rem;
  margin: .625rem .25rem 0 0;
  display: inline-block;
  border-radius: .5rem;
}

.terminal-window header .button.green {
  background: #3BB662;
}

.terminal-window header .button.yellow {
  background: #E5C30F;
}

.terminal-window header .button.red {
  background: #E75448;
}

pre[class*=language-] {
    background: rgba(0, 0, 0, 0.0);
    margin: 0px;
    padding: 1em; 
    overflow: auto;
    border: 0em; 
    border-radius: 0em;
    box-shadow: none;
}

code[class*=language-] {
    text-shadow: 0 0 0;
}
</style>