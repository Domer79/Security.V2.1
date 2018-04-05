import { Injectable } from '@angular/core';
import { Security } from '../contracts/security';
import { UserRepository } from '../contracts/repositories/user-repository';
import { UsersService } from './users.service';

@Injectable()
export class SecurityService implements Security {

  constructor(
    usersService: UsersService
  ) { 
    this.UsersService = usersService;
  }

  userValidate(): boolean {
    throw new Error("Method not implemented.");
  }
  checkAccess(): boolean {
    throw new Error("Method not implemented.");
  }
  setPassword(): boolean {
    throw new Error("Method not implemented.");
  }

  UsersService: UserRepository;

}
