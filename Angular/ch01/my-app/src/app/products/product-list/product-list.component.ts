import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ProductDetailComponent } from '../product-detail/product-detail.component';
import { Product } from '../product';
import { ProductsService } from '../products.service'

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  providers: [ProductsService]
})
export class ProductListComponent implements OnInit, AfterViewInit {

  private productService: ProductsService;

  constructor() {
    this.productService = new ProductsService();
  }

  @ViewChild(ProductDetailComponent)
  productDetail: ProductDetailComponent | undefined;

  selectedProduct: Product | undefined;

  products: Product[] =
    [];

  onBuy(name: string) {
    window.alert(`You just bought ${this.selectedProduct?.name}!`);
  }

  ngOnInit(): void {
    this.products = this.productService.getProducts();
  }

  ngAfterViewInit(): void {
    if (this.productDetail) {
      console.log(`From AfterViewInit ${this.productDetail.product}`);
    }
  }

  trackByProducts(index: number, name: string): string {
    return name;
  }
}
