
const app = require('./app');

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`server running. Check http://localhost:${PORT}/api/v1/2`);
});

