import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class StorageRackService implements OnInit {
  private readonly storageRackUrl = Constants.apiRoot + '/StorageRack';
  private readonly storageUrl = Constants.apiRoot + '/Storage';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getStorageRacks(storageId: number) {
    const url = this.storageUrl + `/${storageId}/StorageRacks`;
    return this.http.get(url);
  }

  public getStorageRack(storageRackId: number) {
    return this.http.get(this.storageRackUrl + `/${storageRackId}`);
  }

  public createStorageRack(storageRack: any) {
    return this.http.post(this.storageRackUrl, storageRack);
  }

  public updateStorageRack(storageRackId: number, storageRack: any) {
    const url = this.storageRackUrl + `/${storageRackId}`;
    return this.http.put(url, storageRack);
  }

  public deleteStorageRack(storageRackId: number) {
    const url = this.storageRackUrl + `/${storageRackId}`;
    return this.http.delete(url);
  }
}
