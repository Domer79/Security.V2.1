import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { UsersRoutingModule } from './users-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { UserRolesComponent } from './user-roles/user-roles.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    UsersRoutingModule,
    NgbModule,
  ],
  declarations: [
    UsersComponent,
    ProfileComponent,
    UserDetailComponent,
    UserGroupsComponent,
    UserRolesComponent
  ]
})
export class UsersModule { }
