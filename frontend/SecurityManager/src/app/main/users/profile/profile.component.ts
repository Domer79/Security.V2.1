import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { User } from '../../../contracts/models/user';
import { UsersService } from '../../../services/users.service';
import { NgForm, Form } from '@angular/forms';
import { map } from 'rxjs/operators';
import { SidePanelService } from '../../services/side-panel.service';

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
    private usersService: UsersService,
    private router: Router,
    private sidePanelService: SidePanelService
  ) { }

  ngOnInit() {
    this.user$ = this.route.parent.data.pipe(map((data: {user: User}) => {
      this.idMember = data.user.IdMember;
      return data.user;
    }));
    // this.user$ = this.route.paramMap.switchMap((params: ParamMap) => {
    //   return this.usersService.getByName(params.get('username'));
    // });
  }

  onSubmit(userForm: NgForm):void{
    this.user$.subscribe(user => {
      this.usersService.update(user).then(() => {
        userForm.control.markAsPristine();
      });
    });
  }

}
