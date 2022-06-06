import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IProduct } from './product';
import { ProductService } from './product.service';

@Component({
  selector: 'pm-products',
  templateUrl: './product-list.component.html',
  // providers: [ProductService],
  // styles: ['thead{color:red}'],
  styleUrls: ['./product-list.component.css']
})

export class ProductListComponent implements OnInit, OnDestroy {
  constructor(private productService : ProductService){}
  pageTitle = 'Product List';
  imageStyle = { width: 50, margin: 2 };
  showImage = false;
  errorMessage : string = '';
  sub!: Subscription;

  private _listFilter: string = '';

  get listFilter(): string {
    return this._listFilter;
  }
  
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredProducts = this.performFilter(value);
    console.log(this.filteredProducts);
  }

  filteredProducts: IProduct[] = [];
  products : IProduct[] = [];

  performFilter(filterBy: string): IProduct[] 
  {
    filterBy = filterBy.toLocaleLowerCase();
    var fProducts = this.products
      .filter((p : IProduct) => 
      {
        return p.productName.toLocaleLowerCase().includes(filterBy)
      });
      
    return fProducts;
  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  ngOnInit(): void {
    this.sub = this.productService.getProducts().subscribe({ 
      next: data => {
        this.products = data;
        this.filteredProducts = this.products;
      },
      error: err => this.errorMessage = err
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  onRatingClicked(message : string) : void {
    this.pageTitle = `Product List: ${message}`
  }
}
