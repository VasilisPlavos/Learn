var days = document.querySelectorAll(".month div span a");

function init() {

  // mark today
  const dayOfTheYear = getDayOfYear();
  days[dayOfTheYear].parentElement.classList.add('t')

  // add click event listener while clicking a day
  for (const day of days) {
    day.addEventListener("click", dateClicked);
  }
}






function dateClicked() {
  var selectedHolidays = parseInt(document.querySelector("#holiday-groups > span").innerText.split("Ημέρες άδειας: ")[1]);

  var classList = this.parentElement.classList;
  if (classList.contains('v')) {
    classList.remove('v');
    selectedHolidays = selectedHolidays - 1;
  } else {
    classList.add('v');
    selectedHolidays = selectedHolidays + 1;
  }

  document.querySelector("#holiday-groups > span").innerText = `Ημέρες άδειας: ${selectedHolidays}`;
}

function getDayOfYear() {
  var now = new Date();
  var start = new Date(now.getFullYear(), 0, 1);
  var diff = (now - start) + ((start.getTimezoneOffset() - now.getTimezoneOffset()) * 60 * 1000);
  var oneDay = 1000 * 60 * 60 * 24;
  var day = Math.floor(diff / oneDay);
  return day;
}




init();