$(".header").hide();
$(".FormEdit").hide();
$("#EditUserId").hide();


changePassContainer     = document.querySelector('#changePasswordContainer')
changePhoneContainer    = document.querySelector('#changePhoneContainer');
AccountContainer        = document.querySelector('#accountSettingsContainer')
SectionContainer        = document.querySelectorAll('.section-container')

changePass              = document.querySelector('.change-password');
changePhone             = document.querySelector('.change-phone');
changeAccount           = document.querySelector('.account-details');


function HideSections(Section_Show,Section_container ,Section1_Hide, Section2_Hide) {
    Section_Show.addEventListener('click', () => {
        SectionContainer.forEach((e) => e.classList.remove("active"))

        Section_container.classList.remove("d-none")
        Section_Show.classList.add("active");

        if (!Section1_Hide.classList.contains("d-none")) {
            Section1_Hide.classList.add("d-none")

        }

        if (!Section2_Hide.classList.contains("d-none")) {
            Section2_Hide.classList.add("d-none")

        }

    })
}


HideSections(changeAccount, AccountContainer, changePhoneContainer, changePassContainer)
HideSections(changePass, changePassContainer, changePhoneContainer, AccountContainer)
HideSections(changePhone, changePhoneContainer, changePassContainer, AccountContainer)