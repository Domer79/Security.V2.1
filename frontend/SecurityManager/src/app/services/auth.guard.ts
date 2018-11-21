import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild, CanLoad, Route, Router, RouteReuseStrategy } from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from './auth.service';
import { TokenService } from './token.service';
import { isNullOrUndefined } from 'util';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate
  // CanActivateChild, 
  // CanLoad 
  {
  constructor(
    private authService: AuthService,
    private tokenService: TokenService,
    private router: Router
  ){}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      //return of(true);
      let canActivate = this.checkToken();
      canActivate.subscribe((res: boolean) => {
        console.log(`CanActivate is: ${res}`);
      });
      return canActivate;;
  }

  // canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
  //   return this.checkToken();
  // }

  // canLoad(route: Route): Observable<boolean>  {
  //   return this.checkToken();
  // }

  checkToken(): Observable<boolean>{
    let token = this.tokenService.token;
    if (isNullOrUndefined(token)){
      this.router.navigate(["to/login/page"]);
      return of<boolean>(false);
    }

    return this.authService.checkTokenExpire(token).pipe(map((auth: boolean) => {
      if (!auth)
        this.router.navigate(["to/login/page"]);

      return auth;
    }));
  }
}
