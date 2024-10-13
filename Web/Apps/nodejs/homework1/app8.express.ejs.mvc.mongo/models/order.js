const { ObjectId } = require("mongodb");
const { getDbAsync } = require("../util/database");

class Order {
  static async addOrder(order) {
    const collection = await getCollectionAsync();
    var result = await collection.insertOne(order);
    return result;
  }

  static async getOrders(userId) {
    const collection = await getCollectionAsync();
    var result = await collection
      .find({ userId: new ObjectId(userId) })
      .toArray();
    return result;
  }
}

async function getCollectionAsync() {
  const db = await getDbAsync();
  return db.collection("orders");
}

module.exports = Order;
