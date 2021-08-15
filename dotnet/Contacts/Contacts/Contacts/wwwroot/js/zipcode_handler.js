// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var zipCodeSelector = document.querySelector("#zipCodeSelect");
var locationSelector = document.querySelector("#locationSelect");


function UpdateZip() {
    zipCodeSelector.value = locationSelector.value;
}

function UpdateLocation() {
    locationSelector.value = zipCodeSelector.value;
}