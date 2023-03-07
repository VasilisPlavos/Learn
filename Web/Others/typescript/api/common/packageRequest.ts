import { IPackageRequest } from "./iPackageRequest";

export class PackageRequest<T> implements IPackageRequest<T> {
  LogHeader: string;

  RequestData: T;
}
