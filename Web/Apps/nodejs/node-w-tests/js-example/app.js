const express = require("express");
const axios = require("axios");

const app = express();

// Route handler for GET /api/v1/:id
app.get("/api/v1/:id", async (req, res) => {
  const { id } = req.params;

  if (id === "2") return res.status(200).json({ result: 2 });

  try {
    const response = await axios.get('https://api.chucknorris.io/jokes/random');
    return res.status(200).json(response.data);
  } catch (error) {
    return res.status(500).json({ error: 'Error fetching joke from external API' });
  }
  
});

module.exports = app;