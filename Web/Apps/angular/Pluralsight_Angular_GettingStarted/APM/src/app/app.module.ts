import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ProductModule } from './products/product.module';
import { WelcomeComponent } from './home/welcome.component';

@NgModule({
  bootstrap: [AppComponent],
  declarations: [ AppComponent, WelcomeComponent ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ProductModule,
    RouterModule.forRoot([
      { path: 'welcome', component: WelcomeComponent },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
    ])
  ]
})
export class AppModule {}
