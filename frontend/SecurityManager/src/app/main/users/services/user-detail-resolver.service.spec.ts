import { TestBed, inject } from '@angular/core/testing';

import { UserDetailResolverService } from './user-detail-resolver.service';

describe('UserDetailResolverService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserDetailResolverService]
    });
  });

  it('should be created', inject([UserDetailResolverService], (service: UserDetailResolverService) => {
    expect(service).toBeTruthy();
  }));
});
