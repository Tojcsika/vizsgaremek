import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css'],
})
export class ProductEditComponent implements OnInit {
  @Input() productId?: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  product: any = {};

  constructor(
    private authService: AuthService,
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
    if (this.productId != null) {
      this.productService.getProduct(this.productId).subscribe((product) => {
        this.product = product;
      });
    }
    else{
      this.product = {
        Id: 0,
        Name: "",
        Width: null,
        Length: null,
        Height: null,
        Weight: null,
        Description: ""
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveProduct() {
    if (this.productId != null) {
      this.productService.updateProduct(this.productId, this.product).subscribe();
    } else {
      this.productService.createProduct(this.product).subscribe();
    }
    this.visible = false;
  }
}
