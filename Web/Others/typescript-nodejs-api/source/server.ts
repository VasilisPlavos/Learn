import express, { Express } from "express";
import morgan from "morgan";
import routes from "./routes/posts";
import http from "http";

const router: Express = express();

router.use(morgan("dev"));
router.use(express.urlencoded({ extended: false }));
router.use(express.json());
router.use((req, res, next) => {
  res.header("Access-Control-Allow-Origin", "*");
  res.header(
    "Access-Control-Allow-Headers",
    "origin, X-Requested-With,Content-Type,Accept, Authorization"
  );
  if (req.method === "OPTIONS") {
    res.header("Access-Control-Allow-Methods", "GET PATCH DELETE POST");
    return res.status(200).json({});
  }
  next();
});


router.use("/", routes);

router.use((req, res, next) => {
  const error = new Error("not found. try http://localhost:6060/posts");
  return res.status(404).json({ message: error.message });
});

const httpServer = http.createServer(router);
const PORT: any = process.env.PORT ?? 6060;
httpServer.listen(PORT, () => 
{
  console.log(`server running on http://localhost:${PORT}`);
  console.log("source: https://www.section.io/engineering-education/how-to-create-a-simple-rest-api-using-typescript-and-nodejs/");
});
