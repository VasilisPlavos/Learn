class Meal {
    constructor(food) {
      this.food = food;
    }
  
    // Prototype Method
    eat() {
      return 'eating...';
    }
  
    // Static Method
    static utensil() {
      return 'utel..';
    }
  }

  const du = new Meal('sss');
  console.log(du.eat())