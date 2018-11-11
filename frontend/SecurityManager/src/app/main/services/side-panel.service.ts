import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidePanelService {

  constructor() { }

  isOpen: boolean;

  open(): void{
    this.isOpen = true;
  }

  close(): void{
    this.isOpen = false;
  }

  checkOpen(): boolean{
    return this.isOpen;
  }
}
