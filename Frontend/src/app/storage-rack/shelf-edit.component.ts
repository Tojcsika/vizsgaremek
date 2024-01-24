import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ShelfService } from '../services/shelf.service';

@Component({
  selector: 'app-shelf-edit',
  templateUrl: './shelf-edit.component.html',
  styleUrls: ['./shelf-edit.component.css'],
})
export class ShelfEditComponent implements OnInit {
  @Input() shelfId?: number;
  @Input() storageRackId!: number;
  @Input() storageId!: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  shelf: any = {};

  constructor(
    private authService: AuthService,
    private shelfService: ShelfService
  ) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  onShow() {
    if (this.shelfId != null) {
      this.shelfService.getShelf(this.shelfId).subscribe((shelf) => {
        this.shelf = shelf;
        this.shelf.storageRackId = this.storageRackId;
        this.shelf.storageId = this.storageId;
      })
    }
    else{
      this.shelf = {
        level: null,
        width: null,
        length: null,
        height: null,
        weightLimit: null,
        storageRackId: this.storageRackId,
        storageId: this.storageId
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveShelf() {
    if (this.shelfId != null) {
      this.shelfService.updateShelf(this.shelfId, this.shelf).subscribe();
    }
    else{
      this.shelfService.createShelf(this.shelf).subscribe();
    }
    this.visible = false;
  }
}
