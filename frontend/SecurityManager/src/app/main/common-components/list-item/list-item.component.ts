import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IEntity } from '../../../contracts/models/IEntity';

@Component({
  selector: 'list-item',
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.sass']
})
export class ListItemComponent implements OnInit {

  @Input() item: IEntity;
  @Output() saveItem = new EventEmitter()

  constructor() { }

  ngOnInit() {
  }

  onBlur(): void{
    this.saveItem.emit(this.item);
  }
}
