<template>
  <div id="home">
    <section class="section">
      <div class="notification is-danger is-hidden" id="pleaseEnterFullnameAndAddress">
        <button class="delete" @click="closeBulmaNotification"></button>
        Εισάγετε
        <b>ονοματεπώνυμο</b> και
        <b>διεύθυνση κατοικίας</b>.
      </div>

      <div class="container">
        <h1 class="title is-4 has-icons-left">
          <span class="icon is-left">
            <img
              src="https://www.gov.gr/gov_logos-08_16x16_favicon.ico"
              alt="Forma Gov App"
              width="16"
              height="16"
            />
          </span>Forma Gov App
        </h1>
        <h2 class="subtitle">Στείλτε εύκολα και γρήγορα το SMS.</h2>
        <!-- sms:+15552345678?body=Hello%20World -->
        <form>
          <div class="field">
            <label class="label" for="fullname">Ονοματεπώνυμο</label>
            <div class="control has-icons-left">
              <input
                class="input"
                v-model="fullname"
                type="text"
                placeholder="Εισάγετε ονοματεπώνυμο"
                value="fullname"
                required
              />
              <span class="icon is-small is-left">
                <i class="fas fa-user"></i>
              </span>
            </div>
          </div>
          <div class="field">
            <label class="label" for="address">Διεύθυνση κατοικίας</label>
            <div class="control has-icons-left">
              <input
                class="input"
                v-model="address"
                type="text"
                placeholder="Εισάγετε διεύθυνση κατοικίας"
                value="address"
                required
              />
              <span class="icon is-small is-left">
                <i class="fas fa-home"></i>
              </span>
            </div>
          </div>
          <div class="field">
            <label class="label" for="exitReason">
              Λόγος εξόδου
              <!-- <span class="icon has-text-info"><i class="fas fa-info-circle" id="exitReasonModal"></i></span> -->
            </label>
            <div class="control">
              <div class="select">
                <select v-model="selectedExitReason">
                  <option
                    v-for="option in exitReasonsToSelect"
                    :key="option.text"
                    v-bind:value="option.value"
                  >{{ option.text }}</option>
                </select>
                <!-- 
                  <option>1. Μετάβαση σε φαρμακείο ή επίσκεψη στον γιατρό, εφόσον αυτό συνιστάται μετά από σχετική επικοινωνία.</option>
                  <option>2. Μετάβαση σε εν λειτουργία κατάστημα προμηθειών αγαθών πρώτης ανάγκης (σούπερ μάρκετ, μίνι μάρκετ), όπου δεν είναι δυνατή η αποστολή τους.</option>
                  <option>3. Μετάβαση στην τράπεζα, στο μέτρο που δεν είναι δυνατή η ηλεκτρονική συναλλαγή.</option>
                  <option>4. Κίνηση για παροχή βοήθειας σε ανθρώπους που βρίσκονται σε ανάγκη.</option>
                  <option>5. Μετάβαση σε τελετή (π.χ. κηδεία, γάμος, βάφτιση) υπό τους όρους που προβλέπει ο νόμος ή μετάβαση διαζευγμένων γονέων ή γονέων που τελούν σε διάσταση που είναι αναγκαία για τη διασφάλιση της επικοινωνίας γονέων και τέκνων, σύμφωνα με τις κείμενες διατάξεις.</option>
                  <option>6. Σωματική άσκηση σε εξωτερικό χώρο ή κίνηση με κατοικίδιο ζώο, ατομικά ή ανά δύο άτομα, τηρώντας στην τελευταία αυτή περίπτωση την αναγκαία απόσταση 1,5 μέτρου.</option>
                -->
              </div>
            </div>
          </div>
          <div style="margin-top: 20px;">
            <a
              class="button is-rounded is-link is-medium"
              id="createSmsButton"
              @click="createSmsButton"
            >
              Δημιουργία
              SMS
            </a>
          </div>
        </form>

        <div class="modal is-clipped">
          <div class="modal-background"></div>
          <div class="modal-content">
            <h1 class="title is-4">Forma Gov App</h1>
          </div>
          <button class="modal-close is-large" aria-label="close"></button>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  data() {
    return {
      el: "#app",
      fullname: "",
      address: "",
      selectedExitReason: 1,
      exitReasonsToSelect: [
        { text: "1. Φαρμακείο ή γιατρός", value: 1 },
        { text: "2. Σούπερ μάρκετ ή μίνι μάρκετ", value: 2 },
        { text: "3. Τράπεζα", value: 3 },
        { text: "4. Παροχή βοήθειας σε τρίτους", value: 4 },
        {
          text: "5. Μετάβαση σε τελετή - π.χ. κηδεία, γάμος, βάφτιση",
          value: 5
        },
        { text: "6. Γυμναστική ή βόλτα με κατοικίδιο", value: 6 }
      ]
    };
  },
  mounted() {
    // var deviceOnline = window.navigator.onLine;
    // if (deviceOnline) {
    //   alert(deviceOnline);
    // }

    // if online url = zwt
    // if offline url = sms

    // check url for params
    let urlParams = new URLSearchParams(window.location.search);
    let fullnameParam = urlParams.get("fullname");
    if (fullnameParam) {
      this.fullname = fullnameParam;
      window.location.search = "";
    }

    let addressParam = urlParams.get("address");
    if (addressParam) {
      this.address = addressParam;
      window.location.search = "";
    }

    // check localstorage for params
    if (localStorage.fullname) {
      this.fullname = localStorage.fullname;
    }
    if (localStorage.address) {
      this.address = localStorage.address;
    }
    if (localStorage.selectedExitReason)
      this.selectedExitReason = localStorage.selectedExitReason;
  },

  methods: {
    closeBulmaNotification() {
      var s = document.querySelector(".notification:not(.is-hidden)");
      s.classList.add("is-hidden");
    },
    createSmsButton() {
      if (this.fullname && this.address) {
        var button = document.getElementById("createSmsButton");
        button.classList.add("is-loading");
        localStorage.selectedExitReason = this.selectedExitReason;
        // url = "https://google.gr/search?q=" + this.selectedExitReason + " " + this.fullname + " " + this.address;
        // formats https://stackoverflow.com/questions/30360701/how-can-i-make-a-website-button-open-up-an-sms-compose-to-a-phone-number
        var url = "";

        var deviceOnline = window.navigator.onLine;
        if (deviceOnline) {
          url =
            "http://zwt.co/sms?t=13033&body=" +
            this.selectedExitReason +
            " " +
            this.fullname +
            " " +
            this.address;
        }

        if (!deviceOnline) {
          url = "sms:13033?body=" + this.selectedExitReason + " " + this.fullname + " " + this.address;
        }

        window.location.href = url;
        setTimeout(function() {
          button.classList.remove("is-loading");
          return;
        }, 5000);
      } else {
        var alert = document.getElementById("pleaseEnterFullnameAndAddress");
        alert.classList.remove("is-hidden");
      }
    }
  },

  watch: {
    fullname(newName) {
      localStorage.fullname = newName;
    },
    address(newAddress) {
      localStorage.address = newAddress;
    }
  }
};
</script>

<style>
</style>