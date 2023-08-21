import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ProductDetailComponent } from '../product-detail/product-detail.component';
import { Product } from '../product';
import { ProductsService } from '../products.service'
import { Subscription, Observable } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  providers: [ProductsService]
})
export class ProductListComponent implements OnInit, OnDestroy, AfterViewInit {

  private productService: ProductsService;

  private productsSub: Subscription | undefined;

  constructor() {
    this.productService = new ProductsService();
  }

  @ViewChild(ProductDetailComponent)
  productDetail: ProductDetailComponent | undefined;

  selectedProduct: Product | undefined;

  products$: Observable<Product[]> | undefined;

  onBuy(name: string) {
    window.alert(`You just bought ${this.selectedProduct?.name}!`);
  }

  ngOnInit(): void {
    this.getProducts();
  }

  ngOnDestroy(): void {
    this.productsSub?.unsubscribe();
  }

  ngAfterViewInit(): void {
    if (this.productDetail) {
      console.log(`From AfterViewInit ${this.productDetail.product}`);
    }
  }

  trackByProducts(index: number, name: string): string {
    return name;
  }

  private getProducts() {
    this.productsSub = this.productService.getProducts().
      subscribe(products => {
        this.products$ = this.productService.getProducts();
      });
  }
}
