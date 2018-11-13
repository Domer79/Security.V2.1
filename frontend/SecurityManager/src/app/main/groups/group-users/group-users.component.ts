import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { GroupsService } from '../../../services/groups.service';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Group } from '../../../contracts/models/group';
import { DialogsService } from '../../../dialogs/dialogs.service';

@Component({
  selector: 'app-group-users',
  templateUrl: './group-users.component.html',
  styleUrls: ['./group-users.component.sass']
})
export class GroupUsersComponent implements OnInit {

  group: Group;
  users$: Observable<User[]>;
  nonIncludedUsers$: Observable<User[]>;
  selectedUsers: User[] = [];

  constructor(
    private route: ActivatedRoute,
    private groupsService: GroupsService,
    private modalService: NgbModal,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {group: Group}) => {
      this.users$ = this.groupsService.getGroupUsers(data.group);
      this.nonIncludedUsers$ = this.groupsService.getNonIncludedUsers(data.group);
      this.group = data.group;
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  addUsersToGroup(users: User[]){
    this.groupsService.addUsersToGroup(this.group, users).then(() => {
      this.users$ = this.groupsService.getGroupUsers(this.group);
      this.nonIncludedUsers$ = this.groupsService.getNonIncludedUsers(this.group);
      this.selectedUsers = [];
    });
  }

  cancelAddUsers($event){
    
  }

  deleteUsers(){
    this.groupsService.deleteUsersFromGroup(this.group, this.selectedUsers).then(() => {
      this.users$ = this.groupsService.getGroupUsers(this.group);
      this.nonIncludedUsers$ = this.groupsService.getNonIncludedUsers(this.group);
      this.selectedUsers = [];
    });
  }

  deleteCancel(){
    
  }

  select(user: User){
    if (this.selectedUsers.some((_: User) => _.Name === user.Name)){
      this.selectedUsers.splice(this.selectedUsers.indexOf(user), 1);
      return;
    }

    this.selectedUsers.push(user);
  }
}
