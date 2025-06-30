import express, { NextFunction, Request, Response } from 'express';

const app = express();
app.use(express.json());

app.post('/api/hello', (req: Request, res: Response, next: NextFunction) => {
  const name  = req.query.name ?? "unknown";
  res.json({ hello: name });
});

app.get('/api/hello', (req: Request, res: Response, next: NextFunction) => {
  res.json({ status: 'api is running' });
})

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`Server running on port ${PORT}`));