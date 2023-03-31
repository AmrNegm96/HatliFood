/*var visitCount = 0 ;*/

function getCookie(cookieName) {
    var name = cookieName + "=";

    var Arrofcookie = document.cookie.split(';');

    for (var i = 0; i < Arrofcookie.length; i++) {
        var v = Arrofcookie[i].trim();

        if (v.indexOf(name) == 0) {
            return v.substring(name.length, v.length);
        }
    }
    return "";
}

function setCookie(cookieName, cookieValue, expiryDate) {
    if (expiryDate) {
        var time = new Date();
        time.setTime(time.getTime() + (expiryDate * 24 * 60 * 60 * 1000));
        //"myCookie=" + cookieValue + "; expires=" + expirationDateString + "; HttpOnly; path=/; domain=" + domain;
        document.cookie = cookieName + "=" + cookieValue + ";" + "expires=" + time.toUTCString() + ";HttpOnly;path=/;domain=localhost";
    }
    else {
        document.cookie = cookieName + "=" + cookieValue + ";HttpOnly;path=/;domain=localhost;";
    }
}

function deleteCookie(cookieName) {
    document.cookie = cookieName + "=" + "" + "; expires=Thu, 01 Jan 2000 00:00:00 UTC";
}

function allCookieList() {
    //return all cookies in 1 string 
    var x = document.cookie;
    return x;
}

function hasCookie(cookieName) {
    var Username = getCookie(cookieName)

    if (Username == "") {
        return false;
    }
    else {
        return true;
    }
}

