### How to run:

1. Go to src\data\databases.ts and uncomment `sequelize.sync` to do the migrations. Important: Don't do this on production
2. npm run dev

### How to add new entity

1. Create the entity in entities
2. add the entity in databases.ts in `createDatabase()`

run with this:
option 1: npx nodemon ./main.ts
option 2: npx tsc main.ts && node main.js
option 3: npm install && npm start