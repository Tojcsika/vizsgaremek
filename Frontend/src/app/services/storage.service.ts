import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class StorageService implements OnInit {
  private readonly storageUrl = Constants.apiRoot + '/storage';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getStorages() {
    // this.http.get(this.storageUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    return [
      {
        Id: 1,
        Name: 'Raktár 1',
        Address: 'Raktár utca 44.',
        Area: 5000,
      },
      {
        Id: 2,
        Name: 'Raktár 2',
        Address: 'Valami utca 23.',
        Area: 9000,
      },
    ];
  }

  public getStorage(storageId: number) {
    // this.http.get(this.storageUrl).subscribe((storages: any) => {
    //   return storages;
    // });
    if (storageId == 1) {
      var storage = {
        Id: 1,
        Name: 'Raktár 1',
        Address: 'Raktár utca 44.',
        Area: 5000,
      };
      return storage;
    }
    return [];
  }
}
