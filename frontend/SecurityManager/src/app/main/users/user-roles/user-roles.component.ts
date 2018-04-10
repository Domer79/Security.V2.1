import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { UsersService } from '../../../services/users.service';
import { User } from '../../../contracts/models/user';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-user-roles',
  templateUrl: './user-roles.component.html',
  styleUrls: ['./user-roles.component.scss']
})
export class UserRolesComponent implements OnInit {
  user$: Observable<User>;
  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService
  ) { }

  ngOnInit() {
    this.user$ = this.route.parent.data.map((data: {user: User}) => {
      return data.user;
    });  
  }

}
