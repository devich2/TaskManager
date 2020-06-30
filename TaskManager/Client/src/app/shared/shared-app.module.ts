import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {NgxPermissionsModule} from 'ngx-permissions';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatTabsModule} from '@angular/material/tabs';
import {MatInputModule} from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatDividerModule} from '@angular/material/divider';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
@NgModule({
  declarations: [
  ],
  imports: [
    // Angular
    CommonModule,
    // Forms
    FormsModule, ReactiveFormsModule,
      // Material
      MatButtonModule, MatButtonToggleModule, MatTabsModule, MatInputModule,
      MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatSelectModule,
      MatSidenavModule, MatIconModule, MatDialogModule, MatExpansionModule, MatDividerModule, MatPaginatorModule,
    // Icons
    FontAwesomeModule
  ],
  exports: [
    NgxPermissionsModule,
    // Icons
    FontAwesomeModule,
     // Forms
     FormsModule, ReactiveFormsModule,
    // Material
    MatButtonModule, MatButtonToggleModule, MatTabsModule, MatInputModule,
    MatDatepickerModule, MatNativeDateModule, MatFormFieldModule, MatSelectModule,
    MatSidenavModule, MatIconModule, MatDialogModule, MatExpansionModule, MatDividerModule, MatPaginatorModule,
  ],
  providers: [
  ]
})
export class SharedAppModule {
}
