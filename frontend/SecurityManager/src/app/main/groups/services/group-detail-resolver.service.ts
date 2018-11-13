import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Group } from '../../../contracts/models/group';
import { Observable } from 'rxjs/Observable';
import { GroupsService } from '../../../services/groups.service';

@Injectable()
export class GroupDetailResolverService implements Resolve<Group> {

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Group> {
    let groupName = route.paramMap.get("groupname");
    return this.groupsService.getByName(groupName);
  }
  constructor(
    private groupsService: GroupsService
  ) { }

}
