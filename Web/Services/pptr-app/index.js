const puppeteer = require('puppeteer');

async function getData() {

    const browser = await puppeteer.launch({
        headless: false, // browser or not browser
        defaultViewport: null
    });

    const page = await browser.newPage();

    const url = 'https://nh.craigslist.org/search/sss?query=bunnies&sort=rel&hasPic=1';
    //    const url = 'https://gr.indeed.com/jobs?as_and=web+developer&as_phr=&as_any=&as_not=&as_ttl=&as_cmp=&jt=all&st=&radius=25&l=%CE%95%CE%BB%CE%BB%CE%AC%CE%B4%CE%B1&fromage=any&limit=50&sort=date&psf=advsrch&from=advancedsearch'

    await page.goto(url);
    await page.waitFor('.result-row');
    const results = await page.$$eval('.result-row', rows => {
        return rows;
    })

    console.log(results);



    // const results = await page.$$eval('.result-row', rows => {
    //     return rows.map( row => {
    //         const properties = {};
    //         const titleElement = row.querySelector('.result-title');
    //         properties.title = titleElement.innerText;
    //         properties.url = titleElement.getAttribute('href');
    //         const priceElement = row.querySelector('.result-price');
    //         properties.price = priceElement ? priceElement.innerText : '';
    //         const imageElement = row.querySelector('.swipe [data-index="0"] img');
    //         properties.imageUrl = imageElement ? imageElement.src : '';
    //         return properties;
    //     });
    // })

    browser.close();
}

getData();