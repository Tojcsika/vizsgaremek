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
  @Input() shelfId!: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  shelfProduct: any = {};
  selectedProduct: any = {};
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
      this.shelfProductService.getShelfProduct(this.shelfProductId).subscribe((shelfProduct) => {
        this.shelfProduct = shelfProduct;
        this.shelfProduct.shelfId = this.shelfId;
      });
    }
    else {
      this.productService.getProducts().subscribe((products) => {
        this.products = products;
      });
      this.shelfProduct = {
        height: null,
        id: 0,
        length: null,
        name: "",
        productId: null,
        quantity: null,
        totalWeight: null,
        weight: null,
        width: null,
        shelfId: this.shelfId,
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveShelfProduct() {
    if (this.shelfProductId != null) {
      this.shelfProductService.updateShelfProduct(this.shelfProductId, this.shelfProduct).subscribe();
    } else {
      this.shelfProductService.createShelfProduct(this.shelfProduct).subscribe();
    }
    this.visible = false;
  }

  updateTotalWeight() {
    this.shelfProduct.totalWeight = this.shelfProduct.quantity * this.shelfProduct.weight;
  }

  productSelected(selectedEvent: any) {
    this.shelfProduct.name = selectedEvent.value.name;
    this.shelfProduct.weight = selectedEvent.value.weight;
    this.shelfProduct.productId = selectedEvent.value.id;
    this.shelfProduct.shelfId = this.shelfId;
  }
}
