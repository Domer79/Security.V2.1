import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { RolesService } from '../../../services/roles.service';
import { Role } from '../../../contracts/models/role';

@Injectable()
export class RoleDetailResolverService implements Resolve<Role> {

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Role> {
    let roleName = route.paramMap.get("rolename");
    return this.rolesService.getByName(roleName);
  }
  constructor(
    private rolesService: RolesService
  ) { }

}
