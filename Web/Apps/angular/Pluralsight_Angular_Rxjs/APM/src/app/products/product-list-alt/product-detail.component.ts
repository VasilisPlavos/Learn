import { ChangeDetectionStrategy, Component } from '@angular/core';
import { catchError, EMPTY, Subject } from 'rxjs';

import { ProductService } from '../product.service';
import { Supplier } from "../../suppliers/supplier";

@Component({
  selector: 'pm-product-detail',
  templateUrl: './product-detail.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ProductDetailComponent {
  pageTitle = 'Product Detail';
  productSuppliers: Supplier[] | null = null;

  constructor(private productService: ProductService) { }

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();

  product$ = this.productService.selectedProduct$
    .pipe(catchError(err => {
      this.errorMessageSubject.next(err);
      return EMPTY;
    }));
  }