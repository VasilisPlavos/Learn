
var tblData;
let dataSet = [];
InitProductsPage();

function displayButton(selector, visible, eventListenerFunction) {
    var btn = document.querySelector(selector);
    btn.style.display = !visible ? "none" : "";
    btn.style.cursor = "pointer";
    if (eventListenerFunction) btn.addEventListener("click", eventListenerFunction);
}

async function InitProductsPage() {
    var products = await GetProductsAsync(false);
    if (products.length === 0) {
        displayButton("#initProductsButton", true, initProductsAsync);
        var table = document.querySelector("#tblData");
        table.style.display = 'none';
    } else {
        displayButton("#updateProductsButton", true, updateProductsAsync);
        displayButton("#addProductButton", true);
        
        for (var p of products)
        {
            dataSet.push([p.code, p.name, p.barcode, p.wholesalePrice, p.retailPrice, p.discount, p.id]);
        }
        loadDataTable(dataSet);
    }
    
}

async function initProductsAsync() {
    var products = await GetProductsAsync(true);
    displayButton("#initProductsButton", false);
    displayButton("#updateProductsButton", true, updateProductsAsync);
    displayButton("#addProductButton", true);
    loadDataTable(dataSet);
    console.log(products);
}

async function updateProductsAsync() {
    var products = await GetProductsAsync(false);
    console.log(products);
}

async function GetProductsAsync(sync) {
    const reqUrl = `https://localhost:44369/api/Products?sync=${sync}`;
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



function loadDataTable(dataSet) {
    tblData = $('#tblData').DataTable({
        data: dataSet,
        columns: [
            { title: "Code" },
            { title: "Name" },
            { title: "Barcode" },
            { title: "WholesalePrice" },
            { title: "RetailPrice" },
            { title: "Discount" },
            {
                title: "", 
                render: function (title) {
                    return `<div class="text-center">
                                <a href="/Products/${title}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> <i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("${title}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }
            }
        ]
    });
}


function Delete(productId) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: `https://localhost:44369/api/Products/${productId}`,
                success: function () {
                    toastr.success("Done");
                    location.reload();
                },
                error: function() {
                    toastr.error("Error");
                }
            });
        }
    });
}