import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Constants } from '../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class ShelfService implements OnInit {
  private readonly storageRackUrl = Constants.apiRoot + '/StorageRack';
  private readonly shelfUrl = Constants.apiRoot + '/Shelf';
  constructor(private http: HttpClient) {}

  ngOnInit() {}

  public getShelves(storageRackId: number) {
    const url = this.storageRackUrl + `/${storageRackId}/Shelves`;
    return this.http.get(url);
  }

  public getShelf(shelfId: number) {
    const url = this.shelfUrl + `/${shelfId}`;
    return this.http.get(url);
  }

  public createShelf(shelf: any) {
    return this.http.post(this.shelfUrl, shelf);
  }

  public updateShelf(shelfId: number, shelf: any) {
    const url = this.shelfUrl + `/${shelfId}`;
    return this.http.put(url, shelf);
  }

  public deleteShelf(shelfId: number) {
    const url = this.shelfUrl + `/${shelfId}`;
    return this.http.delete(url);
  }
}
