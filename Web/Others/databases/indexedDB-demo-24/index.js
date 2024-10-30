let db;
openDatabase();

function openDatabase() {
    const request = indexedDB.open("myDatabase", 1);

    request.onerror = (event) => console.error("Database error: " + event.target.errorCode);
    
    request.onsuccess = (event) => {
        db = event.target.result;
        document.getElementById("output").textContent = "Database opened successfully";
    };

    request.onupgradeneeded = function (event) {
        const db = event.target.result;
        if (!db.objectStoreNames.contains("myObjectStore")) {
            db.createObjectStore("myObjectStore", { keyPath: "id" });
        }
    };
}

function create() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.add({
        id: 1,
        name: "John Doe",
        age: Math.floor(Math.random() * 100),
    });

    request.onsuccess = () => {
        document.getElementById("output").textContent = "Data created successfully";
    };

    request.onerror = function (event) {
        console.error("Error creating data: " + event.target.errorCode);
    };
}

function read() {
    const transaction = db.transaction(["myObjectStore"], "readonly");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.get(1);

    request.onsuccess = function (event) {
        const data = event.target.result;
        if (data) {
            document.getElementById(
                "output"
            ).textContent = `Data read: ${JSON.stringify(data)}`;
        } else {
            document.getElementById("output").textContent = "No data found";
        }
    };

    request.onerror = function (event) {
        console.error("Error reading data: " + event.target.errorCode);
    };
}

function update() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.get(1);

    request.onsuccess = function (event) {
        const data = event.target.result;
        if (data) {
            data.age = Math.floor(Math.random() * 100); // Update the age
            const updateRequest = objectStore.put(data);
            updateRequest.onsuccess = function (event) {
                document.getElementById("output").textContent =
                    "Data updated successfully";
            };
            updateRequest.onerror = function (event) {
                console.error("Error updating data: " + event.target.errorCode);
            };
        } else {
            document.getElementById("output").textContent = "No data found to update";
        }
    };

    request.onerror = function (event) {
        console.error("Error reading data for update: " + event.target.errorCode);
    };
}

function remove() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.delete(1);

    request.onsuccess = function (event) {
        document.getElementById("output").textContent = "Data removed successfully";
    };

    request.onerror = function (event) {
        console.error("Error removing data: " + event.target.errorCode);
    };
}
