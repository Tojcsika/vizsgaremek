import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ShelfProductService implements OnInit {
  private readonly shelfProductUrl = Constants.apiRoot + '/shelfproduct';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getShelfProduct(shelfProductId: number) {
    if (shelfProductId == 1) {
      var shelfProduct = {
            Id: 1,
            ProductName: 'Alma',
            ProductWeight: 0.1,
            ShelfProductQuantity: 230,
            TotalWeight: 23,
            ShelfProductWidth: 100,
            ShelfProductLength: 100,
            ShelfProductHeight: 200,
          }
      return shelfProduct;
    }
    return {};
  }
}
