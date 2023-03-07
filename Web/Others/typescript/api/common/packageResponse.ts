import { IPackageResponse } from "./iPackageResponse";

export class PackageResponse<T> implements IPackageResponse<T> {
  LogHeader: string;
  ResponseData: T;
}
