import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ShelfProductService implements OnInit {
  private readonly shelfProductUrl = Constants.apiRoot + '/ShelfProduct';
  private readonly productUrl = Constants.apiRoot + '/Product';

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getShelfProduct(shelfProductId: number) {
    return this.http.get(this.shelfProductUrl + `/${shelfProductId}`);
  }

  public getProductShelves(productId: number) {
    return this.http.get(this.productUrl + `/${productId}/ProductShelves`);
   }

  public createShelfProduct(shelfProduct: any) {
    return this.http.post(this.shelfProductUrl, shelfProduct);
  }

  public updateShelfProduct(shelfProductId: number, shelfProduct: any) {
    const url = this.shelfProductUrl + `/${shelfProductId}`;
    return this.http.put(url, shelfProduct);
  }

  public deleteShelfProduct(shelfProductId: number) {
    const url = this.shelfProductUrl + `/${shelfProductId}`;
    return this.http.delete(url);
  }
}
