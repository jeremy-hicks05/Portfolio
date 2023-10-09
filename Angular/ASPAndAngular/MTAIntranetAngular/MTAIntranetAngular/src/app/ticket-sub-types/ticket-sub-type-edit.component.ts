import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn, PatternValidator } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

import { TicketSubType } from './ticket-sub-type';
import { Category } from '../categories/category';

@Component({
  selector: 'app-ticket-sub-type-edit',
  templateUrl: './ticket-sub-type-edit.component.html',
  styleUrls: ['./ticket-sub-type-edit.component.scss']
})
export class TicketSubTypeEditComponent implements OnInit {
  // the view title
  title?: string;
  // the form model
  form!: FormGroup;
  // the approval state object to edit or create
  subType?: TicketSubType;

  // the approval state object id, as fetched from the active route:
  // it's NULL when we're adding a new approval state
  // and not NULL when we're editing an existing one
  subTypeId?: number;

  // the approval states array for the select
  subTypes?: TicketSubType[];
  categories?: Category[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {

  }

  ngOnInit() {
    this.form = new FormGroup({
      categoryId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      needsApproval: new FormControl('', Validators.required),
      cclist: new FormControl('', Validators.required)
    }, null, this.isDupeSubType());
    this.loadData();
    this.loadCategories();
  }

  loadData() {

    // load countries
    this.loadSubTypes();

    // retrieve the ID from the 'id' parameter
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.subTypeId = idParam ? +idParam : 0;
    if (this.subTypeId) {
      // fetch the city from the server
      var url = environment.baseUrl + 'api/TicketSubTypes/' + this.subTypeId;
      this.http.get<TicketSubType>(url).subscribe(result => {
        this.subType = result;
        this.title = "Edit - " + this.subType.name;
        // update the form with the city value
        this.form.patchValue(this.subType);
      }, error => console.error(error));
    }
    else {
      // ADD NEW NODE

      this.title = "Create a new Sub Type";
    }
  }

  loadSubTypes() {
    // fetch all the counrties from the server
    var url = environment.baseUrl + 'api/TicketSubTypes';
    var params = new HttpParams()
      .set("pageIndex", 0)
      .set("pageSize", 9999)
      .set("sortColumn", "ticketSubTypeId");

    this.http.get<any>(url, { params }).subscribe(result => {
      this.subTypes = result.data;
    }, error => console.error(error));
  }

  loadCategories() {
    // fetch all the counrties from the server
    var url = environment.baseUrl + 'api/Categories';
    var params = new HttpParams()
      .set("pageIndex", 0)
      .set("pageSize", 9999)
      .set("sortColumn", "name");

    this.http.get<any>(url, { params }).subscribe(result => {
      this.categories = result.data;
    }, error => console.error(error));
  }

  onSubmit() {
    var subType = (this.subTypeId) ? this.subType : <TicketSubType>{};
    if (subType) {
      subType.description = this.form.controls['description'].value;
      subType.ccList = this.form.controls['cclist'].value;
      subType.name = this.form.controls['name'].value;
      subType.categoryId = this.form.controls['categoryId'].value;
      subType.needsApproval = this.form.controls['needsApproval'].value;

      if (this.subTypeId) {

        var url = environment.baseUrl + 'api/TicketSubTypes/' + subType.ticketSubTypeId;
        this.http
          .put<TicketSubType>(url, subType)
          .subscribe(result => {
            console.log("SubType " + subType!.ticketSubTypeId + " has been updated.");
            // go back to approvalSates view
            this.router.navigate(['/ticketSubTypes']);
          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        var url = environment.baseUrl + 'api/TicketSubTypes';
        this.http
          .post<TicketSubType>(url, subType)
          .subscribe(result => {
            console.log("SubType " + result.name + " has been created.");

            // go back to subtypes view
            this.router.navigate(['/ticketSubTypes']);
          }, error => console.error(error));
      }
    }
  }

  isDupeSubType(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } |
      null> => {
      var subType = <TicketSubType>{};
      subType.ticketSubTypeId = (this.subTypeId) ? this.subTypeId: 0;
      subType.description = this.form.controls['description'].value;
      subType.ccList = this.form.controls['cclist'].value;
      subType.name = this.form.controls['name'].value;
      subType.needsApproval = this.form.controls['needsApproval'].value;
      subType.categoryId = +this.form.controls['categoryId'].value;

      var url = environment.baseUrl + 'api/TicketSubTypes/IsDupeSubType';
      return this.http.post<boolean>(url, subType).pipe(map(result => {
        return (result ? { isDupeSubType: true } : null);
      }));
    }
  }
}
