import { Component, OnInit } from '@angular/core';

type LinkEnum = 'USERS'|'GROUPS'|'ROLES';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  link: LinkEnum;

  selectPage(selectLink: LinkEnum):void{
    this.link = selectLink;
  }
}
