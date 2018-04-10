import { TestBed, inject } from '@angular/core/testing';

import { ApplicationContextService } from './application-context.service';

describe('ApplicationContextService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApplicationContextService]
    });
  });

  it('should be created', inject([ApplicationContextService], (service: ApplicationContextService) => {
    expect(service).toBeTruthy();
  }));
});
