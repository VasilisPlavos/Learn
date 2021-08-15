<template>
  <div>
    <button v-for="locale in locales" :key="locale" @click="switchLocale(locale)">{{locale}}</button>
  </div>
</template>

<script>
export default {
  name: "LocaleSwitcher",
  methods: {
    switchLocale(locale) {
      if (this.$i18n.locale !== locale) {
        this.$i18n.locale = locale;
      }

      console.log(`selected ${locale}, ${this.$t('main.welcome', {company: 'xiaomi'})} `);
    },
  },
  data() {
    return {
      locales: process.env.VUE_APP_I18N_SUPPORTED_LOCALE.split(","),
    };
  },
};
</script>

<style>
</style>