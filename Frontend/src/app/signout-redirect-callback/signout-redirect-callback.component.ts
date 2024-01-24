import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signout-redirect-callback',
  template: `<div></div>`,
})
export class SignoutRedirectCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.finishLogout().then(() => {
      this.authService.login();
    });
  }
}
