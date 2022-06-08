import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { CustomerComponent } from './customers/customer.component';
import { ReactiveComponent } from './customers/reactive/reactive.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    ReactiveComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: CustomerComponent },
      { path: 'reactiveform', component: ReactiveComponent }
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
