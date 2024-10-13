const { ObjectId } = require("mongodb");
const { getDbAsync } = require("../util/database");
const Order = require("./order");
const Product = require("./product");

class User {
  constructor(name, email, cart, id) {
    this.name = name;
    this.email = email;
    this.cart = cart; // { items: [] }
    this._id = id;
  }

  async addOrder() {
    var productIds = this.cart.items.map((i) => {
      return i.productId;
    });
    var products = await Product.findAll(productIds);

    var selectedProducts = [];
    for (const i of this.cart.items) {

      var p = products.filter((x) => {
        return x._id.toString() == i.productId.toString();
      })[0];
      
      selectedProducts.push({
        title: p.title,
        productId: i.productId,
        quantity: i.quantity,
      });
    }

    // const order = { products: prepareProducts(products, this.cart) , userId: this._id}

    const order = { products: selectedProducts, userId: this._id };
    await Order.addOrder(order);

    this.cart = { items: [] };
    const collection = await getCollectionAsync();
    await collection.updateOne(
      { _id: new ObjectId(this._id) },
      { $set: { cart: this.cart } }
    );
  }

  async addToCart(product) {
    // const cartProduct = this.cart.items.findIndex(cp => {
    //   return cp._id === product._id;
    // });

    const updatedCart = {
      items: [{ productId: new ObjectId(product._id), quantity: 1 }],
    };
    const collection = await getCollectionAsync();
    return collection.updateOne(
      { _id: new ObjectId(this._id) },
      { $set: { cart: updatedCart } }
    );
  }

  static async findById(userId) {
    const collection = await getCollectionAsync();
    return await collection.findOne({ _id: new ObjectId(userId) });
  }

  async DeleteFromCart(productId) {
    const updatedCartItems = this.cart.items.filter((x) => {
      return x.productId.toString() !== productId;
    });

    const collection = await getCollectionAsync();
    return collection.updateOne(
      { _id: new ObjectId(this._id) },
      { $set: { cart: { items: updatedCartItems } } }
    );
  }

  async save() {
    const collection = await getCollectionAsync();
    try {
      const result = await collection.insertOne(this);
      console.log(result);
      return result;
    } catch (error) {
      console.log(error);
    }
  }

  // TODO:
  update() {}

  // TODO:
  delete() {}
}

async function getCollectionAsync() {
  const db = await getDbAsync();
  return db.collection("users");
}

module.exports = User;
