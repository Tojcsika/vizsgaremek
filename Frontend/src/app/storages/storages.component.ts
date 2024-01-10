import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageService } from '../services/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-storages',
  templateUrl: './storages.component.html',
  styleUrls: ['./storages.component.css'],
})
export class StoragesComponent implements OnInit {
  public userAuthenticated = false;
  storages: any;

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      this.storages = this.storageService.getStorages();
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewStorage(storageId: number) {
    this.router.navigate(['/storage', storageId], { replaceUrl: true });
    // .then(() => {
    //   window.location.reload();
    // });
  }
}
