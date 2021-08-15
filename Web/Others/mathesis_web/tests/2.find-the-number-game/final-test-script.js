// Τα ερωτήματα 2 έως 7 θα απαντηθούν στο αρχείο αυτό

const newGuess = document.querySelector("#new-guess");
const message = document.querySelector("#message");
const lowHigh = document.querySelector("#low-high");
const checkButton = document.querySelector("#check");
const restartButton = document.querySelector("#restart");
const root = document.querySelector(":root");

// 2. να ορίσετε τους σχετικούς χειριστές συμβάντων
newGuess.addEventListener('keyup', checkKey);
checkButton.addEventListener('click', checkGuess);
restartButton.addEventListener('click', restart);

let previousGuesses = [];
let theGuess;
window.onload = newRandom();
newGuess.focus();
window.onload = restartButton.style.visibility = "hidden";
window.onload = newGuess.value = '';

function newRandom() {
	/* 3. συνάρτηση που βρίσκει ένα τυχαίο αριθμό μεταξύ 1 και 100
	 και τον εκχωρεί στη μεταβλητή theGuess */
	theGuess = Math.floor((Math.random() * 100) + 1);
}

function checkKey(e) {
	/* 4. συνάρτηση που όταν ο χρήστης πατήσει <<enter>>
	 να καλεί τη συνάρτηση που αποτελεί τον κεντρικό ελεγκτή του παιχνιδιού.
	 */
	if (e.key === 'Enter') {
		checkGuess();
	}
}

function checkGuess() {
	/* 5. Να ορίσετε συνάρτηση checkGuess η οποία καλείται είτε όταν ο χρήστης πατήσει <<enter>>
	στο πεδίο "new-guess" είτε όταν πατήσει το πλήκτρο "check", η οποία είναι ο κεντρικός ελεγκτής,
	καλεί τη συνάρτηση processGuess (η οποία αποφαίνεται για την ορθότητα του αριθμού) και κάνει
	τις κατάλληλες ενέργειες για να μην μπορεί να εισάγει ο χρήστης νέο αριθμό ή να ανασταλεί η
	λειτουργία του <<enter>>, εμφάνιση του πλήκτρου 'restart' και την εξαφάνιση του πλήκτρου 'check'
	σε περίπτωση ολοκλήρωσης του παιχνιδιού. */

	if (newGuess.value) {
		var processGuessResult = processGuess(newGuess.value);

		if (processGuessResult === 'win') {
			restartButton.style.visibility = "visible";
			checkButton.style.visibility = "hidden";
			newGuess.removeEventListener('keyup', checkKey);
		}
		if (processGuessResult === 'lost') {
			restartButton.style.visibility = "visible";
			checkButton.style.visibility = "hidden";
			newGuess.removeEventListener('keyup', checkKey);
		}
	}
}

function processGuess(newValue) {
	/* 6.  Να ορίσετε συνάρτηση processGuess(newValue) η οποία καλείται από τη συνάρτηση checkGuess,
	περιέχει τη λογική του παιχνιδιού, ελέγχει αν η τιμή του χρήστη είναι σωστή, ή αν το παιχνίδι έχει
	τελειώσει χωρίς ο χρήστης να έχει βρει τον αριθμό, και επιστρέφει αντίστοιχα την τιμή "win", ή "lost",
	δημιουργεί και εμφανίζει τα κατάλληλα μηνύματα, αλλάζοντας το χρώμα του στοιχείου μηνυμάτων.
	Όλα τα μηνύματα του προγράμματος εμανίζονται από την processGuess().
	Σε περίπτωση που το παιχνίδι δεν έχει ακόμα τελειώσει, η συνάρτηση μπορεί είτε να μην επιστρέφει κάποια ιδιαίτερη τιμή,
	είτε να επιστρέφει κάποια τιμή της επιλογής σας */
	newGuess.value = '';

	if (!isNaN(newValue)) {
		function prepareMessage(newValue) {
			if (previousGuesses.length === 0) {
				lowHigh.innerText = "Προηγούμενες προσπάθειες: ";
			}
			lowHigh.innerText = lowHigh.innerText + ' ' + newValue;
			previousGuesses.push(newValue);
		};

		if (newValue > theGuess) {
			message.innerText = "Λάθος, το ξεπέρασες";
			message.style.backgroundColor = "var(--msg-wrong-color)";
			prepareMessage(newValue);
		}
		if (newValue < theGuess) {
			message.innerText = "Λάθος, είσαι πιο χαμηλά";
			message.style.backgroundColor = "var(--msg-wrong-color)";
			prepareMessage(newValue);
		}
		if (newValue == theGuess) {
			message.innerText = "Μπράβο το βρήκες!";
			message.style.backgroundColor = "var(--msg-win-color)";
			prepareMessage(newValue);
			return 'win';
		}
		if (previousGuesses.length == 10) {
			message.innerText = "Τέλος παιχνιδιού, έχασες!";
			message.style.backgroundColor = "var(--msg-wrong-color)";
			return 'lost';
		}
	} else {
		message.innerText = "Δώσε αριθμό!";
		message.style.backgroundColor = "var(--msg-wrong-color)";
	}
}

function restart() {
	/* 7. Να ορίσετε συνάρτηση restart η οποία καλείται όταν ο χρήστης πατήσει το πλήκτρο
	'restart' και επανεκινεί τη διαδικασία */
	restartButton.style.visibility = "hidden";
	checkButton.style.visibility = "visible";
	previousGuesses = [];
	lowHigh.innerText = '';
	newGuess.value = '';
	message.innerText = '';
	newGuess.addEventListener('keyup', checkKey);
	newRandom();
}