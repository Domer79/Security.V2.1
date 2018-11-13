import { Component, OnInit, Input, Output, ViewChild, EventEmitter } from '@angular/core';
import { Dialog } from '../interfaces/dialog';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'confirm-dialog',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent implements OnInit, Dialog {

  message: string;
  @Input('confirmmessage') set confirmmessage(value: string){
    this.message = value;
  }
  @Output() ok: EventEmitter<any> = new EventEmitter<any>();
  @Output() cancel: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild('content') content;
  
  constructor(
    private modalService: NgbModal
  ) { }

  ngOnInit() {
  }

  open(): void {
    this.modalService.open(this.content).result.then(result => {
      this.ok.emit();
    }, reason => {
      this.cancel.emit();
    });
  }

}
