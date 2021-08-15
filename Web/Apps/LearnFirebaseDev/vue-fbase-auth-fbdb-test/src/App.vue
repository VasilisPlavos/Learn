<template>
  <div>
    <nav class="navbar navbar-dark bg-dark justify-content-between flex-nowrap flex-row">
      <div class="container">
        <a class="navbar-brand float-left">Firebase Vue CRUD Example</a>
        <ul class="nav navbar-nav flex-row float-right">
          <li class="nav-item">
            <router-link class="nav-link pr-3" to="/">Add User</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link pr-3" to="/list">View Users</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link pr-3" to="/login">Sign in</router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link pr-3" to="/signup">Sign up</router-link>
          </li>
        </ul>
      </div>
    </nav>
    <p></p>
    <p>
      <a href="https://www.positronx.io/vue-js-firebase-build-vue-crud-app-with-cloud-firestore/">
        Tutorial 1: Vue.js 2 + Firebase: Build Vue CRUD App with Cloud
        Firestore
      </a>
    </p>
    <p>
      <a
        href="https://www.positronx.io/vue-js-firebase-authentication-tutorial-example/"
      >Tutorial 2: Vue.js 2 + Firebase Authentication Tutorial Example</a>
    </p>

    <div class="container mt-5">
      <router-view></router-view>
    </div>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
