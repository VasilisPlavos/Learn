const request = require("request");
const cheerio = require("cheerio");

var url = "https://www.xe.gr/property/poliseis%7Ckatoikies%7Cvoyla-anw-voyla%7C759088757.html?mode=spec";

request(url, (error, response, html) => {
  if (!error && response.statusCode == 200) {
    const $ = cheerio.load(html);
    const phone = "http://www.xe.gr/property/phoneimg?sys_id=759088757";

    const dataContainer = $(".chars-content");
    var str = dataContainer.text();

    while (str.includes("\r\n")) {
      str = str.replace("\r\n", "");
    }
    while (str.includes("  ")) {
      str = str.replace("  ", " ");
    }

    console.log("");
    console.log("Source: ", url);

    console.log("");
    console.log(str);
  }
});