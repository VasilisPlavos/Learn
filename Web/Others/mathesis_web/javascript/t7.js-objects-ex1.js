function createPerson(name){
    let obj = {};
    obj.name = name;
    obj.greeting = function(){

        let printName = this.name;
        if (printName.slice(-1) === 'ς'){
            printName = printName.slice(0, printName.length-1)
        } else {
            printName = printName;
        }     
        return `Γεια με λένε ${printName}.`;
    }
    return obj;
}

let kostas = createPerson('Βασίλης');

console.log(kostas.greeting());
