import { Component, OnInit, AfterViewChecked } from '@angular/core';
import { Router, ActivatedRoute, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators"
import { RolesService } from '../../services/roles.service';
import { SidePanelService } from '../services/side-panel.service';
import { Role } from '../../contracts/models/role';
import { RoleDetailComponent } from './role-detail/role-detail.component';
import { SecurityService } from '../../services/security.service';
import { Policy } from '../../contracts/models/policy';

@Component({
  selector: 'roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.sass']
})
export class RolesComponent implements OnInit, AfterViewChecked {
  roles: Observable<Role[]>;
  selectedId: Observable<string>;
  selectedRole: Role;
  accessed$: Observable<boolean>;

  constructor(
    private router: Router,
    private rolesService: RolesService,
    private route: ActivatedRoute,
    private sidePanelService: SidePanelService,
    private securityService: SecurityService
  ) {
  }

  ngOnInit() {
    this.accessed$ = this.securityService.checkAccess(Policy.ShowRoles);
    this.roles = this.rolesService.getAll();
    this.sidePanelService.close();

    if (this.route.firstChild != null) {
      this.selectedId = this.route.firstChild.data.pipe(map((data: { role: Role }) => {
        return `role${data.role.IdRole}`;
      }));

      this.selectedId.subscribe(selectedId => {
        let roleId = selectedId.substring(4);
        let id = +roleId;
        this.roles.subscribe(roles => {
          let role = roles.filter((role: Role) => role.IdRole === id)[0];
          this.selectRole(role);
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

  createEmptyRole(): void{
    this.rolesService.createEmpty().toPromise().then(() => {
      this.roles = this.rolesService.getAll();
    });
  }

  selectRole(role: Role): void{
    this.selectedRole = role;
    this.navigateByRoleName(`${role.Name}`);
    if (!this.sidePanelService.checkOpen())
      this.openPanel();
  }

  navigateByRoleName(roleName: string): void {
    var activatedRoute = this.route.children[0];
    var segments: Observable<UrlSegment[]>[] = [];
    var paths: string[] = [roleName];
    while (activatedRoute != null){
      let component: any = activatedRoute.component;
      if (component.name === RoleDetailComponent.name){
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

  saveRole($event: Role): void{
    this.rolesService.update($event).then(res => {
      this.selectRole($event);
    });
  }
}
