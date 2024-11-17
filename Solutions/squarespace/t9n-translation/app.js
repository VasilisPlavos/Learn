// init default lang
var defaultLang = 'en';
var currentLang = location.pathname.split("/")[1];
if (currentLang?.length !== 2) 
{
    currentLang = defaultLang;
}

// adjust home page - desktop and mobile
for (const siteTitle of document.querySelectorAll("#site-title")) 
{
    siteTitle.href = `${location.origin}/${currentLang}/home`;
}

// MOBILE Process
fixMenu("mobile");
fixMenu("desktop");

function fixMenu(device) 
{

    // select navBar
    var navBar = device == "desktop" ? document.querySelector(".header-display-desktop") : document.querySelector('.header-menu-nav');

    // hide links with /en/ or /el/ urls
    var navItems = device == "desktop" ? navBar.querySelectorAll('.header-nav-item') : navBar.querySelectorAll('.header-menu-nav-item');
    for (const item of navItems)     
    {
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

    // change language menu
    var actionsMenu = device == "desktop" ? navBar.querySelector(".header-actions") : navBar.querySelector(".header-menu-actions");
    if (device == "desktop") 
    {
        if (currentLang == "en") {
            actionsMenu.innerHTML = 
                "<div class='language' style='margin-left: 1.5vw;' > <a href='/el/home/' class='lang-el'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gr.svg' alt='Change to Greek' style='width: 20px; margin-top: 4px'/></a></div>"
                 + actionsMenu.innerHTML;
        }
        
        if (currentLang == 'el') {
            actionsMenu.innerHTML = 
                "<div class='language' style='margin-left: 1.5vw;'> <a href='/en/home/' class='lang-en'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gb.svg' alt='Change to English' style='width: 20px; margin-top: 4px'/></a></div>"
                 + actionsMenu.innerHTML;
        }
    } 
    
    else 
    {
        if (currentLang == "en") {
            actionsMenu.innerHTML = 
                "<div class='language'> <a href='/el/home/' class='lang-el'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gr.svg' alt='Change to Greek' style='width: 22px; margin-top: 5px'/></a></div>"
                 + actionsMenu.innerHTML;
        }
        
        if (currentLang == 'el') {
            actionsMenu.innerHTML = 
                "<div class='language'> <a href='/en/home/' class='lang-en'><img src='https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/2.6.0/flags/4x3/gb.svg' alt='Change to English' style='width: 22px; margin-top: 5px'/></a></div>"
                 + actionsMenu.innerHTML;
        }
    }
}