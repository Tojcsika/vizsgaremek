import { Component, OnInit, Input, HostListener } from '@angular/core';
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
  screenWidth: number = window.innerWidth;

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService,
    private storageService: StorageService,
    private shelfService: ShelfService,
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
          this.storageRackId = params['storageRackId'];
        });

        if (this.storageRackId != null) {
          this.storageRackService.getStorageRack(this.storageRackId).subscribe((storageRack) => {
            this.storageRack = storageRack;
            this.storageService.getStorage(this.storageRack.storageId).subscribe((storage) => {
              this.storage = storage;
            });
          });
          this.shelfService.getShelves(this.storageRackId).subscribe((shelves) => {
            this.storageRackShelves = shelves;
          })
        }
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
    this.router.navigate(['/storage', this.storage.id]);
  }

  showEditDialog(shelfId?: number) {
    this.shelfEditId = shelfId;
    this.editVisible = true;
  }

  editClosed() {
    this.editVisible = false;
    this.shelfService.getShelves(this.storageRackId).subscribe((shelves) => {
      this.storageRackShelves = shelves;
    })
  }

  confirmDelete(shelfId: number) {
    if(confirm(`Are you sure to delete the Shelf?`)) {
      this.shelfService.deleteShelf(shelfId).subscribe(() => {
        this.shelfService.getShelves(this.storageRackId).subscribe((shelves) => {
          this.storageRackShelves = shelves;
        })
      });
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }
}
