var productId = document.URL.split("/")[5];
InitProductEditPage();
var createOrUpdateButton = document.querySelector("#createOrUpdateButton");
createOrUpdateButton.addEventListener("click", async function() { await SaveAsync(); });

async function InitProductEditPage() {
    if (productId) {
        var product = await GetProductAsync(productId);
        if (!product.id) {
            location.href = 'https://localhost:44329/Products/Edit';
        } else {
            document.querySelector("#Name").value = product.name;
            document.querySelector("#Barcode").value = product.barcode;
            document.querySelector("#Code").value = product.code;
            document.querySelector("#WholesalePrice").value = product.wholesalePrice;
            document.querySelector("#RetailPrice").value = product.retailPrice;
            document.querySelector("#Discount").value = product.discount;
            document.querySelector("#Description").value = product.description;
            document.querySelector("#ExternalId").value = product.externalId;
            document.querySelector("#title").innerText = `Προϊόν ${productId}`;
            createOrUpdateButton.innerText = "Ενημέρωση";
        }

    } else {
        document.querySelector("#title").innerText = `Δημιουργία προϊόντος`;
    }
}


async function SaveAsync() {
    var product = {
        id: productId,
        name: document.querySelector("#Name").value,
        barcode: document.querySelector("#Barcode").value,
        code: document.querySelector("#Code").value,
        wholesalePrice: document.querySelector("#WholesalePrice").value,
        retailPrice: document.querySelector("#RetailPrice").value,
        discount: document.querySelector("#Discount").value,
        description: document.querySelector("#Description").value,
        externalId: document.querySelector("#ExternalId").value
    };

    const reqUrl = `https://localhost:44369/api/Products`;
    var coApiKey = localStorage.getItem("CloudOnApiKey");
    const response = fetch(reqUrl, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        headers: {
            'Content-Type': "application/json",
            'Authorization': coApiKey
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
        body: JSON.stringify(product)
        })
        .then(response => {
            toastr.success("Done");
            location.href = 'https://localhost:44329/Products';
        })
        .catch((error) => { console.error("Error:", error) });


    const delay = ms => new Promise(res => setTimeout(res, ms));
    await delay(5000);
}

async function GetProductAsync(productId) {
    const reqUrl = `https://localhost:44369/api/Products/${productId}`;
    var coApiKey = localStorage.getItem("CloudOnApiKey");
    const response = fetch(reqUrl, {
            method: "GET",
            mode: "cors",
            cache: "no-cache",
            headers: {
                'Content-Type': "application/json",
                'Authorization': coApiKey
            },
            redirect: "follow",
            referrerPolicy: "no-referrer"
        })
        .then(response => { return response.json() })
        .catch((error) => { console.error("Error:", error) });

    return response;
}