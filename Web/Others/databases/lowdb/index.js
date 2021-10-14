// https://github.com/typicode/lowdb
import { Low, JSONFile } from "lowdb";
const db = new Low(new JSONFile("db.json"));
await db.read();
if (db.data == null) db.data = { posts: [] };
db.data.posts.push({ id: 1, title: 'lowdb is awesome' })
await db.write();
console.log(db.data);