import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { NgForm } from '@angular/forms';
import { Role } from '../../../contracts/models/role';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'role-profile',
  templateUrl: './role-profile.component.html',
  styleUrls: ['./role-profile.component.scss']
})
export class RoleProfileComponent implements OnInit {
  idRole: number;
  role$: Observable<Role>;
  constructor(
    private route: ActivatedRoute,
    private rolesService: RolesService
  ) { }

  ngOnInit() {
    this.role$ = this.route.parent.data.pipe(map((data: {role: Role}) => {
      this.idRole = data.role.IdRole;
      return data.role;
    }));
  }

  onSubmit(roleForm: NgForm):void{
    this.role$.subscribe(role => {
      this.rolesService.update(role).then(() => {
        roleForm.control.markAsPristine();
      });
    });
  }

}
