import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ShelfProductService } from '../services/shelf-product.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-shelf-product-edit',
  templateUrl: './shelf-product-edit.component.html',
  styleUrls: ['./shelf-product-edit.component.css'],
})
export class ShelfProductEditComponent implements OnInit {
  @Input() shelfProductId?: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  shelfProduct: any = {};
  selectedProduct: any = null;
  products: any = [];

  constructor(
    private authService: AuthService,
    private shelfProductService: ShelfProductService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  onShow() {
    if (this.shelfProductId != null) {
      this.shelfProduct = this.shelfProductService.getShelfProduct(this.shelfProductId);
    }
    else{
      this.products = this.productService.getProducts();
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveShelfProduct() {
    // HTTP POST mentéshez
    this.visible = false;
  }

  updateTotalWeight() {
    this.shelfProduct.TotalWeight = this.shelfProduct.ShelfProductQuantity * this.shelfProduct.ProductWeight;
  }

  productSelected(selectedEvent: any) {
    this.shelfProduct.ProductName = selectedEvent.value.Name;
    this.shelfProduct.ProductWeight = selectedEvent.value.Weight;
    this.shelfProduct.ProductId = selectedEvent.value.Id;
  }
}
