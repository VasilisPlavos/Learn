import { Component, ChangeDetectionStrategy } from '@angular/core';
import { catchError, EMPTY, Observable } from 'rxjs';

import { Product } from './product';
import { ProductCategory } from '../product-categories/product-category';
import { ProductService } from './product.service';

@Component({
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class ProductListComponent {
  pageTitle = 'Product List';
  errorMessage = '';
  categories: ProductCategory[] = [];

  products$ : Observable<Product[]> = this.productService.productsWithCategory$
  .pipe(catchError(err => 
    {
      console.log(err);
      this.errorMessage = err; 
      return EMPTY;
    }));

  constructor(private productService: ProductService) { }

  onAdd(): void {
    console.log('Not yet implemented');
  }

  onSelected(categoryId: string): void {
    console.log('Not yet implemented');
  }
}
