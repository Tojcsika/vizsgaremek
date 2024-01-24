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
      if (!userAuthenticated) {
        this.authService.login();
      }
      else {
        this.productService.getProducts().subscribe((products) => {
          this.products = products;
        });
      }
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewProduct(productId: number) {
    this.router.navigate(['/product', productId]);
  }

  showEditDialog(productId?: number) {
    this.productEditId = productId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
    this.productService.getProducts().subscribe((products) => {
      this.products = products;
    });
  }

  confirmDelete(productId: number, productName: string) {
    if(confirm(`Are you sure to delete ${productName}?`)) {
      this.productService.deleteProduct(productId).subscribe(() => {
        this.productService.getProducts().subscribe((products) => {
          this.products = products;
        })
      });
    }
  }
}
