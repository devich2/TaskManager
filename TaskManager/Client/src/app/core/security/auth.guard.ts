import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {SecurityService} from './security.service';

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate {

  constructor(private securityService: SecurityService) {
  }

  canActivate(next: ActivatedRouteSnapshot,
              state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (this.securityService.isAuthenticated()) {
      return true;
    } else {
      this.securityService.logoutHandler();
      return false;
    }
  }

}
