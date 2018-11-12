import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router"
import { RoleProfileComponent } from './role-profile/role-profile.component';
import { RolesComponent } from './roles.component';
import { RoleDetailComponent } from './role-detail/role-detail.component';
import { RolePoliciesComponent } from './role-policies/role-policies.component';
import { RoleMembersComponent } from './role-members/role-members.component';
import { RoleDetailResolverService } from './services/roles-detail-resolver.service';

const routes: Routes = [
  { 
    path: '',
    component: RolesComponent,
    children: [
      { 
        path: ':rolename',
        component: RoleDetailComponent,
        resolve: {
          role: RoleDetailResolverService
        },
        children: [
          { path: '', redirectTo: 'profile', pathMatch: 'full' },
          { path: 'profile', component: RoleProfileComponent, },
          { path: 'members', component: RoleMembersComponent },
          { path: 'policy', component: RolePoliciesComponent }
        ]
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [
    RoleDetailResolverService
  ]
})
export class GroupsRoutingModule { }
