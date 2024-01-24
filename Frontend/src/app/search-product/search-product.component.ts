import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ProductService } from '../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-product',
  templateUrl: './search-product.component.html',
  styleUrls: ['./search-product.component.css'],
})
export class SearchProductComponent implements OnInit {
  public userAuthenticated = false;
  products: any;
  productEditId: any;
  editVisible: boolean = false;
  searchString: any = "";

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

  viewProductAvailability(productId: number){
    this.router.navigate(['/productAvailability', productId]);
  }

  searchProduct(searchString: string) {
    if (searchString == "") {
      this.productService.getProducts().subscribe((products) => {
        this.products = products;
      });
      return;
    }

    this.productService.searchProduct(searchString).subscribe((products) => {
      this.products = products;
    });
  }
}
