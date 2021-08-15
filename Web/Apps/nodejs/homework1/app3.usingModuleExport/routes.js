const fs = require('fs');
const appName = 'app3';

const requestHandler = (req, res) => {
    const url = req.url;
    const method = req.method;

    if (url === '/') {
        res.write('<html>');
        res.write('<head><title>Enter message</title></head>');
        res.write(`<body><form action="/message" method="POST"><input type="text" name="message"><button type="submit">Send</button></form></body>`);
        res.write('</html>');
        return res.end();
    }

    if (url === '/message' && method === 'POST') {
        const body = [];
        req.on('data', (chunk) => {
            body.push(chunk);
        });

        return req.on('end', () => {
            const parsedBody = Buffer.concat(body).toString();
            console.log(parsedBody);
            const message = parsedBody.split('=')[1];
            // fs.writeFileSync(`${appName}.message.txt`, message); // <= if you have a huge file system will stuck till finish
            fs.writeFile(`${appName}.message.txt`, message, (err) => {
                console.log(err);
                res.statusCode = 302;
                res.setHeader('Location', '/');
                return res.end();
            });
        });
    }

    res.setHeader('Content-Type', 'text/html');
    res.write('<html>');
    res.write('<head><title>title</title></head>');
    res.write(`<body><h1>hello there</h1></body>`);
    res.write('</html>');
    res.end();
};

// Module export case 1
// module.exports = requestHandler; 

// Module export case 2
// module.exports = {
//     handler: requestHandler,
//     someText: 'Some hard cored text'
// };

// Module export case 3
// module.exports.handler = requestHandler; // exports.handler = requestHandler also works
// module.exports.someText = 'Some hard cored text';

// Module export case 4 - most common
exports.handler = requestHandler;
exports.someText = 'Some hard cored text';