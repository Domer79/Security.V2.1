import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { UsersService } from '../../../services/users.service';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Group } from '../../../contracts/models/group';
import { DialogsService } from '../../../dialogs/dialogs.service';

@Component({
  selector: 'app-user-groups',
  templateUrl: './user-groups.component.html',
  styleUrls: ['./user-groups.component.scss']
})
export class UserGroupsComponent implements OnInit {

  user: User;
  groups$: Observable<Group[]>;
  nonIncludedGroups$: Observable<Group[]>;
  selectedGroups: Group[] = [];

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    private modalService: NgbModal,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {user: User}) => {
      this.groups$ = this.usersService.getUserGroups(data.user);
      this.nonIncludedGroups$ = this.usersService.getNonIncludedGroups(data.user);
      this.user = data.user;
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  addGroupsToUser(groups: Group[]){
    this.usersService.addGroupsToUser(this.user, groups).then(() => {
      this.groups$ = this.usersService.getUserGroups(this.user);
      this.nonIncludedGroups$ = this.usersService.getNonIncludedGroups(this.user);
      this.selectedGroups = [];
    });
  }

  cancelAddGroups($event){
    
  }

  deleteGroups(){
    this.usersService.deleteGroupsFromUser(this.user, this.selectedGroups).then(() => {
      this.groups$ = this.usersService.getUserGroups(this.user);
      this.nonIncludedGroups$ = this.usersService.getNonIncludedGroups(this.user);
      this.selectedGroups = [];
    });
  }

  deleteCancel(){
    
  }

  select(group: Group){
    if (this.selectedGroups.some((_: Group) => _.Name === group.Name)){
      this.selectedGroups.splice(this.selectedGroups.indexOf(group), 1);
      return;
    }

    this.selectedGroups.push(group);
  }
}
