import { Component, OnInit, Input, HostListener } from '@angular/core';
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
  screenWidth: number = window.innerWidth;

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      if (!userAuthenticated) {
        this.authService.login();
      }
      else {
        this.route.params.subscribe((params) => {
          this.storageId = params['storageId'];
        });

        this.storageRackService.getStorageRacks(this.storageId).subscribe((storageRacks) => {
          this.storageRacks = storageRacks;
        });
        this.storageService.getStorage(this.storageId).subscribe((storage) => {
          this.storage = storage;
        });
      }
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
    this.storageRackService.getStorageRacks(this.storageId).subscribe((storageRacks) => {
      this.storageRacks = storageRacks;
    })
  }

  confirmDelete(storageRackId: number) {
    if(confirm(`Are you sure to delete the Storage Rack?`)) {
      this.storageRackService.deleteStorageRack(storageRackId).subscribe(() => {
        this.storageRackService.getStorageRacks(this.storageId).subscribe((storageRacks) => {
          this.storageRacks = storageRacks;
        })
      });
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }
}
