import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storage-rack.service';
import { StorageService } from '../services/storage.service';
import { ShelfService } from '../services/shelf.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-storage-rack',
  templateUrl: './storage-rack.component.html',
  styleUrls: ['./storage-rack.component.css'],
})
export class StorageRackComponent implements OnInit {
  public userAuthenticated = false;
  storageRackId: any;
  storageRack: any = {};
  storage: any = {};
  storageRackShelves: any;
  shelfEditId: any;
  editVisible: boolean = false;

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private shelfService: ShelfService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.storageRackId = params['storageRackId'];
    });

    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      if (this.storageRackId != null) {
        this.storageRack = this.storageRackService.getStorageRack(
          this.storageRackId
        );
        this.storage = this.storageService.getStorage(
          this.storageRack.StorageId
        );
        this.storageRackShelves = this.shelfService.getShelves(
          this.storage.Id,
          this.storageRackId
        );
      }
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewProducts(shelfId: number) {
    this.router.navigate([`/shelves/${shelfId}`]);
  }

  viewStorage() {
    this.router.navigate(['/storage', this.storage.Id]);
  }

  showEditDialog(shelfId?: number) {
    this.shelfEditId = shelfId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
  }

  confirmDelete(shelfId: number) {
    if(confirm(`Are you sure to delete the Shelf?`)) {
      // HTTP DELETE storage
      // HA OK
      this.storageRackShelves = this.storageRackShelves.filter(function(storageRackShelf: any) { return storageRackShelf.Id != shelfId })
    }
  }
}
