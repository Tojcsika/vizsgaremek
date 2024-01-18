import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageRackService } from '../services/storage-rack.service';

@Component({
  selector: 'app-storage-rack-edit',
  templateUrl: './storage-rack-edit.component.html',
  styleUrls: ['./storage-rack-edit.component.css'],
})
export class StorageRackEditComponent implements OnInit {
  @Input() storageRackId?: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  storageRack: any = {};

  constructor(
    private authService: AuthService,
    private storageRackService: StorageRackService
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
    if (this.storageRackId != null) {
      this.storageRack = this.storageRackService.getStorageRack(this.storageRackId);
    }
    else{
      this.storageRack = {
        Row: null,
        RowPosition: null,
        WeightLimit: null,
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveStorageRack() {
    // HTTP POST mentéshez
    this.visible = false;
  }
}
