<template>
  <div class="otherTests">
    <h1>otherTests {{m}}</h1>
    <p>{{ $t('key1') }}</p>
    <p>{{ $t('nested.message1') }}</p>
    <p>{{ $t('linkedString') }}</p>
    <p>{{ $t('errors[0]') }}</p>
    <p>{{ $t('errors[3]') }}</p>
    <p>{{ $t('errors[1].internal1') }}</p>
    <p>{{ $t('errors[2][0]') }}</p>
    <p>{{ $t(m) }}</p>

  </div>
</template>

<script>


export default {
  name: "OtherTests",
  data() {
      return {
          m: ''
      }
  },
  mounted(){
      this.m = 'errors[2][0]';
  }
};
</script>
