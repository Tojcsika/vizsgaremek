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

  // public getProductsOnShelf(shelfId: number) {
  //   // this.http.get(this.shelfUrl).subscribe((storages: any) => {
  //   //   return storages;
  //   // });
  //   if (shelfId == 1) {
  //     return [
  //       {
  //         Id: 1,
  //         StorageRackId: 1,
  //         Level: 1,
  //         Width: 100,
  //         Length: 200,
  //         Height: 300,
  //         WeightLimit: 200,
  //       },
  //       {
  //         Id: 2,
  //         StorageRackId: 1,
  //         Level: 2,
  //         Width: 100,
  //         Length: 200,
  //         Height: 450,
  //         WeightLimit: 200,
  //       },
  //     ];
  //   }
  //   return []
  // }

  public getProductShelves(productId: number) {
    return [
      {
        Id: 1,
        ProductId: 1,
        ProductName: 'Alma',
        ProductWeight: 0.1,
        ShelfId: 1,
        ShelfProductQuantity: 230,
        StorageName: 'Raktár 1',
        StorageRackRow: 1,
        StorageRackRowPosition: 1,
        ShelfLevel: 1
      },
      {
        Id: 2,
        ProductId: 1,
        ProductName: 'Alma',
        ProductWeight: 0.1,
        ShelfId: 2,
        ShelfProductQuantity: 20,
        StorageName: 'Raktár 2',
        StorageRackRow: 2,
        StorageRackRowPosition: 3,
        ShelfLevel: 4
      }
    ]
  }
}
