

Categories   = document.getElementById("Categories");
Menus = document.getElementById("Menus");
Orders = document.getElementById("Orders");


CategoriesContainer = document.getElementById('Categories-container')
MenusContainer = document.getElementById('Menus-container')
OrdersContainer = document.getElementById('Orders-container')



if (Categories != null) {
    Categories.onclick = () => {
        CategoriesContainer.classList.add('d-flex', 'active');

        MenusContainer.classList.remove('active');
        MenusContainer.classList.remove('d-flex');

        OrdersContainer.classList.remove('d-flex');
        OrdersContainer.classList.remove('active');


    }
}


if (Menus != null) {
    Menus.onclick = () => {
        MenusContainer.classList.add('d-flex');
        OrdersContainer.classList.add('active');

        CategoriesContainer.classList.remove('d-flex');
        CategoriesContainer.classList.remove('active');

        OrdersContainer.classList.remove('d-flex');
        OrdersContainer.classList.remove('active');


    }
}

if (Orders != null) {
    Orders.onclick = () => {
        OrdersContainer.classList.add('d-flex');

        CategoriesContainer.classList.remove('d-flex');
        CategoriesContainer.classList.remove('active');

        MenusContainer.classList.remove('d-flex');
        MenusContainer.classList.remove('active');

    }
}


