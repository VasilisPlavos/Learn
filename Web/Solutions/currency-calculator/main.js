// importing Axios library
const axios = window.axios;

new Vue({
  el: '#app',
  vuetify: new Vuetify(),
  mounted() {
    // Update List of Currencies dynamically for both FROM and TO dropdown menus.
    this.getListOfCurrencies();

    // Looking for hash(#) on URL to autocomplete the values on the calculator if possible.
    if (document.location.hash) {
      var urlHash = document.location.hash;
      if (urlHash.split("/").length) { this.updateCalculatorValues(urlHash) }
    }
  },
  data() {
    return {
      amount: null,
      amountRules: [
        (value) => !isNaN(value) || "Please enter numbers only." // Number validation.
      ],
      selectedFromCurrency: "EUR - Euro",
      selectedToCurrency: "USD - United States Dollar",
      exchangeRate: null,
      exchangeRequest: '',
      exchangeResults: "",

      // Default currencies in case that JSON file will not return data.
      currencies: ['EUR - Euro', 'USD - United States Dollar', 'CHF - Swiss Franc', 'GBP - British Pound Sterling', 'JPY - Japanese Yen', 'CAD - Canadian Dollar'],
      
      // Error Modal properties for user warnings.
      errorModal: {
        isActive: false,
        title: "Error",
        subTitle: "Error! Please refresh and try again in 5 minutes.",
      },
    }
  },
  methods: {
    async getListOfCurrencies() {
      // Checking LocalStorage to see if I already have the currencies from physical_currency_list.data.json
      // If I already have them, I am not downloading them again to decrease requests.
      if (localStorage.getItem('currencies')) {
        var lSCurrencies = JSON.parse(localStorage.getItem('currencies'))
        if (lSCurrencies.length == 158) {       // Validate that all the currency codes exist in LocalStorage.
          this.currenciesInit(lSCurrencies);
          return
        }
      }

      // If localStorage key is empty or something is error I clear the values of this key for security reasons.
      localStorage.removeItem('currencies')
      await axios
        .get('./physical_currency_list.data.json')
        .then((response) => this.currenciesInit(response.data.currencies))
        .catch(function (error) {
          // handle error
          console.log(error);
        });
    },

    // Initialize currencies for dropdown options.
    currenciesInit(currenciesObj) {
      this.currencies = [];
      for (let i in currenciesObj) {
        this.currencies.push(`${currenciesObj[i].code} - ${currenciesObj[i].name}`)
      }

      // Saving currencies from physical_currency_list.data.json in LocalStorage to decrease server requests.
      localStorage.setItem('currencies', JSON.stringify(currenciesObj))
    },


    updateCalculatorValues(urlHash) {
      // Pattern example of urlHash: #3/EUR/USD
      var firstHash = urlHash.split("/")[0].substring(1)  // 3
      var secondHash = urlHash.split("/")[1]              // EUR
      var thirdHash = urlHash.split("/")[2]               // USD

      // Updating Amount from URL
      if (!isNaN(firstHash)) { this.amount = firstHash }

      // Updating From from URL
      if (secondHash) {
        for (let i in this.currencies) {
          let currencyCode = this.currencies[i].split(" - ")[0]
          if (secondHash.toUpperCase() == currencyCode.toUpperCase()) { this.selectedFromCurrency = this.currencies[i] }
        }
      }

      // Updating To from URL
      if (thirdHash) {
        for (let i in this.currencies) {
          let currencyCode = this.currencies[i].split(" - ")[0]
          if (thirdHash.toUpperCase() == currencyCode.toUpperCase()) { this.selectedToCurrency = this.currencies[i] }
        }
      }
    },

    // Calculate button method
    calculate() {
      var alertMessage = this.formValidations();
      if (alertMessage.length) {
        this.errorModal.isActive = true;
        this.errorModal.subTitle = alertMessage;
        return;
      }

      var selectedFromCurrencyCode = this.selectedFromCurrency.split(" - ")[0]
      var selectedToCurrencyCode = this.selectedToCurrency.split(" - ")[0]
      this.doCalculate(this.amount, selectedFromCurrencyCode, selectedToCurrencyCode);
    },

    
    formValidations() {
      var alertMessage = "";
      if (this.amount == null) {
        alertMessage = alertMessage + `Please enter amount.`
        return alertMessage
      }
      if (isNaN(this.amount)) {
        alertMessage = alertMessage + "Please enter numbers only. \r\n"
      }

      return alertMessage
    },


    // Check FROM, TO props.
    // If FROM, TO props, are same with the previous calculation,
    // then I skip the API request.
    async doCalculate(amount, from, to) {
      document.location.hash = `#${amount}/${from}/${to}` // updating url hash
      let newRequest = `${from} ${to}`
      if (newRequest != this.exchangeRequest) {
        this.exchangeResults = '';
        await axios
          .get(`https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=${from}&to_currency=${to}&apikey=TODO:key-here`)
          .then((response) => this.exchangeRate = response.data["Realtime Currency Exchange Rate"]["5. Exchange Rate"])
          .catch((error) => {
            console.log(error);
            return;
          })
      }

      let result = amount * this.exchangeRate; // Result in number
      this.exchangeResults = `${amount} ${from} = ${result} ${to}`; // The results property on index.html
      this.exchangeRequest = newRequest // Saving the request for reuse of the Exchange Rate
    }


  }
})