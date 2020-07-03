import {NgModule, Provider} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoginComponent} from './security/login/login.component';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {UnwraperInterceptor} from './api/interceptors/unwraper.interceptor';
import {RouterModule} from '@angular/router';
import {environment} from '../../environments/environment';
import {LoggingInterceptor} from './api/interceptors/logger.interceptor';
import {NgxPermissionsModule} from 'ngx-permissions';
import { SharedAppModule } from '../shared/shared-app.module';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
const interceptors: Provider[] = [{provide: HTTP_INTERCEPTORS, useClass: UnwraperInterceptor, multi: true}];

if (!environment.production) {
  interceptors.push({provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true});
}

@NgModule({
  declarations: [LoginComponent, HeaderComponent, SidebarComponent],
  imports: [
    CommonModule, HttpClientModule, RouterModule, SharedAppModule, NgxPermissionsModule.forRoot()
  ],
  exports: [
    SharedAppModule,
    SidebarComponent, HeaderComponent
  ],
  providers: [...interceptors]
})
export class CoreModule {
}
