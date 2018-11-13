import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';
import { UsersService } from '../../../services/users.service';

@Injectable()
export class UserDetailResolverService implements Resolve<User> {

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<User> {
    let userName = route.paramMap.get("username");
    return this.usersService.getByName(userName);
  }
  constructor(
    private usersService: UsersService
  ) { }

}
