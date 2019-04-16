import "rxjs/operator/switchMap";
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { ApplicationService } from "../services/application.service";
import { ApplicationContextService } from "../services/application-context.service";
import { AuthService } from "../services/auth.service";

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private appService: ApplicationService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(param => {
      this.appService.getByName(param.application).subscribe(app => {
        if (app != null){
          return;
        }

        this.router.navigate(["/notfound"]);
      });
    });
  }

  exit(): void{
    this.authService.exit().then(res => {
      this.router.navigate(["to/login/page"]);
    });
  }
}
