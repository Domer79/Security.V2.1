import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';
import { CustomInterceptorService } from './system/service/custom-interceptor.service';
import { UsersService } from './services/users.service';
import { CommonService } from './system/service/common.service';
import { SecurityService } from './services/security.service';
import { SettingsComponent } from './settings/settings.component';
import { ApplicationService } from './services/application.service';
import { ApplicationContextService } from './services/application-context.service';
import { ApplicationPageComponent } from './application-page/application-page.component';
import { ApplicationComponent } from './application/application.component';
import { ApplicationInfoComponent } from './application-info/application-info.component';
import { ApplicationRedirectComponent } from './application-redirect/application-redirect.component';
import { HttpNotFoundComponent } from './http-not-found/http-not-found.component';
import { GroupsService } from './services/groups.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    SettingsComponent,
    ApplicationPageComponent,
    ApplicationComponent,
    ApplicationInfoComponent,
    ApplicationRedirectComponent,
    HttpNotFoundComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    NgbModule.forRoot(),
    AppRoutingModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CustomInterceptorService,
      multi: true
    },
    UsersService,
    GroupsService,
    CommonService,
    SecurityService,
    ApplicationService,
    ApplicationContextService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
