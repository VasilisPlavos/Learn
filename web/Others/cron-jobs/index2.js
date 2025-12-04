var cron = require('node-cron');

console.log('pos: 1\r\n')

cron.schedule('* * * * * *', () => {
    console.log('pos: 2\r\n');
});

for (let i = 0; i < 100000; i++) {
    console.log('loop\r\n');
}

console.log('pos: 3\r\n');

cron.schedule('* * * * * *', () => {
    console.log('pos: 4\r\n');
});

cron.schedule('* * * * * *', () => {
    console.log('pos: 5\r\n');
});

cron.schedule('* * * * *', () => {
    console.log('waiting for 1 minute\r\n');
});

