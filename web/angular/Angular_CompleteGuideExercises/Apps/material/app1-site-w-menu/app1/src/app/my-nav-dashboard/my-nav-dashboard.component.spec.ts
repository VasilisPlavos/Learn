
import { fakeAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyNavDashboardComponent } from './my-nav-dashboard.component';

describe('MyNavDashboardComponent', () => {
  let component: MyNavDashboardComponent;
  let fixture: ComponentFixture<MyNavDashboardComponent>;

  beforeEach(fakeAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ MyNavDashboardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyNavDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});
