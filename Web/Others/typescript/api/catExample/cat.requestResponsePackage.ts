import { PackageRequest } from "../common/packageRequest";
import { PackageResponse } from "../common/packageResponse";

import { catModel } from "./cat.model";

export interface CatRequestPackage extends PackageRequest<CatRequestResponse> {}
export class CatResponsePackage extends PackageResponse<CatRequestResponse> {}

export class CatRequestResponse {
  Cat: catModel;
}
