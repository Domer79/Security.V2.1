import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute, RouterEvent } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs/Subscription';
import { User } from '../../../contracts/models/user';

type ActiveLink = 'PROFILE' | 'GROUPS' | 'ROLES';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit, OnDestroy {
  routerEventSubscriber: Subscription;
  fragment: string;
  link: ActiveLink;

  constructor(
    private router: Router
  ) {
  }

  ngOnInit() {
    // #region Этот участок кода отвечает за правильный линк при загрузке приложения
    if (this.routerEventSubscriber == undefined){
      let segmentRoute = this.router.url.split('/').pop().toUpperCase();
      this.link = segmentRoute as ActiveLink;  
    }
    // #endregion

    this.routerEventSubscriber = this.router.events.subscribe((event: RouterEvent) => {
      if (event instanceof NavigationEnd) {
        let segmentRoute = this.router.url.split('/').pop().toUpperCase();
        this.link = segmentRoute as ActiveLink;
      }
    });
  }

  ngOnDestroy(): void {
    this.routerEventSubscriber.unsubscribe();
  }
}
