import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { DialogsService } from '../../../dialogs/dialogs.service';
import { Role } from '../../../contracts/models/role';
import { Policy } from '../../../contracts/models/policy';
import { RolesService } from '../../../services/roles.service';

@Component({
  selector: 'app-role-policies',
  templateUrl: './role-policies.component.html',
  styleUrls: ['./role-policies.component.scss']
})
export class RolePoliciesComponent implements OnInit {

  role: Role;
  policies$: Observable<Policy[]>;
  nonIncludedPolicies$: Observable<Policy[]>;
  selectedPolicies: Policy[] = [];

  constructor(
    private route: ActivatedRoute,
    private rolesService: RolesService,
    private dialogsService: DialogsService
  ) { }

  ngOnInit() {
    this.route.parent.data.subscribe((data: {role: Role}) => {
      this.policies$ = this.rolesService.getPolicies(data.role);
      this.nonIncludedPolicies$ = this.rolesService.getNonIncludedPolicies(data.role);
      this.role = data.role;
    });  
  }

  open(content){
    this.dialogsService.open(content);
  }

  setGrants(policies: Policy[]){
    this.rolesService.setGrants(this.role, policies).then(() => {
      this.policies$ = this.rolesService.getPolicies(this.role);
      this.nonIncludedPolicies$ = this.rolesService.getNonIncludedPolicies(this.role);
      this.selectedPolicies = [];
    });
  }

  cancelSetGrants($event){

  }

  deleteCancel(){

  }

  removePolicies(){
    this.rolesService.deletePoliciesFromRole(this.role, this.selectedPolicies).then(() => {
      this.policies$ = this.rolesService.getPolicies(this.role);
      this.nonIncludedPolicies$ = this.rolesService.getNonIncludedPolicies(this.role);
      this.selectedPolicies = [];
    });
  }

  select(policy: Policy){
    if (this.selectedPolicies.some((_: Policy) => _.Name === policy.Name)){
      this.selectedPolicies.splice(this.selectedPolicies.indexOf(policy), 1);
      return;
    }

    this.selectedPolicies.push(policy);
  }
}
