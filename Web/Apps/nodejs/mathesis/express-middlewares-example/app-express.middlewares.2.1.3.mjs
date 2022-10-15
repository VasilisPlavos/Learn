import express from "express";

const app = express();

const myMw1 = (req, res, next) => {
  console.log("myMw1");
  // res.send("ok1");
  next();
};

const myMw2 = (req, res, next) => {
  console.log("myMw2");
  res.send("ok2");
  next();
};

const myMw3 = (req, res, next) => {
  res.send("ok3");
  console.log("myMw3");
  next();
};

// app.use(myMw1, myMw2, myMw3);
// app.use(myMw2);
// app.use(myMw3);

const pipeline = [myMw1, myMw3, myMw2];
// app.use(pipeline);
// app.get('/', pipeline);
// app.get('/', myMw1, myMw3, myMw2);

app.get("/3", myMw3);
app.get("/", myMw2,myMw1);
app.use(myMw1);

app.listen(3000, () => console.log(`app start at http://localhost:3000`));
