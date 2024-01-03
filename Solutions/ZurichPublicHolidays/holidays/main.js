var days = document.querySelectorAll(".month div span a");
const weekdayNames = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];


function init() {
  generateCalendar(0, 2023);

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

function generateCalendar(month, year) {
    // Create a new Date object
    let date = new Date(year, month, 1);

      // Get the first day of the month
    let firstDay = date.getDay();

    // Get the total days in the month
    let daysInMonth = new Date(year, month + 1, 0).getDate();


    var monthSpan = document.createElement('span');
    monthSpan.classList.add('month')
}

function generateCalendarOld(month, year) {
  // Create a new Date object
  let date = new Date(year, month, 1);

  // Get the first day of the month
  let firstDay = date.getDay();

  // Get the total days in the month
  let daysInMonth = new Date(year, month + 1, 0).getDate();

  // Create an HTML table for the calendar
  let calendarTable = document.createElement('table');
  calendarTable.classList.add('calendar');

  // Create the table header
  let headerRow = document.createElement('tr');
  for (let i = 0; i < 7; i++) {
    let dayName = document.createElement('th');
    dayName.textContent = weekdayNames[i];
    headerRow.appendChild(dayName);
  }
  calendarTable.appendChild(headerRow);

  // Create the table body
  let body = document.createElement('tbody');

  // Generate the days of the month
  for (let i = 0; i < 6; i++) {
    let weekRow = document.createElement('tr');

    for (let j = 0; j < 7; j++) {
      let dayCell = document.createElement('td');

      // Calculate the day of the month
      let currentDay = firstDay + i * 7 + j;

      // Check if the current day is within the current month
      if (currentDay <= daysInMonth) {
        // Create a link for the current day
        let dayLink = document.createElement('a');
        dayLink.textContent = currentDay;
        dayLink.href = `#${year}-${month}-${currentDay}`;
        dayLink.setAttribute('data-day', currentDay);
        dayCell.appendChild(dayLink);
      } else {
        // Create a blank cell for days outside the current month
        let blankCell = document.createElement('span');
        blankCell.textContent = '&nbsp;';
        dayCell.appendChild(blankCell);
      }

      weekRow.appendChild(dayCell);
    }

    body.appendChild(weekRow);
  }

  calendarTable.appendChild(body);
  var calendarElement = document.getElementById('calendar');
  calendarElement.appendChild(calendarTable);
}


init();