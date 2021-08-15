<template>
  <div class="hello">
    <LocaleSwitcher />
    <h1>{{ msg }}</h1>
    <p>{{ $t('main.welcome', {company: 'Lokalise'}) }}</p>
    <p>{{ $t('message') }}</p>
    <p>{{ $tc('main.new_message', 4) }}</p>
    <p>{{ $tc('banana', 1) }}</p>
    <p>
      Source:
      <a href="https://www.youtube.com/watch?v=rRhBNfn8dEs"
      >https://www.youtube.com/watch?v=rRhBNfn8dEs</a>
    </p>
    <p>
      Plugin page:
      <a href="https://kazupon.github.io/vue-i18n/">kazupon.github.io/vue-i18n/</a>
    </p>
  </div>
</template>

<script>
import LocaleSwitcher from "./LocaleSwitcher.vue";

export default {
  name: "HelloWorld",
  components: {
    LocaleSwitcher,
  },
  props: {
    msg: String,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
