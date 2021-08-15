const http = require('http');

const person = {
    name: 'vasilis'
}

const server = http.createServer((req, res) => {
    //console.log(req.url, req.method, req.headers);

    // This returns html code
    res.setHeader('Content-Type', 'text/html');
    res.write('<html>');
    res.write('<head><title>title</title></head>');
    res.write(`<body><h1>hello ${person.name}</h1></body>` );
    res.write('</html>');
    res.end();

    // // This returns json
    // res.setHeader('Content-Type', 'application/json');
    // res.end(JSON.stringify(person));
});

server.listen(3000);