import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from '../main.component';
import { GroupsComponent } from '../groups/groups.component';
import { RolesComponent } from '../roles/roles.component';

const mainRoutes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', redirectTo: "users", pathMatch: 'full' },
      { 
        path: 'users', 
        loadChildren: "app/main/users/users.module#UsersModule"
      },
      { path: 'groups', component: GroupsComponent },
      { path: 'roles', component: RolesComponent }
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
