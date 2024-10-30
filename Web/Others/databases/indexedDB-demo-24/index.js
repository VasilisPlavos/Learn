let db;
window.onload = openDatabase;

async function openDatabase() {
    const request = indexedDB.open("myDatabase", 1);

    request.onerror = (event) => console.error("Database error: " + event.target.error.message);

    request.onsuccess = (event) => {
        db = event.target.result;
        document.getElementById("output").textContent = "Database opened successfully";
    };

    request.onupgradeneeded = async function (event) {
        const db = event.target.result;
        if (!db.objectStoreNames.contains('myObjectStore')) {
            await new Promise(resolve => setTimeout(resolve, 100)); // Delay for demonstration purposes
            db.createObjectStore('myObjectStore', { keyPath: 'id' });
        }
    };
}

async function create() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.add({
        id: 1,
        name: "John Doe",
        age: Math.floor(Math.random() * 100),
    });

    await new Promise((resolve, reject) => {
        request.onsuccess = () => {
            document.getElementById("output").textContent = "Data created successfully";
            resolve;
        };
        request.onerror = event => {
            console.error("Error creating data: " + event.target.error.message);
            reject(event.target.error.message);
        };
    });
}

async function read() {
    const transaction = db.transaction(["myObjectStore"], "readonly");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.get(1);

    await new Promise((resolve, reject) => {
        request.onsuccess = event => resolve(event.target.result);
        request.onerror = event => {
            console.error("Error reading data: " + event.target.error.message);
            reject(event.target.error.message);
        }
    });

    const data = request.result;
    if (data) {
        document.getElementById("output").textContent = `Data read: ${JSON.stringify(data)}`;
    } else {
        document.getElementById("output").textContent = "No data found";
    }
}

async function update() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.get(1);

    await new Promise((resolve, reject) => {
        request.onsuccess = event => resolve(event.target.result);
        request.onerror = event => {
            console.error("Error reading data: " + event.target.error.message);
            reject(event.target.error.message);
        }
    });

    const data = request.result;
    if (data) {
        data.age = Math.floor(Math.random() * 100); // Update the age
        const updateRequest = objectStore.put(data);

        await new Promise((resolve, reject) => {
            updateRequest.onsuccess = resolve;
            updateRequest.onerror = event => reject(event.target.error.message);
        });


        document.getElementById("output").textContent = `Data updated: ${JSON.stringify(data)}`;
    } else {
        document.getElementById("output").textContent = "No data found to update";
    }
}

async function remove() {
    const transaction = db.transaction(["myObjectStore"], "readwrite");
    const objectStore = transaction.objectStore("myObjectStore");

    const request = objectStore.delete(1);

    await new Promise((resolve, reject) => {
        request.onsuccess = () => {
            document.getElementById("output").textContent = "Data removed successfully";
            resolve;
        };
        request.onerror = event => reject(event.target.error.message);
    });
}
