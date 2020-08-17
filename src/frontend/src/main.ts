import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';

// @ts-ignore
import VueTailwind from 'vue-tailwind';
/*import '@deckdeckgo/highlight-code';
import { defineCustomElements as deckDeckGoElement } from '@deckdeckgo/highlight-code/dist/loader';
deckDeckGoElement();*/

// @ts-ignore
import 'prismjs';
import 'prismjs/themes/prism-tomorrow.css';

import dateTimeFilter from './utilities/filters/DateTime';

const theme = {};

Vue.use(VueTailwind, theme);

Vue.config.productionTip = false;

dateTimeFilter(Vue);

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
