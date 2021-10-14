// https://deta.sh
const { Deta } = require("deta");

// add your Project Key
const deta = Deta("deta.sh project key");

// name your DB
const db = deta.Base("humans-dev");

// use it!
// const a = await db.put({
//     name: "Geordi",
//     title: "Chief Engineer",
//     has_visor: true
// })

db.fetch().then(function (result) {
  console.log(result);
  return result;
});

// db.delete("zjlgta52f1g0");