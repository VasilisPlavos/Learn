import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BehaviorSubject, combineLatest, from, merge, Observable, Subject, throwError } from 'rxjs';
import { catchError, filter, map, mergeMap, scan, shareReplay, switchMap, tap, toArray } from 'rxjs/operators';

import { Product } from './product';
import { ProductCategoryService } from '../product-categories/product-category.service';
import { SupplierService } from '../suppliers/supplier.service';
import { Supplier } from '../suppliers/supplier';

@Injectable({ providedIn: 'root' })

export class ProductService {
  private productsUrl = 'api/products';
  private suppliersUrl = 'api/suppliers';

  constructor(
    private http: HttpClient,
    private productCategoryService: ProductCategoryService,
    private supplierService: SupplierService
    ) {}

  products$ = this.http.get<Product[]>(this.productsUrl)
    .pipe(
      // map(i => i.price * 1.5),
      map((products) => products.map((product) => 
        ({
          ...product,
          price: product.price ? product.price * 1.5 : undefined,
          searchKey: [product.productName]
        } as Product))),
      // tap((data) => console.log('Products: ', JSON.stringify(data))),
      catchError(this.handleError));

  productsWithCategory$ = combineLatest([
    this.products$,
    this.productCategoryService.productCategories$,
  ]).pipe(
    map(([products, categories]) =>
      products.map((product) =>
      ({
        ...product,
        category: categories.find((c) => product.categoryId == c.id)?.name
      } as Product))),
    shareReplay(1));

  private productSelectedSubject = new BehaviorSubject<number>(0);
  productSelectedAction$ = this.productSelectedSubject.asObservable();

  selectedProduct$ = combineLatest([
    this.productsWithCategory$,
    this.productSelectedAction$,
  ]).pipe(
    map(([products, selectedProductId]) => 
      products.find((p) => p.id === selectedProductId)),
    tap((x) => console.log('selectedProduct', x)),
    shareReplay(1));

  selectedProductChanged(productId: number): void {
    this.productSelectedSubject.next(productId);
  }

  private productInsertedSubject = new Subject<Product>();
  productInsertedAction$ = this.productInsertedSubject.asObservable();

  productsWithAdd$ = merge(this.productsWithCategory$, this.productInsertedAction$)
    .pipe(
      scan((acc: Product[], value: any) => [...acc, value]), 
      map((x) => x as Product[]));

  // Get It All approach
  // selectedProductSuppliers$ = combineLatest([this.selectedProduct$, this.supplierService.suppliers$])
  //   .pipe(map(([selectedProduct, suppliers]) => 
  //     suppliers.filter(supplier => selectedProduct?.supplierIds?.includes(supplier.id))));
  
  // Just in Time approach
  selectedProductSuppliers$ = this.selectedProduct$
    .pipe(
      filter(product => Boolean(product)),
      switchMap(product => {
        var supplierIds: number[] = [];
        if (product?.supplierIds?.length) supplierIds = product.supplierIds;
        return from(supplierIds)
          .pipe(
            mergeMap(supplierId => this.http.get<Supplier>(`${this.suppliersUrl}/${supplierId}`)),
            toArray(),
            tap(d => console.log(d))
            )
          })
    );

  addProduct(newProduct?: Product) {
    newProduct = newProduct || this.fakeProduct();
    this.productInsertedSubject.next(newProduct);
  }

  private fakeProduct(): Product {
    return {
      id: 42,
      productName: 'Another One',
      productCode: 'TBX-0042',
      description: 'Our new product',
      price: 8.9,
      categoryId: 3,
      // category: 'Toolbox',
      quantityInStock: 30,
    };
  }

  private handleError(err: HttpErrorResponse): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.message}`;
    }
    console.error(err);
    return throwError(() => errorMessage);
  }
}