const fs = require('fs')
var cron = require('node-cron');

toWrite = ''
toWrite = toWrite + ' ' + 'pos: 1\r\n'
c = 0;

cron.schedule('* * * * * *', () => {
    toWrite = toWrite + ' ' + 'pos: 2\r\n';
});

for (let i = 0; i < 100000; i++) {
    toWrite = toWrite + ' ' + 'loop\r\n';

}

toWrite = toWrite + ' ' + 'pos: 3\r\n';

cron.schedule('* * * * * *', () => {
    toWrite = toWrite + ' ' + 'pos: 4\r\n';
});


cron.schedule('* * * * * *', () => {
    toWrite = toWrite + ' ' + 'pos: 5\r\n';
});


cron.schedule('* * * * *', () => {
    toWrite = toWrite + ' ' + 'waiting for 1 minute\r\n';
    const data = new Uint8Array(Buffer.from(toWrite));
    fs.writeFile('message.txt', data, (err) => {
        if (err) throw err;
        console.log('The file has been saved!');
    });

});

