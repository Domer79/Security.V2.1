import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { FormsModule } from '@angular/forms';
import { DialogsModule } from '../../dialogs/dialogs.module';
import { GroupsRoutingModule } from './roles-routing.module';
import { RoleProfileComponent } from './role-profile/role-profile.component';
import { RolesComponent } from './roles.component';
import { RoleDetailComponent } from './role-detail/role-detail.component';
import { RolePoliciesComponent } from './role-policies/role-policies.component';
import { RoleMembersComponent } from './role-members/role-members.component';
import { CommonComponentsModule } from '../common-components/common-components.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GroupsRoutingModule,
    NgbModule,
    DialogsModule,
    CommonComponentsModule
  ],
  declarations: [
    RolesComponent,
    RoleProfileComponent,
    RoleDetailComponent,
    RolePoliciesComponent,
    RoleMembersComponent,
  ]
})
export class RolesModule { }
