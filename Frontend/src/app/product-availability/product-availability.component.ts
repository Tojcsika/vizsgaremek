import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ShelfProductService } from '../services/shelf-product.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-availability',
  templateUrl: './product-availability.component.html',
  styleUrls: ['./product-availability.component.css'],
})
export class ProductAvailabilityComponent implements OnInit {
  public userAuthenticated = false;
  productId: any;
  product: any;
  productAvailabilities: any = {};

  constructor(
    private authService: AuthService,
    private shelfProductService: ShelfProductService,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {

    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      if (!userAuthenticated) {
        this.authService.login();
      }
      else {
        this.route.params.subscribe((params) => {
          this.productId = params['productId'];
        });
        if (this.productId != null) {
          this.shelfProductService.getProductShelves(this.productId).subscribe((productAvailabilities) => {
            this.productAvailabilities = productAvailabilities;
          });
          this.productService.getProduct(this.productId).subscribe((product) => {
            this.product = product;
          });
        }
      }
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewShelf(shelfId: number) {
    this.router.navigate(['/shelves', shelfId]);
  }
}
