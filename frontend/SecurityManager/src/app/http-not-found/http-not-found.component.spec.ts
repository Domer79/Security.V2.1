import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HttpNotFoundComponent } from './http-not-found.component';

describe('HttpNotFoundComponent', () => {
  let component: HttpNotFoundComponent;
  let fixture: ComponentFixture<HttpNotFoundComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HttpNotFoundComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HttpNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
