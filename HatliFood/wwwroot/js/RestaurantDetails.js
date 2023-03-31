console.log(window.location);


Categories   = document.getElementById("Categories");
Menus        = document.getElementById("Menus");

if (Categories != null) {
    Categories.onclick = () => {
        console.log("cat")
        console.log(document.querySelector('.Categories'));
        document.querySelector('.Categories').style.display = "block"
    }
}


if (Menus != null) {
    Menus.onclick = () => {
        console.log("Menus")
        document.querySelector('.Menus').style.display = "block"
    }
}


