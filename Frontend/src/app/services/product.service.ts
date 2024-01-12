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
    // this.http.get(this.shelfUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    return [
      {
        Id: 1,
        StorageRackId: 1,
        Level: 1,
        Width: 100,
        Length: 200,
        Height: 300,
        WeightLimit: 200,
      },
      {
        Id: 2,
        StorageRackId: 1,
        Level: 2,
        Width: 100,
        Length: 200,
        Height: 450,
        WeightLimit: 200,
      },
    ];
  }
}
