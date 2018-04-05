import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing/main-routing.module';
import { MainComponent } from './main.component';
import { GroupsComponent } from './groups/groups.component';
import { RolesComponent } from './roles/roles.component';


@NgModule({
  imports: [
    CommonModule,
    MainRoutingModule,
  ],
  declarations: [
    MainComponent,
    GroupsComponent,
    RolesComponent
  ]
})
export class MainModule { }
