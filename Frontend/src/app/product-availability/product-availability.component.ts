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
    this.route.params.subscribe((params) => {
      this.productId = params['productId'];
    });

    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      if (this.productId != null) {
        this.productAvailabilities = this.shelfProductService.getProductShelves(this.productId);
        this.product = this.productService.getProduct(this.productId);

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
