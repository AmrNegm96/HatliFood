// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


order = document.querySelector('.btn-order');

order?.addEventListener("click", function () {
    chekButton = document.querySelector('.AuthenticationWrapper')
    if ((getCookie('isAuth') && getCookie('UserRole'))) {
      location.href = "/Orders/"
    }
})

    