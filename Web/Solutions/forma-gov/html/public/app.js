const app = new Vue({
    el: '#app',
    data: {
        fullname: '',
        address: '',
        selectedExitReason: 1,
        exitReasonsToSelect: [
            {text: '1. Φαρμακείο ή γιατρός', value: 1},
            {text: '2. Σούπερ μάρκετ ή μίνι μάρκετ', value: 2},
            {text: '3. Τράπεζα', value: 3},
            {text: '4. Παροχή βοήθειας σε τρίτους', value: 4},
            {text: '5. Μετάβαση σε τελετή - π.χ. κηδεία, γάμος, βάφτιση', value: 5},
            {text: '6. Γυμναστική ή βόλτα με κατοικίδιο', value: 6}
        ],
    },
    mounted() {
        // check url for params
        let urlParams = new URLSearchParams(window.location.search);
        let fullnameParam = urlParams.get('fullname');
        if (fullnameParam) {
            this.fullname = fullnameParam;
            window.location.search = '' }

        let addressParam = urlParams.get('address');
        if (addressParam) {
            this.address = addressParam;
            window.location.search = ''} 

        // check localstorage for params
        if (localStorage.fullname) { this.fullname = localStorage.fullname };
        if (localStorage.address) { this.address = localStorage.address };
        if (localStorage.selectedExitReason) this.selectedExitReason = localStorage.selectedExitReason;
    },
    methods: {
        closeBulmaNotification() {
            var s = document.querySelector('.notification:not(.is-hidden)')
            s.classList.add('is-hidden')
        },
        createSmsButton() {
            if (this.fullname && this.address) {
                var button = document.getElementById("createSmsButton");
                button.classList.add("is-loading");
                localStorage.selectedExitReason = this.selectedExitReason;
                // url = "https://google.gr/search?q=" + this.selectedExitReason + " " + this.fullname + " " + this.address;
                // formats https://stackoverflow.com/questions/30360701/how-can-i-make-a-website-button-open-up-an-sms-compose-to-a-phone-number
                url = "sms:13033?body=" + this.selectedExitReason + " " + this.fullname + " " + this.address; 
                window.location.href = url;
                setTimeout(function(){
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

    },
})