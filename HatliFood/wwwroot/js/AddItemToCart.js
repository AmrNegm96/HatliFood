function AddItemToCart(Id , Name , Price , ImgPath , Description) {

    let Item = {
        Id,
        Name,
        Price,
        ImgPath,
        Description
    };

    setCookie("HatliFood-"+Item.Id , JSON.stringify(Item));
    // cart logic will be added 
}

