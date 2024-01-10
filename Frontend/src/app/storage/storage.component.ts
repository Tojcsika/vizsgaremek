import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storageRack.service';
import { StorageService } from '../services/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.css'],
})
export class StorageComponent implements OnInit {
  @Input()
  set id(storageId: number) {
    this.storageId = storageId;
  }
  public userAuthenticated = false;
  storageId: any;
  storageRacks: any;
  storage: any;

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      this.storageRacks = this.storageRackService.getStorageRacks(1);
      this.storage = this.storageService.getStorage(1);
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }
}
