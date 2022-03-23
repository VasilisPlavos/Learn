// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (location.pathname !== "/login")
{
    function isUserAuth()
    {
        let userAuth = getCookie("UserAuth");
        if (!userAuth) return false;
        return true;
    }

    function getCookie(cookieName) {
        const cookieValue = document.cookie.match("(^|;)\\s*" + cookieName + "\\s*=\\s*([^;]+)");
        return cookieValue ? cookieValue.pop() : "";
//        var cookies = ` ${document.cookie}`.split(";");
//        var val = "";
//        for (var i = 0; i < cookies.length; i++) {
//            var cookie = cookies[i].split("=");
//            if (cookie[0] == ` ${cname}`) {
//                return cookie[1];
//            }
//        }
//        return "";
    }

    function logout() {
        document.cookie = "UserAuth=false; expires=Fri, 31 Dec 1999 23:59:59 GMT; SameSite=None; Secure";
        localStorage.removeItem('CloudOnApiKey');
        window.location.replace("login");
    }

    if (isUserAuth())
    {
        var logoutButton = document.querySelector("#login-logout");
        logoutButton.innerText = "Αποσύνδεση";
        logoutButton.href = "/logout";
    } else
    {
        logout();
    }
}

