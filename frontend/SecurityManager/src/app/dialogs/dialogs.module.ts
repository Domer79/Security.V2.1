import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectItemDialogComponent } from './select-item-dialog/select-item-dialog.component';
import { DialogsService } from './dialogs.service';
import { ConfirmComponent } from './confirm/confirm.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [SelectItemDialogComponent, ConfirmComponent],
  exports: [
    SelectItemDialogComponent,
    ConfirmComponent
  ],
  providers: [DialogsService]
})
export class DialogsModule { }
