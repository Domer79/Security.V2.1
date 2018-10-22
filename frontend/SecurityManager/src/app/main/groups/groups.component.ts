import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators"
import { Location } from '@angular/common';
import { GroupsService } from '../../services/groups.service';
import { Group } from '../../contracts/models/group';

@Component({
  selector: 'groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit, AfterViewChecked {
  groups: Observable<Group[]>;
  selectedId: Observable<string>;

  constructor(
    private router: Router,
    private groupsService: GroupsService,
    private route: ActivatedRoute,
    private location: Location
  ) {
  }

  ngOnInit() {
    this.groups = this.groupsService.getAll();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.pipe(map((data: { group: Group }) => {
        return `member${data.group.IdMember}`;
      }));
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
