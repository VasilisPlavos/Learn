export interface ResponsePackage<T> {
    success: boolean
    data?: T
    message?: string
}