// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var loginLogoutButton = document.querySelector("#login-logout");

function logout() {
    document.cookie = "UserAuth=false; expires=Fri, 31 Dec 1999 23:59:59 GMT; SameSite=None; Secure";
    localStorage.removeItem('CloudOnApiKey');
    window.location.replace("login");
}

function isUserAuth() {
    let userAuth = getCookie("UserAuth");
    if (!userAuth) return false;
    return true;
}

function getCookie(cookieName) {
    const cookieValue = document.cookie.match("(^|;)\\s*" + cookieName + "\\s*=\\s*([^;]+)");
    return cookieValue ? cookieValue.pop() : "";
}


if (location.pathname !== "/login")
{
    if (isUserAuth())
    {
        loginLogoutButton.innerText = "Αποσύνδεση";
        loginLogoutButton.style.cursor = "pointer";
        loginLogoutButton.addEventListener("click", function() { logout() });
    } else
    {
        logout();
    }
}

