import {
  Component,
  OnInit,
  Input,
  EventEmitter,
  Output,
} from '@angular/core';
import { AuthService } from '../services/auth.service';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-storage-edit',
  templateUrl: './storage-edit.component.html',
  styleUrls: ['./storage-edit.component.css'],
})
export class StorageEditComponent implements OnInit {
  @Input() storageId?: number;
  @Input() visible!: boolean;
  @Output() onClose = new EventEmitter();

  public userAuthenticated = false;
  storage: any = {};

  constructor(
    private authService: AuthService,
    private storageService: StorageService
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
    if (this.storageId != null) {
      this.storage = this.storageService.getStorage(this.storageId);
    }
    else{
      this.storage = {
        Name: "",
        Address: "",
        Area: null
      }
    }
  }

  onHide() {
    this.onClose.emit();
  }

  saveStorage() {
    // HTTP POST mentéshez
    this.visible = false;
  }
}
