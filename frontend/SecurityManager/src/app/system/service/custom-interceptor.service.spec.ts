import { TestBed, inject } from '@angular/core/testing';

import { CustomInterceptorService } from './custom-interceptor.service';

describe('CustomInterceptorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CustomInterceptorService]
    });
  });

  it('should be created', inject([CustomInterceptorService], (service: CustomInterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
