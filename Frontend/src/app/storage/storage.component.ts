import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storageRack.service';
import { StorageService } from '../services/storage.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.css'],
})
export class StorageComponent implements OnInit {
  public userAuthenticated = false;
  storageId: any;
  storageRacks: any;
  storage: any = {};

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.storageId = params['storageId'];
    });

    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      this.storageRacks = this.storageRackService.getStorageRacks(1);
      this.storage = this.storageService.getStorage(this.storageId);
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewStorageRack(storageRackId: number) {
    this.router.navigate([`/storageracks/${storageRackId}`], {
      replaceUrl: true,
    });
  }
}
