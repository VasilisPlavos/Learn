import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { catchError, combineLatest, EMPTY, map, Observable, startWith, Subject } from 'rxjs';

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

  private categorySelectedSubject = new Subject<number>();
  categorySelectedAction$ = this.categorySelectedSubject.asObservable();
  
  constructor(private productCategoryService: ProductCategoryService, private productService: ProductService) { }

  products$ : Observable<Product[]> = combineLatest(
    [
      this.productService.productsWithCategory$,
      this.categorySelectedAction$.pipe(startWith(0))
    ])
    .pipe(map(([products, selectedCategoryId]) => products.filter(p => selectedCategoryId ? p.categoryId === selectedCategoryId : true)),
          catchError(err => {
            console.log(err);
            this.errorMessage = err; 
            return EMPTY;
        }))

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
    this.categorySelectedSubject.next(+categoryId);
  }
}
