import { catModel, CatRequestPackage, CatRequestResponse } from "./catExample";

var cat: catModel = { sex: "male" };

var requestData: CatRequestResponse = { Cat: cat };
var catRequest: CatRequestPackage = {
  LogHeader: "test",
  RequestData: requestData,
};
console.log(cat, catRequest);