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
  @Input() storageId!: number;
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
      this.storageRackService.getStorageRack(this.storageRackId).subscribe((storageRack) => {
        this.storageRack = storageRack;
        this.storageRack.storageId = this.storageId;
      });
    }
    else{
      this.storageRack = {
        row: null,
        rowPosition: null,
        weightLimit: null,
        storageId: this.storageId
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveStorageRack() {
    if (this.storageRackId != null) {
      this.storageRackService.updateStorageRack(this.storageRackId, this.storageRack).subscribe();
    } else {
      this.storageRackService.createStorageRack(this.storageRack).subscribe();
    }
    this.visible = false;
  }
}
