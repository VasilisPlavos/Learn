class Person {
    constructor(name){
        this.firstName = name;
        this.lastName = 'Doe';
    }
    greeting = function(){
        return `Γεια είμαι η ${this.firstName}.`;
    }

    get fullName() {
        return `${this.firstName} ${this.lastName}`;
    }

    set age(years){
        this.dob = years;
    }
}

const popi = new Person('Πόπη');

console.log(popi.greeting());

console.log(new Person('Πόπη').firstName);

popi.fullName = 's';

console.log(popi.fullName);

popi.age = 1;

console.log(popi.dob);

console.log(popi.age);
