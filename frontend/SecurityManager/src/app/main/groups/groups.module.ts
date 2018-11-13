import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { FormsModule } from '@angular/forms';
import { DialogsModule } from '../../dialogs/dialogs.module';
import { GroupsRoutingModule } from './groups-routing.module';
import { GroupProfileComponent } from './group-profile/group-profile.component';
import { GroupsComponent } from './groups.component';
import { GroupDetailComponent } from './group-detail/group-detail.component';
import { GroupUsersComponent } from './group-users/group-users.component';
import { GroupRolesComponent } from './group-roles/group-roles.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GroupsRoutingModule,
    NgbModule,
    DialogsModule
  ],
  declarations: [
    GroupsComponent,
    GroupProfileComponent,
    GroupDetailComponent,
    GroupUsersComponent,
    GroupRolesComponent,
  ]
})
export class GroupsModule { }
