import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router"
import { GroupProfileComponent } from './group-profile/group-profile.component';
import { GroupsComponent } from './groups.component';
import { GroupDetailComponent } from './group-detail/group-detail.component';
import { GroupUsersComponent } from './group-users/group-users.component';
import { GroupRolesComponent } from './group-roles/group-roles.component';
import { GroupDetailResolverService } from './services/group-detail-resolver.service';

const routes: Routes = [
  { 
    path: '',
    component: GroupsComponent,
    children: [
      { 
        path: ':groupname',
        component: GroupDetailComponent,
        resolve: {
          group: GroupDetailResolverService
        },
        children: [
          { path: '', redirectTo: 'profile', pathMatch: 'full' },
          { path: 'profile', component: GroupProfileComponent, },
          { path: 'users', component: GroupUsersComponent },
          { path: 'roles', component: GroupRolesComponent }
        ]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [
    GroupDetailResolverService
  ]
})
export class GroupsRoutingModule { }
