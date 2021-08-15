<!-- Here there is an example of how to use css on a .vue file -->


<template>
  <div class="hello">
    <div class="holder">
      <ul>
        <li v-for="(data, index) in skills" :key="index">{{ index }}. {{ data.skill }}</li>
      </ul>

      <p v-if="skills.length = 1">You have more than 1 skills</p>
      <p v-else>You have less than or equal to 1 skill </p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Skills',
  data() {
    return {
      skills: [
        { "skill": "Vuew.js"},
        { "skill": "Frontend Dev"},
      ]
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style src="./Skills.1.css" scoped>

</style>
