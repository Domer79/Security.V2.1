import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { UsersService } from '../../../services/users.service';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';
import { Role } from '../../../contracts/models/role';
import { DialogsService } from '../../../dialogs/dialogs.service';

@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.sass']
})
export class UserRolesComponent implements OnInit {
  user: User;
  nonIncludedRoles$: Observable<Role[]>;
  roles$: Observable<Role[]>;
  selectedRoles: Role[] = [];

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {user: User}) => {
      this.roles$ = this.usersService.getUserRoles(data.user);
      this.nonIncludedRoles$ = this.usersService.getNonIncludedRoles(data.user);
      this.user = data.user;    
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  addRolesToUser(roles: Role[]){
    this.usersService.addRolesToUser(this.user, roles).then(() => {
      this.roles$ = this.usersService.getUserRoles(this.user);
      this.nonIncludedRoles$ = this.usersService.getNonIncludedRoles(this.user);
      this.selectedRoles = [];
    });
  }

  cancelAddRoles($event){
    
  }

  deleteRoles(){
    this.usersService.deleteRolesFromUser(this.user, this.selectedRoles).then(() => {
      this.roles$ = this.usersService.getUserRoles(this.user);
      this.nonIncludedRoles$ = this.usersService.getNonIncludedRoles(this.user);
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
