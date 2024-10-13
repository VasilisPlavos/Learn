const { ObjectId } = require("mongodb");
const { getDbAsync } = require("../util/database");

async function getCollectionAsync() {
  const db = await getDbAsync();
  return db.collection("products");
}

class Product {
  constructor(title, price, description, imageUrl, id, userId) {
    this.title = title;
    this.price = price;
    this.description = description;
    this.imageUrl = imageUrl;
    this._id = id;
    this.userId = userId;
  }
  

  static async deleteById(productId) {
    const collection = await getCollectionAsync();
    await collection.deleteOne({ _id: new ObjectId(productId) });
    return true;
  }

  static async fetchAll() {
    const collection = await getCollectionAsync();
    return await collection.find().toArray();
  }

  static async findAll(productIds){
    const collection = await getCollectionAsync();
    return await collection.find({ _id: { $in: productIds } }).toArray()

  }

  static async findById(productId) {
    const collection = await getCollectionAsync();
    var product = await collection.findOne({ _id: new ObjectId(productId) });
    return product;
  }

  async saveAsync() {
    const collection = await getCollectionAsync();
    try {
      const result = await collection.insertOne(this);
      console.log(result);
      return result;
    } catch (error) {
      console.log(error);
    }
  }

  async updateAsync() {
    const collection = await getCollectionAsync();
    try {
      const result = await collection.updateOne(
        { _id: new ObjectId(this._id) },
        { $set: this }
      );
      console.log(result);
      return result;
    } catch (error) {
      console.log(error);
    }
  }
}

module.exports = Product;
