import {Component, OnInit} from '@angular/core';
import {SecurityService} from '../security.service';
import {Router} from '@angular/router';
import {filter, take} from 'rxjs/operators';
import {identity} from 'rxjs';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import {faEye, faEyeSlash} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userForm: FormGroup;

  invalidData = false;
  hide = true;

  faEye = faEye;
  faEyeSlash = faEyeSlash;

  constructor(
              private router: Router) {
  }

  ngOnInit(): void {
  /*  this.securityService.isAuthenticated()
      .pipe(
        take(1),
        filter(identity)
      )
      .subscribe(() => this.router.navigateByUrl('/'))
      */;
        console.log("HELLO");
    this.userForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });
  }

  login(): void {
   
  }

  prevent(event: any): void {
    event.preventDefault();
    this.login();
  }
}
