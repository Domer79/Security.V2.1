import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing/main-routing.module';
import { MainComponent } from './main.component';
import { RolesComponent } from './roles/roles.component';
import { SidePanelService } from './services/side-panel.service';


@NgModule({
  imports: [
    CommonModule,
    MainRoutingModule,
  ],
  declarations: [
    MainComponent
  ],
  providers: [
    SidePanelService
  ]
})
export class MainModule { }
