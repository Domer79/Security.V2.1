import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CommonModule } from '@angular/common';
import { SettingsComponent } from '../settings/settings.component';
import { environment } from '../../environments/environment';

const appRoutes: Routes = [
  {
    path: "",
    redirectTo: "/main",
    pathMatch: "full"
  },
  {
    path: "main",
    loadChildren: "app/main/main.module#MainModule"
  },
  {
    path: "settings",
    component: SettingsComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes, {
      enableTracing: !environment.production
    })
  ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
