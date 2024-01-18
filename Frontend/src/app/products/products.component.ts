import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ProductService } from '../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit {
  public userAuthenticated = false;
  products: any;
  productEditId: any;
  editVisible: boolean = false;

  constructor(
    private authService: AuthService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      this.products = this.productService.getProducts();
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewProduct(productId: number) {
    this.router.navigate(['/product', productId], { replaceUrl: true });
  }

  showEditDialog(productId?: number) {
    this.productEditId = productId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
  }

  confirmDelete(productId: number, productName: string) {
    if(confirm(`Are you sure to delete ${productName}?`)) {
      // HTTP DELETE storage
      // HA OK
      this.products = this.products.filter(function(product: any) { return product.Id != productId })
    }
  }
}
