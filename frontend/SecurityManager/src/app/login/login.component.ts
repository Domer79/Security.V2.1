import { Component, OnInit } from '@angular/core';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ApplicationContextService } from '../services/application-context.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  notAuth: boolean;

  constructor(
    private tokenService: TokenService,
    private appContext: ApplicationContextService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login(loginOrEmail: string, password: string): void{
    this.authService.createToken(loginOrEmail, password).subscribe((token: string) => {
      if (token == AuthService.NotAuthenticated){
        this.notAuth = true;
        return;
      }
        
      this.tokenService.setToken(token);
      this.appContext.Application.subscribe(app => {
        this.router.navigate([`/${app.AppName}`]);
      });
    });
  }

}
