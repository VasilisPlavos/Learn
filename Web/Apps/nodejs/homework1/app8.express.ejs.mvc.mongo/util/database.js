const { MongoClient, ServerApiVersion } = require("mongodb");
require('dotenv').config();

// Create a MongoClient with a MongoClientOptions object to set the Stable API version
let client = new MongoClient(process.env.MONGO_URL, {
  serverApi: {
    version: ServerApiVersion.v1,
    strict: true,
    deprecationErrors: true,
  },
});

const mongoConnect = async () => {
  try {
    // Connect the client to the server	(optional starting in v4.7)
    await client.connect();
    // Send a ping to confirm a successful connection
    await client.db("admin").command({ ping: 1 });
    console.log(
      "Pinged your deployment. You successfully connected to MongoDB!"
    );
  } catch (error) {
    console.log(error);
  } finally {
    // Ensures that the client will close when you finish/error
    await client.close();
  }
};
// run().catch(console.dir);

const getDbAsync = async () => {
  await client.connect();
  if (client.db("shop")) return client.db("shop");
  throw new Error("No database found!");
};

exports.mongoConnect = mongoConnect;
exports.getDbAsync = getDbAsync;
