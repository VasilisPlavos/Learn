import express, { NextFunction, Request, Response } from 'express';

const app = express();
app.use(express.json());
app.use((req: Request, res: Response, next: NextFunction) => {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
})

app.post('/api/hello', (req: Request, res: Response, next: NextFunction) => {
  const name = req.query.name ?? "unknown";
  res.json({ hello: name });
});

app.get('/api/hello', (req: Request, res: Response, next: NextFunction) => {
  res.json({ status: 'api is running' });
})

const PORT = process.env.PORT || 3002;
app.listen(PORT, () => console.log(`Server running on port ${PORT}`));