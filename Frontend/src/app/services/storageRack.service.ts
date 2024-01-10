import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class StorageRackService implements OnInit {
  private readonly storageRackUrl = Constants.apiRoot + '/storageRack';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getStorageRacks(storageId: number) {
    // this.http.get(this.storageRackUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    if (storageId == 1) {
      return [
        {
          Id: 1,
          StorageId: 1,
          Row: 1,
          RowPosition: 1,
          WeightLimit: 1200,
        },
        {
          Id: 2,
          StorageId: 1,
          Row: 1,
          RowPosition: 2,
          WeightLimit: 1300,
        },
      ];
    }
    return [];
  }
}
