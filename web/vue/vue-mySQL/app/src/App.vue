<template>
  <div id="app">
    <h1>hello</h1>
    <p>{{ message }}</p>
  </div>
</template>

<script>
var m = Date();

export default {
  name: "App",
  data() {
    return {
      message: m
    };
  }
};
</script>

<style>
</style>