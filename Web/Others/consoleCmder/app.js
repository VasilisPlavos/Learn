// run apps from console: example: node app cmd
let command = 'no command set. try: node app cmd';
if (process.argv[2]) command = process.argv[2];
console.log(command);