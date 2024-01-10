import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Vizsgaremek';
  sidebarVisible = true;

  constructor(private authService: AuthService, private router: Router) {}

  public storages = () => {
    this.router.navigate(['/storages'], { replaceUrl: true });
  };

  public products = () => {
    this.router.navigate(['/products'], { replaceUrl: true });
  };
}
