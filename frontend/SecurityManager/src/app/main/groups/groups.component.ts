import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators"
import { Location } from '@angular/common';
import { GroupsService } from '../../services/groups.service';
import { Group } from '../../contracts/models/group';
import { SidePanelService } from '../services/side-panel.service';
import { SecurityService } from '../../services/security.service';
import { Policy } from '../../contracts/models/policy';

@Component({
  selector: 'groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.sass']
})
export class GroupsComponent implements OnInit, AfterViewChecked {
  groups: Observable<Group[]>;
  selectedId: Observable<string>;
  selectedGroup: Group;
  accessed$: Observable<boolean>;

  constructor(
    private router: Router,
    private groupsService: GroupsService,
    private route: ActivatedRoute,
    private sidePanelService: SidePanelService,
    private securityService: SecurityService
  ) {
  }

  ngOnInit() {
    this.accessed$ = this.securityService.checkAccess(Policy.ShowGroups);
    this.groups = this.groupsService.getAll();
    this.sidePanelService.close();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.pipe(map((data: { group: Group }) => {
        return `member${data.group.IdMember}`;
      }));

      this.selectedId.subscribe(selectedId => {
        let memberId = selectedId.substring(6);
        let userId = +memberId;
        this.groups.subscribe(groups => {
          let group = groups.filter((group: Group) => group.IdMember === userId)[0];
          this.selectGroup(group);
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

  createEmptyGroup(): void{
    this.groupsService.createEmpty().toPromise().then(user => {
      this.groups = this.groupsService.getAll();
    });
  }

  selectGroup(group: Group): void{
    this.selectedGroup = group;
    this.navigateToLogin(`${group.Name}`);
    if (!this.sidePanelService.checkOpen())
      this.openPanel();
  }

  navigateToLogin(login: string): void {
    var activatedRoute = this.route.children[0];
    var segments: Observable<UrlSegment[]>[] = [];
    var paths: string[] = [login];
    while (activatedRoute != null){
      let component: any = activatedRoute.component;
      if (component.name === "GroupDetailComponent"){
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

  saveGroup($event: Group):void{
    this.groupsService.update($event).then(res => {
      this.selectGroup($event);
    });
  }
}
