import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  get token(): string{
    return window.localStorage.getItem("token")
  }

  setToken(token: string): void{
    window.localStorage.setItem("token", token);
  }
}
