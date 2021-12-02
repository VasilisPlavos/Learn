// Client-side solution
CheckIfImageExist();

async function CheckIfImageExist() {
    var url = '';
    var pathName = '';

    // pathname = '/dF5SId3UHWd.svg';
    // url = await getReceiptUrl(pathname);
    // if (url){console.log(`it works: ${url}`);}
    
    var receiptImage = document.querySelector("#receiptImg");
    if (receiptImage) {
        pathName = new URL(receiptImage.src).pathname;
        url = await getReceiptUrl(pathName);
        if (!url) console.log("picture not found");
        receiptImage.src = url;
    }

    async function getReceiptUrl(pathName) {
        var testUrl = `https://example.com/broken-image${pathName}`;
        var existImage = await imageExists(testUrl);
        if (existImage) { return testUrl; }

        testUrl = `https://i.giphy.com/media/l0K4mbH4lKBhAPFU4${pathName}`;
        existImage = await imageExists(testUrl);
        if (existImage) { return testUrl; }

        console.log(`error with: ${pathName}`);
        return null;
    }

    async function imageExists(imgUrl) {
        if (!imgUrl) { return false; }

        console.log(`testing ${imgUrl}`);
        return new Promise(res => {
            const image = new Image();
            image.onload = () => res(true);
            image.onerror = () => res(false);
            image.src = imgUrl;
        });
    }
}