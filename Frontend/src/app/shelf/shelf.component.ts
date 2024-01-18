import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storage-rack.service';
import { StorageService } from '../services/storage.service';
import { ShelfService } from '../services/shelf.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-shelf',
  templateUrl: './shelf.component.html',
  styleUrls: ['./shelf.component.css'],
})
export class ShelfComponent implements OnInit {
  public userAuthenticated = false;
  shelfId: any;
  storageRack: any = {};
  storage: any = {};
  shelf: any = {};
  shelfProductEditId: any;
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
      this.shelfId = params['shelfId'];
    });

    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
      if (this.shelfId != null) {
        this.shelf = this.shelfService.getShelf(this.shelfId);
        console.log(this.shelf);
        this.storageRack = this.storageRackService.getStorageRack(
          this.shelf.StorageRackId
        );
        this.storage = this.storageService.getStorage(
          this.storageRack.StorageId
        );
      }
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewStorage() {
    this.router.navigate(['/storage', this.storage.Id], { replaceUrl: true });
  }

  viewStorageRack() {
    this.router.navigate([`/storageracks/${this.storageRack.Id}`], {
      replaceUrl: true,
    });
  }

  showEditDialog(shelfProductId?: number) {
    this.shelfProductEditId = shelfProductId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
  }

  confirmDelete(shelfProductId: number) {
    if(confirm(`Are you sure to delete the Products from the Shelf?`)) {
      // HTTP DELETE storage
      // HA OK
      this.shelf.ShelfProducts = this.shelf.ShelfProducts.filter(function(shelfProduct: any) { return shelfProduct.Id != shelfProductId })
    }
  }
}
