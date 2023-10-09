import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

import { ApprovalState } from './approval-state';

@Component({
  selector: 'app-approval-state-edit',
  templateUrl: './approval-state-edit.component.html',
  styleUrls: ['./approval-state-edit.component.scss']
})

export class ApprovalStateEditComponent implements OnInit {
  // the view title
  title?: string;
  // the form model
  form!: FormGroup;
  // the approval state object to edit or create
  approvalState?: ApprovalState;

  // the approval state object id, as fetched from the active route:
  // it's NULL when we're adding a new approval state
  // and not NULL when we're editing an existing one
  approvalStateId?: number;

  // the approval states array for the select
  approvalStates?: ApprovalState[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient) { }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required)
    }, null, this.isDupeApprovalState());
    this.loadData();
  }

  loadData() {

    // load countries
    this.loadApprovalStates();

    // retrieve the ID from the 'id' parameter
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.approvalStateId = idParam ? +idParam : 0;
    if (this.approvalStateId) {
      // fetch the city from the server
      var url = environment.baseUrl + 'api/ApprovalStates/' + this.approvalStateId;
      this.http.get<ApprovalState>(url).subscribe(result => {
        this.approvalState = result;
        this.title = "Edit - " + this.approvalState.name;
        // update the form with the city value
        this.form.patchValue(this.approvalState);
      }, error => console.error(error));
    }
    else {
      // ADD NEW NODE

      this.title = "Create a new Approval State";
    }
  }
  loadApprovalStates() {
    // fetch all the counrties from the server
    var url = environment.baseUrl + 'api/ApprovalStates';
    var params = new HttpParams()
      .set("pageIndex", 0)
      .set("pageSize", 9999)
      .set("sortColumn", "name");

    this.http.get<any>(url, { params }).subscribe(result => {
      this.approvalStates = result.data;
    }, error => console.error(error));
  }

  onSubmit() {
    var approvalState = (this.approvalStateId) ? this.approvalState : <ApprovalState>{};
    if (approvalState) {
      //approvalState.approvalStateId = +this.form.controls['id'].value;
      approvalState.name = this.form.controls['name'].value;

      if (this.approvalStateId) {

        var url = environment.baseUrl + 'api/ApprovalStates/' + approvalState.approvalStateId;
        this.http
          .put<ApprovalState>(url, approvalState)
          .subscribe(result => {
            console.log("Approval State " + approvalState!.approvalStateId + " has been updated.");
            // go back to approvalSates view
            this.router.navigate(['/approvalStates']);
          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        var url = environment.baseUrl + 'api/ApprovalStates';
        this.http
          .post<ApprovalState>(url, approvalState)
          .subscribe(result => {
            console.log("ApprovalState " + result.name + " has been created.");

            // go back to cities view
            this.router.navigate(['/approvalStates']);
          }, error => console.error(error));
      }
    }
  }
  isDupeApprovalState(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } |
      null> => {
      var approvalState = <ApprovalState>{};
      approvalState.approvalStateId = (this.approvalStateId) ? this.approvalStateId : 0;
      approvalState.name = this.form.controls['name'].value;

      var url = environment.baseUrl + 'api/ApprovalStates/IsDupeApprovalState';
      return this.http.post<boolean>(url, approvalState).pipe(map(result => {
        return (result ? { isDupeApprovalState: true } : null);
      }));
    }
  }
}
