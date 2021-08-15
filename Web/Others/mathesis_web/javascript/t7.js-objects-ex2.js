class Person {
    constructor(name){
        this.name = name;
    }
    greeting = function(){
        return `Γεια είμαι η ${this.name}.`;
    }
}

const popi = new Person('Πόπη');

console.log(popi.greeting());

console.log(new Person('Πόπη').name);