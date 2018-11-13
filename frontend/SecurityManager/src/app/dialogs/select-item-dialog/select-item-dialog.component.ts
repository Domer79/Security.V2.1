import { Component, OnInit, Output, Input, ViewChild, OnDestroy } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { IEntity } from '../../contracts/models/IEntity';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from '../../services/users.service';
import { Observable } from 'rxjs/Observable';
import { SelectDialog } from '../interfaces/dialog';

@Component({
  selector: 'select-item-dialog',
  templateUrl: './select-item-dialog.component.html',
  styleUrls: ['./select-item-dialog.component.sass']
})
export class SelectItemDialogComponent implements OnInit, SelectDialog {
  selectedItems: IEntity[] = [];
  dialogContent: any;

  @Input() items: Observable<IEntity[]>;
  @Output() save: EventEmitter<IEntity[]> = new EventEmitter<IEntity[]>();
  @Output() cancel: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild('content') content;

  constructor(
    private modalService: NgbModal,
  ) { }

  ngOnInit() {
  }

  open(): void{
    this.modalService.open(this.content).result.then(result => {
      this.save.emit(this.selectedItems);
      this.selectedItems = [];
    }, reason => {
      this.cancel.emit(reason);
      this.selectedItems = [];
    });
  }

  select(item: IEntity): void{
    if (this.selectedItems.some((_: IEntity) => _.Name === item.Name)){
      this.selectedItems.splice(this.selectedItems.indexOf(item), 1);
      return;
    }

    this.selectedItems.push(item);
  }
}
