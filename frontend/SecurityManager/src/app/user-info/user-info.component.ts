import { Component, OnInit } from '@angular/core';
import { TokenService } from '../services/token.service';
import { Observable } from 'rxjs';
import { User } from '../contracts/models/user';
import { map } from 'rxjs/operators';
import { Application } from '../contracts/models/Application';

@Component({
  selector: 'user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.sass']
})
export class UserInfoComponent implements OnInit {
  userName$: Observable<string>;

  constructor(
    private tokenService: TokenService,
  ) { }

  ngOnInit() {
    this.userName$ = this.tokenService.getUser().pipe(map((user: User) => {
      return user.Login;
    }));
  }


}
