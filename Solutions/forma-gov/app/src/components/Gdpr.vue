<template>
  <div id="gdpr">
    <!-- TODO add gdpr compliance -->
    <div class="notification is-hidden" id="gdpr-notification">
      <button class="delete" @click="closeGdprNotification"></button>
      By using our platform you agree to our
      <a
        href="https://github.com/VasilisPlavos/LearnWEB/blob/master/app6-forma-gov/docs/policy.md"
        target="_blank"
      >use of cookies</a>.
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },

  mounted() {
    if (!localStorage.cookieConsent) {
      var s = document.getElementById("gdpr-notification");
      s.classList.remove("is-hidden");
      
    }
  },

  methods: {
    closeGdprNotification() {
      var s = document.getElementById("gdpr-notification");
      s.classList.add("is-hidden");
      localStorage.cookieConsent = Date();
    }
  }
};
</script>

<style>
#gdpr-notification {
  position: fixed;
  top: auto;
  right: 0;
  left: auto;
  bottom: 0;
}
</style>