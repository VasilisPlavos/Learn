// var mobileNav = document.querySelector(".header-display-mobile");
var mobileNav = document.querySelector('.header-menu-nav');

// desktop navBar
// var navBar = document.querySelector("#header > div.header-menu.header-menu--folder-list > div.header-menu-nav > nav > div > div.header-menu-nav-folder-content > div");

// init default lang
var defaultLang = 'en';
var currentLang = location.pathname.split("/")[1];
if (currentLang?.length !== 2) 
{
    currentLang = defaultLang;
}


// hide links with /en/ or /el/ urls
var navItems = mobileNav.querySelectorAll('.header-menu-nav-item');
for (const item of navItems) {
    if (currentLang == 'el') 
    {
        if (item.querySelector('a').href == `${location.origin}/`) 
            {
                item.style.display = 'none';
                continue;
            }
    }

    var itemLang = item.querySelector('a').href.split('/')[3];
    if (itemLang?.length !== 2) continue;
    if (itemLang == currentLang) continue;
    item.style.display = 'none';    
}

// fix change language menu
var mobActionsMenu = document.querySelector(".header-menu-actions");
if (currentLang == "en") {
    mobActionsMenu.innerHTML = 
        "<div class='language'> <a href='/el/home/' class='lang-el'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gr.svg' alt='Change to Greek' style='width: 22px; margin-top: 5px'/></a></div>"
         + mobActionsMenu.innerHTML;
}

if (currentLang == 'el') {
    mobActionsMenu.innerHTML = 
        "<div class='language'> <a href='/en/home/' class='lang-en'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gb.svg' alt='Change to English' style='width: 22px; margin-top: 5px'/></a></div>"
         + mobActionsMenu.innerHTML;
}