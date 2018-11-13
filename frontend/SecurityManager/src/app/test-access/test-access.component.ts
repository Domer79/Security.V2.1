import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../contracts/models/user';
import { Policy } from '../contracts/models/policy';
import { UsersService } from '../services/users.service';
import { PolicyService } from '../services/policy.service';
import { Application } from '../contracts/models/Application';
import { ApplicationContextService } from '../services/application-context.service';
import { first, map } from 'rxjs/operators';
import { CommonSecurityService } from '../services/common-security.service';

@Component({
  selector: 'app-test-access',
  templateUrl: './test-access.component.html',
  styleUrls: ['./test-access.component.sass']
})
export class TestAccessComponent implements OnInit {
  selectedUser: string;
  selectedPolicy: string;
  users$: Observable<User[]>;
  policies$: Observable<Policy[]>;
  result$: Observable<string>;
  get application$(): Observable<Application>{
    return this.appContext.Application;
  }  

  constructor(
    private usersService: UsersService,
    private policyService: PolicyService,
    private appContext: ApplicationContextService,
    private commonSecurityService: CommonSecurityService
  ) { }

  ngOnInit() {
    this.users$ = this.usersService.getAll();
    this.policies$ = this.policyService.getAll();

    this.users$.subscribe(u => {
      if (u.length > 0)
        this.selectedUser = u[0].Login;
    });

    this.policies$.subscribe(p => {
      if (p.length > 0)
        this.selectedPolicy = p[0].Name;
    });
  }

  selectPolicy($event){
    this.selectedPolicy = $event.target.value;
    console.log($event.target.value);
  }

  selectUser($event){
    this.selectedUser = $event.target.value;
    console.log($event.target.value);
  }

  checkAccess(){
    this.result$ = this.commonSecurityService.checkAccess(this.selectedUser, this.selectedPolicy).pipe(map(_ => _ === true ? "true" : "false"));
  }
}
