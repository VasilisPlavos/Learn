const app = new Vue({
    el: '#app',
    data: {
      cats: [],
      newCat: null
    },
    mounted() {
      if (localStorage.getItem('cats')) {
        try {
          this.cats = JSON.parse(localStorage.getItem('cats'));
        } catch(e) {
          localStorage.removeItem('cats');
        }
      }
    },
    methods: {
      addCat() {
        // ensure they actually typed something
        if (!this.newCat) {
          return;
        }
  
        this.cats.push(this.newCat);
        this.newCat = '';
        this.saveCats();
      },
      removeCat(x) {
        this.cats.splice(x, 1);
        this.saveCats();
      },
      saveCats() {
        const parsed = JSON.stringify(this.cats);
        localStorage.setItem('cats', parsed);
      }
    }
  })