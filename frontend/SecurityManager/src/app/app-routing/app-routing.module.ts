import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CommonModule } from '@angular/common';
import { SettingsComponent } from '../settings/settings.component';
import { environment } from '../../environments/environment';
import { ApplicationPageComponent } from '../application-page/application-page.component';
import { ApplicationComponent } from '../application/application.component';
import { ApplicationRedirectComponent } from '../application-redirect/application-redirect.component';
import { HttpNotFoundComponent } from '../http-not-found/http-not-found.component';
import { TestAccessComponent } from '../test-access/test-access.component';
import { LoginComponent } from '../login/login.component';
import { AuthGuard } from '../services/auth.guard';

const appRoutes: Routes = [
  { path: "", component: ApplicationRedirectComponent },
  { path: "notfound", component: HttpNotFoundComponent },
  { path: "to/login/page", component: LoginComponent },
  { 
    path: ":application", 
    canActivate: [AuthGuard],
    component: ApplicationComponent,
    children: [
      {
        path: "",
        redirectTo: "main",
        pathMatch: "full"
      },
      { path: "main", loadChildren: "app/main/main.module#MainModule" },
      { path: "settings", component: SettingsComponent },
      { path: "applications", component: ApplicationPageComponent },
      { path: "testaccess", component: TestAccessComponent },
      { path: "**", redirectTo: "/notfound", pathMatch: "full" },
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(appRoutes, {
      enableTracing: environment.routeTrace
    })
  ],
  declarations: [],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
