import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Vizsgaremek';
  sidebarVisible = true;
  userAuthenticated = false;
  screenWidth = window.innerWidth;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isAuthenticated().then((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated
    });

    this.authService.loginChanged.subscribe((userAuthenticated) => {
      this.userAuthenticated = userAuthenticated;
    });
  }

  public storages = () => {
    this.router.navigate(['/storages']);
  };

  public products = () => {
    this.router.navigate(['/products']);
  };

  public searchProduct = () => {
    this.router.navigate(['/searchProduct']);
  };

  public logout() {
    this.authService.logout();
  }

  public getUserName() {
    return this.authService.getUserName();
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }
}
