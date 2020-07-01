import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {SecurityService} from '../../security/security.service';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userName: Observable<string>;

  constructor(private securityService: SecurityService) {
  }

  ngOnInit(): void {
    this.userName = this.securityService.user.pipe(map(u => u ? u.userName : ''));
  }

  logOut(): void {
    this.securityService.logout();
  }
}
