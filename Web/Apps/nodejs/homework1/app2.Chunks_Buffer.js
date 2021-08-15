const http = require('http');
const fs = require('fs');
const appName = 'app2';

const person = {
    name: 'vasilis'
}

const server = http.createServer((req, res) => {
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
    res.write(`<body><h1>hello ${person.name}</h1></body>`);
    res.write('</html>');
    res.end();
});

server.listen(3000);