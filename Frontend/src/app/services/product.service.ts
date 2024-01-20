import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ProductService implements OnInit {
  private readonly productUrl = Constants.apiRoot + '/product';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getProducts() {
    return [
      {
        Id: 1,
        Name: "Toy Train",
        Width: 10,
        Length: 20,
        Height: 5,
        Weight: 50,
        Description: "Wooden train set"
      },
      {
        Id: 2,
        Name: "Doll",
        Width: 8,
        Length: 15,
        Height: 12,
        Weight: 30,
        Description: "Classic doll"
      },
      {
        Id: 3,
        Name: "Teddy Bear",
        Width: 12,
        Length: 10,
        Height: 8,
        Weight: 20,
        Description: "Soft and cuddly bear"
      }
    ]
  }

  public getProduct(productId: number){
    var product: any = {};
    if (productId == 1){
      product = {
        Id: 1,
        Name: "Toy Train",
        Width: 10,
        Length: 20,
        Height: 5,
        Weight: 50,
        Description: "Wooden train set"
      };
    }
    else if (productId == 2) {
      product = {
        Id: 2,
        Name: "Doll",
        Width: 8,
        Length: 15,
        Height: 12,
        Weight: 30,
        Description: "Classic doll"
      }
    }
    else if (productId == 3) {
      product = {
        Id: 3,
        Name: "Teddy Bear",
        Width: 12,
        Length: 10,
        Height: 8,
        Weight: 20,
        Description: "Soft and cuddly bear"
      }
    }
    return product;
  }

  public searchProduct(searchString: string) {
    // this.http.get(this.shelfUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    return [
      {
        Id: 1,
        Name: "Toy Train",
        Width: 10,
        Length: 20,
        Height: 5,
        Weight: 50,
        Description: "Wooden train set"
      },
      {
        Id: 3,
        Name: "Teddy Bear",
        Width: 12,
        Length: 10,
        Height: 8,
        Weight: 20,
        Description: "Soft and cuddly bear"
      }
    ]
  }
}
