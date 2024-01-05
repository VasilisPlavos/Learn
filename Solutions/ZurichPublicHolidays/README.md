# TODO:
1. I can use this api: https://holidayapi.com/countries/ch-zh/2023
2. Translations
3. Make everything works programmaticly

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


Sources:
* https://www.stadt-zuerich.ch/portal/de/index/jobs/anstellungsbedingungen/ferien-urlaub-betriebsferientage/feiertage-betriebsferientage.html
* https://www.zh.ch/content/dam/zhweb/bilder-dokumente/footer/arbeiten-fuer-den-kanton/personalamt/Feiertage2023.pdf