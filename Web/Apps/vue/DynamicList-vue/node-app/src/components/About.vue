<template>
    <div class="about">
    <h1 v-if="their_name"> This is about page, {{ their_name }} </h1>
    <h1 v-if="!their_name"> This is about page </h1>
    
    <p v-if="!their_name"> Try to go at /about/[enter name here] </p>
    <p v-if="their_name"> This is a paragraph </p>
    </div>
</template>


<script>
export default {
  name: 'About',
  data() {
    return {
        their_name: this.$route.params.name

    }
  },

}
</script>
