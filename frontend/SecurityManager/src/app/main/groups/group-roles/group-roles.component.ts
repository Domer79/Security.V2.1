import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';
import { Role } from '../../../contracts/models/role';
import { DialogsService } from '../../../dialogs/dialogs.service';
import { Group } from '../../../contracts/models/group';
import { GroupsService } from '../../../services/groups.service';

@Component({
  selector: 'app-group-roles',
  templateUrl: './group-roles.component.html',
  styleUrls: ['./group-roles.component.scss']
})
export class GroupRolesComponent implements OnInit {
  group: Group;
  nonIncludedRoles$: Observable<Role[]>;
  roles$: Observable<Role[]>;
  selectedRoles: Role[] = [];

  constructor(
    private route: ActivatedRoute,
    private groupsService: GroupsService,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {group: Group}) => {
      this.roles$ = this.groupsService.getGroupRoles(data.group);
      this.nonIncludedRoles$ = this.groupsService.getNonIncludedRoles(data.group);
      this.group = data.group;    
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  addRolesToGroup(roles: Role[]){
    this.groupsService.addRolesToGroup(this.group, roles).then(() => {
      this.roles$ = this.groupsService.getGroupRoles(this.group);
      this.nonIncludedRoles$ = this.groupsService.getNonIncludedRoles(this.group);
      this.selectedRoles = [];
    });
  }

  cancelAddRoles($event){
    
  }

  deleteRoles(){
    this.groupsService.deleteRolesFromGroup(this.group, this.selectedRoles).then(() => {
      this.roles$ = this.groupsService.getGroupRoles(this.group);
      this.nonIncludedRoles$ = this.groupsService.getNonIncludedRoles(this.group);
      this.selectedRoles = [];
    });
  }

  deleteCancel(){
    
  }

  select(role: Role){
    if (this.selectedRoles.some((_: Role) => _.Name === role.Name)){
      this.selectedRoles.splice(this.selectedRoles.indexOf(role), 1);
      return;
    }

    this.selectedRoles.push(role);
  }
}
