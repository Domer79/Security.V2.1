import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/distinctUntilChanged';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Group } from '../../../contracts/models/group';
import { GroupsService } from '../../../services/groups.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'group-profile',
  templateUrl: './group-profile.component.html',
  styleUrls: ['./group-profile.component.scss']
})
export class GroupProfileComponent implements OnInit {
  idMember: number;
  group$: Observable<Group>;
  constructor(
    private route: ActivatedRoute,
    private groupsService: GroupsService
  ) { }

  ngOnInit() {
    this.group$ = this.route.parent.data.pipe(map((data: {group: Group}) => {
      this.idMember = data.group.IdMember;
      return data.group;
    }));
    // this.user$ = this.route.paramMap.switchMap((params: ParamMap) => {
    //   return this.usersService.getByName(params.get('username'));
    // });
  }

  onSubmit(groupForm: NgForm):void{
    this.group$.subscribe(group => {
      this.groupsService.update(group).then(() => {
        groupForm.control.markAsPristine();
      });
    });
  }

}
