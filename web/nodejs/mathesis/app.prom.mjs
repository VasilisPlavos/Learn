import fs from "fs/promises";

const fileName = "myBooks.txt";
const fd = await fs.open(fileName, 'a+');
await fd.write('{ title: "Start wars", author: "Σ Λεμ" }\n');
await fd.close();
const data = await fs.readFile(fileName, "utf-8");
console.log(data);