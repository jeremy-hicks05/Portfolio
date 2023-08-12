import {
  Component, Input, OnInit, Output,
  EventEmitter, OnChanges, SimpleChanges, SimpleChange

} from '@angular/core';

import { Product } from '../product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})

export class ProductDetailComponent implements OnChanges {

  constructor() {
    console.log(`Name is ${this.product?.name} in the constructor`);
  }

  @Input()
  product: Product | undefined;

  @Output()
  bought = new EventEmitter<string>();

  buy() {
    this.bought.emit(this.product?.name);
  }

  get productName(): string | undefined {
    console.log(`Get ${this.product?.name}`);
    return this.product?.name;
  }

  ngOnChanges(changes: SimpleChanges): void {
    const product = changes[`product`];
    if (!product.isFirstChange()) {
      const oldValue = product.previousValue.name;
      const newValue = product.currentValue.name;
      console.log(`Product changed from ${oldValue} to ${newValue}`);
    }
  }
}
