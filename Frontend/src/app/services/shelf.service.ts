import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ShelfService implements OnInit {
  private readonly shelfUrl = Constants.apiRoot + '/shelf';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getShelves(storageId: number, storageRackId: number) {
    // this.http.get(this.shelfUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    if (storageId == 1 && storageRackId == 1) {
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
    return [];
  }

  public getShelf(shelfId: number) {
    if (shelfId == 1) {
      var shelf = {
        Id: 1,
        StorageRackId: 1,
        Level: 1,
        Width: 100,
        Length: 200,
        Height: 300,
        WeightLimit: 200,
        ShelfProducts: [
          {
            Id: 1,
            Name: 'Alma',
            Weight: 0.1,
            Quantity: 230,
            TotalWeight: 23,
            Width: 100,
            Length: 100,
            Height: 200,
          },
        ],
      };
      return shelf;
    }
    return {};
  }
}
