# Notes
I have some notes here and some things to remember.

### Install Angular:
1. Download [Node.js](https://nodejs.org)
2. open CMD (Admin)
3. ```npm install -g @angular/cli@latest```


### Angular Material Commands I use
```
ng new app3 --routing --style=scss
cd app1
npm install
npm install --save @angular/material @angular/cdk @angular/animations
npm install hammerjs
npm install
ng serve --open
```

### Things to Remember
- ```ng add @angular/material``` - other way to add Angular Material
- **Important:** Check Material Design Schematics https://material.angular.io/guide/schematics
- ```ng generate @angular/material:material-nav --name <component-name>``` - The navigation schematic will create a new component that includes a toolbar with the app name and the side nav responsive based on Material breakpoints.
- ```ng generate @angular/material:material-dashboard --name <component-name>``` - The dashboard schematic will create a new component that contains a dynamic grid list of cards.
- ```ng generate @angular/material:material-table --name <component-name>``` - The table schematic will create a new table component pre-configured with a datasource for sorting and pagination.


# Above this line content is Temporary

### Use Bootstrap (or another css)
- ```npm install --save bootstrap@3```
- update angular.json with .css file at styles
- ```ng serve```

### Commands to Remember
- ```ng generate component servers``` (create a component named servers) **or** ```ng g c servers```
- ```ng new app1 --routing --style=scss``` it creates an app named app1, including routing template and replacing css with scss


### Important Projects - Exercises we finished:
- **my-first-app** - you are righting something at a form and you can see it dynamically


### About the Repo
In this repo I will upload many small **Angular Excercises**.


###### Made with ❤️ by [Vasilis Plavos](https://www.linkedin.com/in/vasilisplavos/)
