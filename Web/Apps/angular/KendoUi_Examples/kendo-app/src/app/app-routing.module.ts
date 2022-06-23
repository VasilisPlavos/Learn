import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WelcomeComponent } from './home/welcome.component';
import { ChartsComponent } from './kendo/charts/charts/charts.component';
import { PageNotFoundComponent } from './page-not-found.component';
import { SupplierTestComponent } from './suppliers/supplier-test/supplier-test.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: 'kendo/charts', component: ChartsComponent },
      {
        path: 'products',
        loadChildren: () =>
          import('./products/product.module').then(m => m.ProductModule)
      },
      { path: 'suppliertest', component: SupplierTestComponent },
      { path: 'welcome', component: WelcomeComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
