import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationRedirectComponent } from './application-redirect.component';

describe('ApplicationRedirectComponent', () => {
  let component: ApplicationRedirectComponent;
  let fixture: ComponentFixture<ApplicationRedirectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationRedirectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationRedirectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
