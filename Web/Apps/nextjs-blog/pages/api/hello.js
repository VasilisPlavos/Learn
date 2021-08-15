export default function handler(req, res) {
  const name = `Hello ${req.body.name}`
  res.status(200).json({ text: name })
}

// body example

// {
//   "name": "mary"
// }