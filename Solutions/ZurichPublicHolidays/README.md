# TODO:
1. I can use this api: https://holidayapi.com/countries/ch-zh/2023
2. Translations
3. Make everything works programmaticly

### Quick-n-dirty 2 hack
I can just edit the public holidays of argies.gr

1. Go to [argies.gr](https://argies.gr/), login, open console
1. add this function
    ```
    function addOrUpdateHoliday(d, m, title){
        $.getJSON(`/ajax/add-or-update-holiday?id=f%3A2%3A1&name=${title}&moveable=0&offset=0&day=${d}&month=${m}`)
    }
    ```
1. Add the holidays using the function. example `addOrUpdateHoliday(20, 05, "Pfingstmontag")`


### Quick-n-dirty hack
With this hack I can use argies.gr for 2024

```
function clearCircles() {
    for (const circle of document.querySelectorAll('span.encircle')) {
        circle.classList.remove('encircle');
    }
}

function addYellow() {
    var selectedIds = [329, 401, 415, 501, 508, 509, 510, 520, 801, 909, 1224, 1225, 1226, 1227, 1230, 1231];
    for (const id of selectedIds) {
        document.getElementById(id).style.backgroundColor = 'yellow';
    }
}
```

### Quick-n-dirty hack for Tampermonkey
Copy paste the above code to Tampermonkey and then visit [argies.gr](https://argies.gr/)

```
// ==UserScript==
// @name         Argies.gr scripts
// @namespace    https://plavos.com
// @version      2024-02-17
// @description  try to take over the world!
// @author       You
// @match        https://www.argies.gr/*
// @grant        none
// ==/UserScript==

(async function () {
  'use strict';

  console.log("Tampermonkey script running: Kinkster scripts");
  await new Promise(resolve => setTimeout(resolve, 1));

  init();

  function init() {
    createButton("swiss argies");
  }

  function activateSwissArgies() {

    if (document.querySelector("#holiday-list > h1").innerText != 'Ημερολόγιο αργιών του 2024') {
      alert("this functionality works only for 2024!");
      return;
    }

    clearCircles();
    addYellow();
  }

  function addYellow() {
    var selectedIds = [329, 401, 415, 501, 508, 509, 510, 520, 801, 909, 1224, 1225, 1226, 1227, 1230, 1231];
    for (const id of selectedIds) {
      document.getElementById(id).style.backgroundColor = 'yellow';
    }
  }

  function clearCircles() {
    for (const circle of document.querySelectorAll('span.encircle')) {
      circle.classList.remove('encircle');
    }
  }


  function createButton(buttonText) {
    const divElement = document.createElement('div');
    divElement.id = 'mybutton';

    const buttonElement = document.createElement('button');
    buttonElement.classList.add('button-class-example');
    buttonElement.textContent = buttonText;
    buttonElement.addEventListener('click', () => { activateSwissArgies(); });

    divElement.appendChild(buttonElement);
    document.body.appendChild(divElement);

    divElement.style.setProperty('position', 'fixed');
    divElement.style.setProperty('bottom', '8px');
    divElement.style.setProperty('right', '10px');
  }


})();
```

Sources:
* https://www.stadt-zuerich.ch/portal/de/index/jobs/anstellungsbedingungen/ferien-urlaub-betriebsferientage/feiertage-betriebsferientage.html
* https://www.zh.ch/content/dam/zhweb/bilder-dokumente/footer/arbeiten-fuer-den-kanton/personalamt/Feiertage2023.pdf