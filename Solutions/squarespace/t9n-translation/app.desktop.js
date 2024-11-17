// desktop navBar
var desktopNav = document.querySelector(".header-display-desktop");

// init default lang
var defaultLang = 'en';

var currentLang = location.pathname.split("/")[1];
if (currentLang?.length !== 2) 
{
    currentLang = defaultLang;
}

// adjust home page - desktop and mobile
var siteTitles = document.querySelectorAll("#site-title");
for (const siteTitle of siteTitles) 
{
    siteTitle.href = `${document.querySelector("#site-title").href}${currentLang}`;
}

// hide links with /en/ or /el/ urls
var navItems = desktopNav.querySelectorAll('.header-nav-item');
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
var desktopActionsMenu = desktopNav.querySelector(".header-actions");
if (currentLang == "en") {
    desktopActionsMenu.innerHTML = 
        "<div class='language' style='margin-left: 1.5vw;' > <a href='/el/home/' class='lang-el'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gr.svg' alt='Change to Greek' style='width: 20px; margin-top: 4px'/></a></div>"
         + desktopActionsMenu.innerHTML;
}

if (currentLang == 'el') {
    desktopActionsMenu.innerHTML = 
        "<div class='language' style='margin-left: 1.5vw;'> <a href='/en/home/' class='lang-en'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gb.svg' alt='Change to English' style='width: 20px; margin-top: 4px'/></a></div>"
         + desktopActionsMenu.innerHTML;
}