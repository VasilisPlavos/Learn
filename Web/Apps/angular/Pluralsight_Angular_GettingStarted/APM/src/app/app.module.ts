import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ProductModule } from './products/product.module';
import { ReactiveFormsModule } from '@angular/forms';
import { WelcomeComponent } from './home/welcome.component';
import { EditComponent } from './users/edit.component';
import { Edit2Component } from './users/edit2.component';


@NgModule({
  bootstrap: [AppComponent],
  declarations: [ AppComponent, WelcomeComponent, EditComponent, Edit2Component ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ProductModule,
    RouterModule.forRoot([
      { path: 'welcome', component: WelcomeComponent },
      { path: 'users/edit', component: EditComponent  },
      { path: 'users/edit2', component: Edit2Component  },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ]),
    ReactiveFormsModule
  ]
})
export class AppModule {}
