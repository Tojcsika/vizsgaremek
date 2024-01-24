import { Component, OnInit, Input, HostListener } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storage-rack.service';
import { StorageService } from '../services/storage.service';
import { ShelfService } from '../services/shelf.service';
import { ShelfProductService } from '../services/shelf-product.service';
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
  screenWidth: number = window.innerWidth;

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private shelfService: ShelfService,
    private shelfProductService: ShelfProductService,
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
          this.shelfId = params['shelfId'];
        });

        this.authService.isAuthenticated().then((userAuthenticated) => {
          this.userAuthenticated = userAuthenticated;
          if (this.shelfId != null) {
            this.shelfService.getShelf(this.shelfId).subscribe((shelf) => {
              this.shelf = shelf;
              this.storageRackService.getStorageRack(this.shelf.storageRackId).subscribe((storageRack) => {
                this.storageRack = storageRack;
                this.storageService.getStorage(this.storageRack.storageId).subscribe((storage) => {
                  this.storage = storage;
                });
              });
            });
          }
        });
      }
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  viewStorage() {
    this.router.navigate(['/storage', this.storage.id]);
  }

  viewStorageRack() {
    this.router.navigate([`/storageracks/${this.storageRack.id}`]);
  }

  showEditDialog(shelfProductId?: number) {
    this.shelfProductEditId = shelfProductId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
    this.shelfService.getShelf(this.shelfId).subscribe((shelf) => {
      this.shelf = shelf;
    })
  }

  confirmDelete(shelfProductId: number) {
    if(confirm(`Are you sure to delete the Products from the Shelf?`)) {
      this.shelfProductService.deleteShelfProduct(shelfProductId).subscribe(() => {
        this.shelfService.getShelf(this.shelfId).subscribe((shelf) => {
          this.shelf = shelf;
        });
      });
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }
}
