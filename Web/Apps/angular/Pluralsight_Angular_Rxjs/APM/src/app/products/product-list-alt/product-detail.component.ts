import { ChangeDetectionStrategy, Component } from '@angular/core';
import { catchError, EMPTY, map, Subject } from 'rxjs';

import { ProductService } from '../product.service';
import { Supplier } from "../../suppliers/supplier";

@Component({
  selector: 'pm-product-detail',
  templateUrl: './product-detail.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ProductDetailComponent {
  productSuppliers: Supplier[] | null = null;

  constructor(private productService: ProductService) { }

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  product$ = this.productService.selectedProduct$
    .pipe(catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    }));

  pageTitle$ = this.product$.pipe(map(product => 
    product ? `Product Detail for ${product.productName}` : null));
  
  productSuppliers$ = this.productService.selectedProductSuppliers$
    .pipe(catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    }));
  }