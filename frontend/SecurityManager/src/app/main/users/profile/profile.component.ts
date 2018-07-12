import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/distinctUntilChanged';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { User } from '../../../contracts/models/user';
import { UsersService } from '../../../services/users.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  idMember: number;
  user$: Observable<User>;
  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService
  ) { }

  ngOnInit() {
    this.user$ = this.route.parent.data.map((data: {user: User}) => {
      this.idMember = data.user.IdMember;
      return data.user;
    });
    // this.user$ = this.route.paramMap.switchMap((params: ParamMap) => {
    //   return this.usersService.getByName(params.get('username'));
    // });
  }

  onSubmit(user: User):void{
    this.usersService.update(user).then(() => {
      this.user$ = this.usersService.getElement(this.idMember);
    });
  }

}
