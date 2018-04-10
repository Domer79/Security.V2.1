import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { UsersComponent } from './users.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { UserRolesComponent } from './user-roles/user-roles.component';
import { UserDetailResolverService } from './services/user-detail-resolver.service';

const routes: Routes = [
  { 
    path: '',
    component: UsersComponent,
    children: [
      { 
        path: ':username',
        component: UserDetailComponent,
        resolve: {
          user: UserDetailResolverService
        },
        children: [
          { path: '', component: ProfileComponent, },
          { path: 'groups', component: UserGroupsComponent },
          { path: 'roles', component: UserRolesComponent }
        ]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [
    UserDetailResolverService
  ]
})
export class UsersRoutingModule { }
