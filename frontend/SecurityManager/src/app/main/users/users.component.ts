import { Component, OnInit, Inject, AfterViewChecked } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../contracts/models/user';
import { Router, ActivatedRoute, NavigationEnd, RouterOutlet, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Location } from '@angular/common';
import { map }  from "rxjs/operators"
import { SidePanelService } from '../services/side-panel.service';
import { UserDetailComponent } from './user-detail/user-detail.component';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.sass'],
})
export class UsersComponent implements OnInit, AfterViewChecked {
  users: Observable<User[]>;
  selectedId: Observable<string>;
  selectedUser: User;

  constructor(
    private router: Router,
    private usersService: UsersService,
    private route: ActivatedRoute,
    private sidePanelService: SidePanelService
  ) {
  }

  ngOnInit() {
    this.users = this.usersService.getAll();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.pipe(map((data: { user: User }) => {
        return `member${data.user.IdMember}`;
      }));

      this.selectedId.subscribe(selectedId => {
        let memberId = selectedId.substring(6);
        let userId = +memberId;
        this.users.subscribe(users => {
          let user = users.filter((user: User) => user.IdMember === userId)[0];
          this.selectUser(user);
        });
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

  createEmptyUser(): void{
    this.usersService.createEmpty().toPromise().then(user => {
      this.users = this.usersService.getAll();
    });
  }

  selectUser(user: User): void{
    this.selectedUser = user;
    var activatedRoute = this.route.children[0];
    var segments: Observable<UrlSegment[]>[] = [];
    var paths: string[] = [`${user.Login}`];
    while (activatedRoute != null){
      let component: any = activatedRoute.component;
      if (component.name === "UserDetailComponent"){
        activatedRoute = activatedRoute.children[0];
        continue;
      }

      let segment = activatedRoute.url;
      segments.push(segment)
      activatedRoute = activatedRoute.children[0];
    }

    segments.forEach(element => {
      element.subscribe(seg => {
        if (seg.length == 1)
          if (paths.indexOf(seg[0].path) === -1)
            paths.push(seg[0].path)
      });
    });

    this.router.navigate(paths, {relativeTo: this.route});
    if (!this.sidePanelService.checkOpen())
      this.openPanel();
  }

  openPanel(){
    this.sidePanelService.open();
  }

  closePanel(){
    this.sidePanelService.close();
  }

  isOpenPanel(): boolean{
    return this.sidePanelService.checkOpen();
  }
}
