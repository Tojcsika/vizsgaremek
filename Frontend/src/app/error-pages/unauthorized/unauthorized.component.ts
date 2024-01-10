import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css'],
})
export class UnauthorizedComponent implements OnInit {
  public isUserAuthenticated: boolean = false;
  constructor(private authService: AuthService) {
    this.authService.loginChanged.subscribe((res) => {
      this.isUserAuthenticated = res;
    });
  }
  ngOnInit(): void {
    this.authService.isAuthenticated().then((isAuth) => {
      this.isUserAuthenticated = isAuth;
    });
  }
  public login = () => {
    this.authService.login();
  };
  public logout = () => {
    this.authService.logout();
  };
}
