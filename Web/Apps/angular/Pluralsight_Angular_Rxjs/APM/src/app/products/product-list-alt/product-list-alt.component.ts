import { ChangeDetectionStrategy, Component } from '@angular/core';
import { catchError, EMPTY, Observable } from 'rxjs';

import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'pm-product-list',
  templateUrl: './product-list-alt.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ProductListAltComponent {
  pageTitle = 'Products';
  errorMessage = '';

  constructor(private productService: ProductService) { }

  products$ : Observable<Product[]> = this.productService.productsWithCategory$
    .pipe(catchError(err => 
      {
        console.log(err);
        this.errorMessage = err; 
        return EMPTY;
      }));

  selectedProduct$ = this.productService.selectedProduct$;

  onSelected(productId: number): void {
    this.productService.selectedProductChanged(productId);
  }
}
