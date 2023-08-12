import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ProductDetailComponent } from '../product-detail/product-detail.component';
import { Product } from '../product';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements AfterViewInit {

  @ViewChild(ProductDetailComponent) productDetail:
    ProductDetailComponent | undefined;

  selectedProduct: Product | undefined;

  products: Product[] =
    [
      { name: 'Webcam', price: 100 },
      { name: 'Microphone', price: 200 },
      { name: 'Wireless Keyboard', price: 85 }
    ];

  onBuy(name: string) {
    window.alert(`You just bought ${this.selectedProduct?.name}!`);
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
