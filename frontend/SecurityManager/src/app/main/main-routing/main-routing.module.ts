import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from '../main.component';
import { AuthGuard } from '../../services/auth.guard';

const mainRoutes: Routes = [
  {
    path: '',
    component: MainComponent,
    canActivateChild: [AuthGuard],
    children: [
      { path: '', redirectTo: "users", pathMatch: 'full' },
      { 
        path: 'users', 
        loadChildren: "app/main/users/users.module#UsersModule",
        canLoad: [AuthGuard] 
      },
      { 
        path: 'groups', 
        loadChildren: "app/main/groups/groups.module#GroupsModule",
        canLoad: [AuthGuard] 
      },
      { 
        path: 'roles', 
        loadChildren: "app/main/roles/roles.module#RolesModule",
        canLoad: [AuthGuard],
      }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(mainRoutes)
  ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class MainRoutingModule { }
