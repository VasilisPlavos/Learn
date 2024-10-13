const { MongoClient, ServerApiVersion } = require("mongodb");

const uri =
  "mongodb+srv://bil:7XvtRxIjUEmKOawf@free-cluster.0pqqh.mongodb.net/?retryWrites=true&w=majority&appName=free-cluster";

// Create a MongoClient with a MongoClientOptions object to set the Stable API version
let client = new MongoClient(uri, {
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
