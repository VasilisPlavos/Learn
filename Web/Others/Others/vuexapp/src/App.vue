<template>
  <div id="app">
    <p>{{ count }}</p>
    <p>{{ countAlias }}</p>
    <p>{{ countPlusLocalState }}</p>
  </div>
</template>

<script>
import Vuex from "vuex";
import Vue from "vue";
Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    count: 3,
  },
});

import { mapState } from "vuex";

export default {
  name: "App",
  store,
  computed: mapState({
    count: (state) => state.count,
    countAlias: "count",
    countPlusLocalState (state){
      return state.count + this.localCount
    }
  }),
  // computed: mapState([
  //   'count'
  // ]),
  data() {
    return {
      localCount: 4,
    };
  },
};
</script>

<style>
</style>
