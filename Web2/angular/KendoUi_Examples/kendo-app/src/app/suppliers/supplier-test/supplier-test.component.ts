import { Component } from '@angular/core';
import { SupplierService } from '../supplier.service';

@Component({
  selector: 'pm-supplier-test',
  templateUrl: './supplier-test.component.html',
  styleUrls: ['./supplier-test.component.css'],
})
export class SupplierTestComponent {
  constructor(private supplierService: SupplierService) {}

  runTest(): void {
    this.supplierService.supplierWithConcatMap$.subscribe();
    this.supplierService.supplierWithMap$.subscribe();
    this.supplierService.supplierWithMergeMap$.subscribe();
    this.supplierService.supplierWithSwitchMap$.subscribe();
  }
}
