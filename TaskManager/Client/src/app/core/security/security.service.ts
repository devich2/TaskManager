import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {User} from './auth/auth.model';
import {BehaviorSubject, Observable} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import {NgxPermissionsService} from 'ngx-permissions';

@Injectable({providedIn: 'root'})
export class SecurityService {

  public user = new BehaviorSubject<User>(null);

  constructor(private httpClient: HttpClient,
              private router: Router,
              private permissionsService: NgxPermissionsService) {
    this.loadUserData().subscribe();
    this.permissionsService.permissions$.subscribe((temp) => console.log(temp));
  }

 
  login(email: string, password: string): Observable<User> {
    return this.httpClient.post<User>('/api/auth/login', {email, password})
      .pipe(tap(user => this.userHandler(user), () => this.logoutHandler()));
  }

  logout(): void {
    this.httpClient.get('/api/auth/sign_out')
      .subscribe(() => this.logoutHandler(), () => this.logoutHandler());
  }
  
  loadUserData(): Observable<User> {
    return this.httpClient.get<User>('/api/Users/@me')
      .pipe(
        tap(user => this.userHandler(user), () => this.logoutHandler())
      );
  }

  isAuthenticated(): Observable<boolean> {
    return this.user.pipe(map(user => !!user));
  }
  
  private userHandler(user: User): void {
    this.user.next(user);
  }

  logoutHandler(): void {
    this.user.next(null);
    this.permissionsService.flushPermissions();
    this.router.navigateByUrl('login', {skipLocationChange: true});
  }
}
