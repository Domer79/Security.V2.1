import { TestBed, inject } from '@angular/core/testing';

import { UsersService } from './users.service';
import { CommonService } from '../system/service/common.service';
import { HttpClientModule } from '@angular/common/http';

describe('UsersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[
        HttpClientModule
      ],
      providers: [
        CommonService,
        UsersService
      ]
    });
  });

  it('should be created', inject([UsersService], (service: UsersService) => {
    expect(service).toBeTruthy();
  }));

  
});
