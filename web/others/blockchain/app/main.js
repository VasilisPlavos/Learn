const { SHA256 } = require("crypto-js");

class Block{
    constructor(index, timestamp, data, previousHash = '')
    {
        this.index = index;
        this.timestamp = timestamp;
        this.data = data;
        this.previousHash = previousHash;
        this.hash = this.calculateHash();
    }

    calculateHash(){
        var a = SHA256(this.index + this.previousHash + this.timestamp + JSON.stringify(this.data)).toString();
        console.log(a);
        return a;
    }
}

class Blockchain{
    constructor(){
        this.chain = [this.createGenesisBlock()];
    }

    createGenesisBlock(){
        return new Block(0, "01/01/2017", "Genesis Block", "0");
    }

    getLastBlock(){
        return this.chain[this.chain.length - 1];
    }

    addBlock(newBlock){
        newBlock.previousHash = this.getLastBlock().hash;
        newBlock.hash = newBlock.calculateHash();
        this.chain.push(newBlock);
        console.log(newBlock);
    }

    isChainValid(){
        for (let i = 1; i < this.chain.length; i++) {
            const curentBlock = this.chain[i];
            const previousBlock = this.chain[i-1];

            if (curentBlock.hash !== curentBlock.calculateHash()){ return false; }
            if (curentBlock.previousHash !== previousBlock.hash){ return false; }
            return true;
        }
    }
}

let sCoin = new Blockchain();
sCoin.addBlock(new Block(1, "10/07/2017", { amount: 4 }));
sCoin.addBlock(new Block(2, "10/07/2017", { amount: 10 }));
console.log("valid?", sCoin.isChainValid());

var tempChain = sCoin.chain[1];
sCoin.chain[1].data = { amount: 100 };
sCoin.chain[1].hash = tempChain.hash;
sCoin.chain[1].previousHash = tempChain.previousHash;

console.log("valid?",sCoin.isChainValid());