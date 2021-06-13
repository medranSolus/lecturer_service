import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {

  constructor(private authService: AuthService,
              private localStorageService: LocalStorageService,
              private router: Router) { }

  ngOnInit(): void {
    
  }

  logout() {
    this.authService.logout();
  }

  getUserName(): string {
    const user = this.localStorageService.getUserData();
    if (user) {
      return `${user.name} ${user.surname}`;
    }
  }

  isUserLoggedIn(): boolean {
    return this.authService.isUserLogedIn();
  }

  isCurrentRoute(url: string) {
    return this.router.url === url;
  }

  isAdmin() {
    return this.localStorageService.getUserData()?.roleTypeID === 0;
  }

}
