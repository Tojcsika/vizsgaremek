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
      this.shelf = this.shelfService.getShelf(this.shelfId);
    }
    else{
      this.shelf = {
        Level: null,
        Width: null,
        Length: null,
        Height: null,
        WeightLimit: null,
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveShelf() {
    // HTTP POST mentéshez
    this.visible = false;
  }
}
