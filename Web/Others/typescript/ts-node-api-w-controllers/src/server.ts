import express, { Express } from "express";
import http from "http";
import { tasksRouter } from "./controllers/tasks.controller";

const router: Express = express();

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
    res.status(200).json({});
  }
  next();
});


router.use("/", tasksRouter);

router.use((req, res, next) => {
  const error = new Error("not found. try http://localhost:6060/posts");
  res.status(404).json({ message: error.message });
});

const httpServer = http.createServer(router);
const PORT: any = process.env.PORT ?? 6060;
httpServer.listen(PORT, () => 
{
  console.log(`server running on http://localhost:${PORT}`);
});
