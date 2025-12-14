const puppeteer = require('puppeteer');
const fs = require('fs');

async function getData() {

    const browser = await puppeteer.launch({
        headless: false, // browser or not browser
        defaultViewport: null
    });

    async function getJobsFromPage(start){
        const page = await browser.newPage();
        const url = `https://gr.indeed.com/jobs?as_and=web+developer&as_phr=&as_any=&as_not=&as_ttl=&as_cmp=&jt=all&st=&radius=25&l=%CE%95%CE%BB%CE%BB%CE%AC%CE%B4%CE%B1&fromage=any&limit=50&start=${start}&sort=date&psf=advsrch&from=advancedsearch`
    
        await page.goto(url);

        await page.waitFor('div.result');
        let jobsOfPage = await page.$$eval('div.result', jobs => {
            return jobs.map(row => {
                const job = {};
    
                const titleElement = row.querySelector('.title');
                job.title = titleElement.innerText.split("\nΝέο")[0];
    
                const compElement = row.querySelector('.company');
                if (compElement) { job.company = compElement.innerText }
    
                job.id = row.getAttribute('data-jk')    
                return job;

            })
        })

        page.close();
        return jobsOfPage;
    }

    async function addJobsPageToJobs(jobsPage){
        for (let i in jobsPage){
            console.log(i);
            jobs.push(jobsPage[i])
        }
        return jobsPage;
    }

    var limit = 750
    var start = 0
    jobs = []
    jobsPage = await getJobsFromPage(start)
    await addJobsPageToJobs(jobsPage)
    
    while (start < limit) {
        start = start + 50;
        jobsPage = await getJobsFromPage(start)
        await addJobsPageToJobs(jobsPage)
    }

    console.log(jobs.length);
    const data = JSON.stringify(jobs);
    fs.writeFile('user.json', data, (err) => {
        if (err) {
            throw err;
        }
        console.log("JSON data is saved.");
    });
}

getData();