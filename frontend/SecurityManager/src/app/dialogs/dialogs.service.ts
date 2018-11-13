import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Dialog } from './interfaces/dialog';

@Injectable()
export class DialogsService {

  constructor(
  ) { }

  open(dialog: Dialog){
    dialog.open();
  }
}
