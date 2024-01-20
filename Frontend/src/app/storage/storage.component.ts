import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storage-rack.service';
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
  storageRackEditId: any;
  storage: any = {};
  editVisible: boolean = false;

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
    this.router.navigate([`/storageracks/${storageRackId}`]);
  }

  showEditDialog(storageRackId?: number) {
    this.storageRackEditId = storageRackId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
  }

  confirmDelete(storageRackId: number) {
    if(confirm(`Are you sure to delete the Storage Rack?`)) {
      // HTTP DELETE storage
      // HA OK
      this.storageRacks = this.storageRacks.filter(function(storageRack: any) { return storageRack.Id != storageRackId })
    }
  }
}
