import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StorageService implements OnInit {
  private readonly storageUrl = Constants.apiRoot + '/Storage';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getStorages() {
    return this.http.get(this.storageUrl);
  }

  public getStorage(storageId: number) {
    const url = this.storageUrl + `/${storageId}`
    return this.http.get(url);
  }

  public createStorage(storage: any) {
    return this.http.post(this.storageUrl, storage);
  }

  public updateStorage(storageId: number, storage: any) {
    const url = this.storageUrl + `/${storageId}`
    return this.http.put(url, storage);
  }

  public deleteStorage(storageId: number) {
    const url = this.storageUrl + `/${storageId}`
    return this.http.delete(url);
  }
}
