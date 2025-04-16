import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplierTestComponent } from './supplier-test.component';

describe('SupplierTestComponent', () => {
  let component: SupplierTestComponent;
  let fixture: ComponentFixture<SupplierTestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SupplierTestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
