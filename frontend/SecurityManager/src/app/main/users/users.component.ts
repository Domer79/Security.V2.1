import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../contracts/models/user';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: Observable<User[]>;
  selectedId: number;

  constructor(
    private router: Router,
    private usersService: UsersService,
  ) {}

  ngOnInit() {

    this.users = this.usersService.getAll();

    // this.usersService.getAll().subscribe(data => {
    //   this.users = data;
    // });
  }

}
