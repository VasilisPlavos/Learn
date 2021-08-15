// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Write your JavaScript code.
function filterRow(tds, filter) {
    for (let i = 0; i < tds.length - 1; i++) {
        const cellValue = tds[i].textContent || tds[i].innerText;
        if (cellValue.toUpperCase().indexOf(filter) > -1)
            return true;
    }
    return false;
}


function searchContact() {
    var input, filter, table, tr, i;
    input = document.getElementById("searchContactInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("contactTable");
    tr = table.getElementsByTagName("tr");

    for (i = 1; i < tr.length; i++) {
        const tds = tr[i].getElementsByTagName("td");
        const keepRow = filterRow(tds, filter);
        if (keepRow) { tr[i].style.display = ""; }
        else { tr[i].style.display = "none"; }
    }
}