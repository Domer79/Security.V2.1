import { Component, OnInit, Inject, AfterViewChecked } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../contracts/models/user';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import "rxjs/add/operator/map";
import { Location } from '@angular/common';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, AfterViewChecked {
  users: Observable<User[]>;
  selectedId: Observable<string>;

  constructor(
    private router: Router,
    private usersService: UsersService,
    private route: ActivatedRoute,
    private location: Location
  ) {
  }

  ngOnInit() {
    this.users = this.usersService.getAll();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.map((data: { user: User }) => {
        return `member${data.user.IdMember}`;
      });
    }
  }

  ngAfterViewChecked(): void {
    if (this.selectedId) {
      this.selectedId.subscribe(selectedId => {
        let element = document.querySelector(`#${selectedId}-header`);
        if (element != null) {
          element.scrollIntoView();
        }
      });
    }
  }
}
