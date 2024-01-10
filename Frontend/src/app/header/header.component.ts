import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public userAuthenticated = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  public login = () => {
    this.authService.login();
  };

  public logout = () => {
    this.authService.logout();
  };

  public getUserName() {
    return this.authService.getUserName();
  }

  public builder = () => {
    this.router.navigate(['/builder'], { replaceUrl: true }).then(() => {
      window.location.reload();
    });
  };

  public dashboard = () => {
    this.router.navigate(['/dashboard'], { replaceUrl: true });
  };

  public isBuilder(): any {
    return window.location.pathname.startsWith('/builder');
  }

  public isDashboard(): any {
    return window.location.pathname.startsWith('/dashboard');
  }
}
