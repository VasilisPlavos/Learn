# Currency Calculator

### Technologies
I select Vuejs with [Vuetify - Material Design Component Framework](http://vuetifyjs.com/).
I use API from Alpha Vantage for updating the currencies. ([API documentation](https://www.alphavantage.co/documentation/#currency-exchange))
I use the Axios library for HTTP and API requests.

### URL hash
You can use the URL hash to autocomplete the values of the calculator.
**example:** [http://localhost:8887/#32123/EUR/USD](http://localhost:8887/#32123/EUR/USD)
```
will add:
amount=32123
from=EUR and
to=USD
```

### User experience optimization
**For better User Experience:**
* Application Focus automatically on the Amount field.
* Clearable field: User can delete Amount field pressing (x)
* Can submit the Calculate button pressing the Enter key

### Resources optimization
**For fewer server resources:** I use LocalStorage to store some data.
**For fewer API resources:** If the user didn't change FROM or TO props I reuse the stored exchange rate from the last request.

**Warning:** This API key has limits at 5 requests per minute.