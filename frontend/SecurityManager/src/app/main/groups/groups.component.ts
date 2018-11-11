import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators"
import { Location } from '@angular/common';
import { GroupsService } from '../../services/groups.service';
import { Group } from '../../contracts/models/group';
import { SidePanelService } from '../services/side-panel.service';

@Component({
  selector: 'groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.sass']
})
export class GroupsComponent implements OnInit, AfterViewChecked {
  groups: Observable<Group[]>;
  selectedId: Observable<string>;
  selectedGroup: Group;

  constructor(
    private router: Router,
    private groupsService: GroupsService,
    private route: ActivatedRoute,
    private sidePanelService: SidePanelService
  ) {
  }

  ngOnInit() {
    this.groups = this.groupsService.getAll();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.pipe(map((data: { group: Group }) => {
        return `member${data.group.IdMember}`;
      }));

      this.selectedId.subscribe(selectedId => {
        let memberId = selectedId.substring(6);
        let userId = +memberId;
        this.groups.subscribe(groups => {
          let user = groups.filter((group: Group) => group.IdMember === userId)[0];
          this.selectGroup(user);
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
    this.router.navigate([`${group.Name}`], {relativeTo: this.route});
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
