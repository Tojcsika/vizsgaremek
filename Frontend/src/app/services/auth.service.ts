import { Injectable } from '@angular/core';
import { UserManager, User, UserManagerSettings } from 'oidc-client';
import { Constants } from '../shared/constants';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userManager: UserManager;
  private user: User | null;
  private loginChangedSubject = new Subject<boolean>();

  public loginChanged = this.loginChangedSubject.asObservable();

  private get idpSettings(): UserManagerSettings {
    return {
      authority: Constants.idpAuthority,
      client_id: Constants.clientId,
      redirect_uri: `${Constants.clientRoot}/signin-callback`,
      scope: 'openid profile vizsgaremekAPI',
      response_type: 'code',
      post_logout_redirect_uri: `${Constants.clientRoot}/signout-callback`,
      automaticSilentRenew: true,
      silent_redirect_uri: `${Constants.clientRoot}/assets/silent-callback.html`,
    };
  }
  constructor() {
    this.userManager = new UserManager(this.idpSettings);
    this.userManager.events.addAccessTokenExpired((_) => {
      this.loginChangedSubject.next(false);
    });
    this.user = null;
  }

  public login = () => {
    return this.userManager.signinRedirect();
  };

  public isAuthenticated = (): Promise<boolean> => {
    return this.userManager.getUser().then((user) => {
      if (this.user !== user) {
        this.loginChangedSubject.next(this.checkUser(user));
      }

      this.user = user;
      return this.checkUser(this.user);
    });
  };

  private checkUser = (user: User | null): boolean => {
    return !!user && !user.expired;
  };

  public finishLogin = (): Promise<User> => {
    return this.userManager.signinRedirectCallback().then((user) => {
      this.user = user;
      localStorage.setItem('id_token', user.access_token);
      this.loginChangedSubject.next(this.checkUser(user));
      return user;
    });
  };

  public logout = () => {
    this.userManager.signoutRedirect();
  };

  public finishLogout = () => {
    this.user = null;
    localStorage.removeItem('id_token');
    this.loginChangedSubject.next(false);
    return this.userManager.signoutRedirectCallback();
  };

  public getAccessToken = (): Promise<string | null> => {
    return this.userManager.getUser().then((user) => {
      return !!user && !user.expired ? user.access_token : null;
    });
  };

  public getUserName() {
    if (this.user) {
      return this.user.profile.name;
    }
    return '';
  }
}
