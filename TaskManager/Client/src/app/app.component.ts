import { Component } from '@angular/core';
import { SecurityService } from './core/security/security.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public securityService: SecurityService) {
  }
}
