import express from "express";

const app = express();

const myMw1 = (req, res, next) => {
  console.log("myMw1");
  res.send('ok');
  next();
};

const myMw2 = (req, res, next) => {
  console.log("myMw2");
  next();
};

const myMw3 = (req, res, next) => {
  console.log("myMw3");
  next();
};

// app.use(myMw1, myMw2, myMw3);
// app.use(myMw2);
// app.use(myMw3);

const pipeline = [ myMw1, myMw3, myMw2 ]
app.use(pipeline);

app.listen(3000, () => console.log(`app start at http://localhost:3000`));
