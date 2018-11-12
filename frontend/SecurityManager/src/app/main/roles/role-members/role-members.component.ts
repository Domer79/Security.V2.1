import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Role } from '../../../contracts/models/role';
import { DialogsService } from '../../../dialogs/dialogs.service';
import { Member } from '../../../contracts/models/member';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'app-role-roles',
  templateUrl: './role-members.component.html',
  styleUrls: ['./role-members.component.scss']
})
export class RoleMembersComponent implements OnInit {
  role: Role;
  members$: Observable<Member[]>;
  selectedMembers: Member[] = [];

  constructor(
    private route: ActivatedRoute,
    private rolesService: RolesService,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {role: Role}) => {
      this.members$ = this.rolesService.getRoleMembers(data.role);
      this.role = data.role;    
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  select(member: Member){
    if (this.selectedMembers.some((_: Member) => _.Name === member.Name)){
      this.selectedMembers.splice(this.selectedMembers.indexOf(member), 1);
      return;
    }

    this.selectedMembers.push(member);
  }
}
