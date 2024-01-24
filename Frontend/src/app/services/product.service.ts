import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ProductService implements OnInit {
  private readonly productUrl = Constants.apiRoot + '/Product';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getProducts() {
    return this.http.get(this.productUrl);
  }

  public getProduct(productId: number){
    return this.http.get(this.productUrl + `/${productId}`);
  }

  public searchProduct(searchString: string) {
    return this.http.get(this.productUrl + `/Search?searchString=${searchString}`);
  }

  public createProduct(product: any) {
    return this.http.post(this.productUrl, product);
  }

  public updateProduct(productId: number, product: any) {
    const url = this.productUrl + `/${productId}`;
    return this.http.put(url, product);
  }

  public deleteProduct(productId: number) {
    const url = this.productUrl + `/${productId}`;
    return this.http.delete(url);
  }
}
