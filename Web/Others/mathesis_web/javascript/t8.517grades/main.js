let lessons = 0;
let gradesSum = 0;

const calculateButton = document.querySelector('#calculateButton');
calculateButton.addEventListener('click', calculate);

const resetButton = document.querySelector('#resetButton');
resetButton.addEventListener('click', reset);

const newGrade = document.querySelector('#this-grade');
newGrade.addEventListener('keyup', addNewGrade);

function addNewGrade(e){
    if (e.key === 'Enter'){
        calculate();
    }
}

function updateGrades(average, grades){
    document.querySelector("#this-grade").value = '';
    document.querySelector("#average").value = average;
    document.querySelector("#numOfGrades").value = grades;
}

function reset(){
    updateGrades('','');
    
}

function calculate() {
  let thisGrade = parseInt(document.querySelector("#this-grade").value);
  if (thisGrade >= 0 && thisGrade < 11) {
    lessons = lessons + 1;
    gradesSum = gradesSum + thisGrade;
    let average = gradesSum / lessons;
    updateGrades(average, lessons);
  } else {
      alert('Εισάγετε βαθμό από 0 έως και 10!')
  }
}