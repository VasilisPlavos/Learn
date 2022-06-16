import { Component, ChangeDetectionStrategy } from '@angular/core';
import { catchError, EMPTY, map, Observable } from 'rxjs';

import { Product } from './product';
import { ProductCategory } from '../product-categories/product-category';
import { ProductCategoryService } from '../product-categories/product-category.service';
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
  selectedCategoryId : number | undefined;
  constructor(private productCategoryService: ProductCategoryService, private productService: ProductService) { }

  products$ : Observable<Product[]> = this.productService.productsWithCategory$
  .pipe(catchError(err => 
    {
      console.log(err);
      this.errorMessage = err; 
      return EMPTY;
    }));

    productsSimpleFilter$ = this.productService.productsWithCategory$
      .pipe(map(products => 
        products.filter(product => 
          this.selectedCategoryId ? product.categoryId === this.selectedCategoryId : true)));

    categories$ : Observable<ProductCategory[]> = this.productCategoryService.productCategories$
      .pipe(catchError(err => {
        console.log(err);
        this.errorMessage = err; 
        return EMPTY;
      }));

  onAdd(): void {
    console.log('Not yet implemented');
  }

  onSelected(categoryId: string): void {
    this.selectedCategoryId = +categoryId;
    this.productsSimpleFilter$ = this.productService.productsWithCategory$
    .pipe(map(products => 
      products.filter(product => 
        this.selectedCategoryId ? product.categoryId === this.selectedCategoryId : true)));

  this.categories$ = this.productCategoryService.productCategories$
    .pipe(catchError(err => {
      console.log(err);
      this.errorMessage = err; 
      return EMPTY;
    }));
  }
}
