import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

import { Impact } from './impact';

@Component({
  selector: 'app-impact-edit',
  templateUrl: './impact-edit.component.html',
  styleUrls: ['./impact-edit.component.scss']
})
export class ImpactEditComponent implements OnInit{

  // the view title
  title?: string;
  // the form model
  form!: FormGroup;
  // the approval state object to edit or create
  impact?: Impact;

  // the approval state object id, as fetched from the active route:
  // it's NULL when we're adding a new approval state
  // and not NULL when we're editing an existing one
  impactId?: number;

  // the approval states array for the select
  impacts?: Impact[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {

  }

  ngOnInit() {
    this.form = new FormGroup({
      description: new FormControl('', Validators.required)
    }, null, this.isDupeImpact());
    this.loadData();
  }

  loadData() {

    // load countries
    this.loadImpacts();

    // retrieve the ID from the 'id' parameter
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.impactId = idParam ? +idParam : 0;
    if (this.impactId) {
      // fetch the city from the server
      var url = environment.baseUrl + '/api/Impacts/' + this.impactId;
      this.http.get<Impact>(url).subscribe(result => {
        this.impact = result;
        this.title = "Edit - " + this.impact.description;
        // update the form with the city value
        this.form.patchValue(this.impact);
      }, error => console.error(error));
    }
    else {
      // ADD NEW NODE

      this.title = "Create a new Impact";
    }
  }

  loadImpacts() {
    // fetch all the counrties from the server
    var url = environment.baseUrl + '/api/Impacts';
    var params = new HttpParams()
      .set("pageIndex", 0)
      .set("pageSize", 9999)
      .set("sortColumn", "impactId")
      .set("filterColumn", "description");

    this.http.get<any>(url, { params }).subscribe(result => {
      this.impacts = result.data;
    }, error => console.error(error));
  }

  onSubmit() {
    var impact = (this.impactId) ? this.impact: <Impact>{};
    if (impact) {
      //approvalState.approvalStateId = +this.form.controls['id'].value;
      impact.description = this.form.controls['description'].value;

      if (this.impactId) {

        var url = environment.baseUrl + '/api/Impacts/' + impact.impactId;
        this.http
          .put<Impact>(url, impact)
          .subscribe(result => {
            console.log("Impact " + impact!.impactId + " has been updated.");
            // go back to approvalSates view
            this.router.navigate(['/impacts']);
          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        var url = environment.baseUrl + '/api/Impacts';
        this.http
          .post<Impact>(url, impact)
          .subscribe(result => {
            console.log("Impact " + result.description + " has been created.");

            // go back to cities view
            this.router.navigate(['/impacts']);
          }, error => console.error(error));
      }
    }
  }

  isDupeImpact(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } |
      null> => {
      var impact = <Impact>{};
      impact.impactId = (this.impactId) ? this.impactId: 0;
      impact.description = this.form.controls['description'].value;

      var url = environment.baseUrl + '/api/Impacts/IsDupeImpact';
      return this.http.post<boolean>(url, impact).pipe(map(result => {
        return (result ? { isDupeImpact: true } : null);
      }));
    }
  }
}
