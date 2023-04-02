
var SearchText = document.querySelector('#SearchText');

    
document.querySelector('.SearchHandler').addEventListener("keyup",  () => {
    var resultCon = document.querySelector('.search-result');
    if (SearchText.value) {
       
        $.get(`/GetSearchedData/${SearchText.value}`, null, (result) => {

            $(".search-result a").remove();
            if (result.length > 0) {
                
                resultCon.classList.remove('d-none');

                result.forEach(value => {
                    $(".search-result").append(`<a href='Restaurants/RestaurantDetails/${value.id}'> ${value.name} </a>`)
                })
            }
            else {
                resultCon.classList.add('d-none');
            }
            
        })
        
        $(".search-result a").remove();


    }
    else {

        resultCon.classList.add('d-none');
        $(".search-result a").remove();
    }
   
    
})
