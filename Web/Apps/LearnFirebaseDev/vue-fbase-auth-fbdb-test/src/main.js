import Vue from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap/dist/css/bootstrap.min.css'
import * as firebase from 'firebase';

  // Your web app's Firebase configuration
  // check https://console.firebase.google.com/
  const firebaseConfig = {
    apiKey: "...",
    authDomain: "...",
    databaseURL: "....",
    projectId: "...",
    messagingSenderId: "..."
};

//   initialize FirebaseApp
const firebaseApp = firebase.initializeApp(firebaseConfig);
export const db = firebaseApp.firestore();


Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
