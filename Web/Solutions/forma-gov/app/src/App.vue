<template>
  <div id="app">
    <router-view/>
    <Footer/>
    <Gdpr/>
  </div>
</template>

<script>
import Footer from './components/Footer.vue'
import Gdpr from './components/Gdpr.vue'

export default {
  name: 'App',
  components: {
    Footer,
    Gdpr
  }
}
</script>
