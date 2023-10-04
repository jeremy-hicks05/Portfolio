import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { environment } from './../../environments/environment';

import { Ticket } from './ticket';
import { Impact } from '../impacts/impact';
import { Category } from '../categories/category';
import { TicketSubType } from '../ticket-sub-types/ticket-sub-type';
import { ApprovalState } from '../approval-states/approval-state';
import { MatDatepicker } from '@angular/material/datepicker';
import { DatePipe } from '@angular/common';
import { MatFormField } from '@angular/material/form-field';

//@Directive({ selector: ".test." })
//class ChildDirective { }

@Component({
  selector: 'app-ticket-edit',
  templateUrl: './ticket-edit.component.html',
  styleUrls: ['./ticket-edit.component.scss']
})
export class TicketEditComponent implements OnInit {
  // the view title
  title?: string;

  // the form model
  form!: FormGroup;

  // the ticket object to edit
  ticket?: Ticket;

  // the ticket object id, as fetched from the active route:
  // it's NULL when we're adding a new city
  // and not NULL when we're editing an existing one
  id?: number;

  @ViewChild(MatDatepicker)
  datePicker!: MatDatepicker<any>;

  @ViewChild('[subCat]')
  subCategorySelect!: MatFormField;

  today = new Date();

  // the arrays for the select for the foreign keys
  approvalStates?: ApprovalState[];
  categories?: Category[];
  categoriesTwo?: Category[];
  impacts?: Impact[];
  subTypes?: TicketSubType[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    public datePipe: DatePipe) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      ticketId: new FormControl(''),
      categoryId: new FormControl('', Validators.required),
      subTypeId:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl('', Validators.required) :
          new FormControl({ value: '', disabled: true }, Validators.required),
      impactId: new FormControl('', Validators.required),
      summary: new FormControl('', Validators.required),
      //reasonForRejection: new FormControl({value: 'NA', disabled: true}, Validators.required),
      reasonForRejection:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl({ value: '', disabled: false }) :
          new FormControl({ value: 'NA', disabled: true }),
      approvalStateId:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl({ value: '', disabled: true }, Validators.required) :
          new FormControl({ value: '', disabled: true }, Validators.required),
      approvedBy:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl({ value: '', disabled: true }, Validators.required) :
          new FormControl({ value: 'NA', disabled: true }, Validators.required),
      dateEntered:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl({ value: '', disabled: true }, Validators.required) :
          new FormControl({ value: this.datePipe.transform(this.today, 'yyyy-MM-ddTHH:mm:ss'), disabled: true }, Validators.required),
      dateLastUpdated:
        this.activatedRoute.snapshot.paramMap.get('id') ?
          new FormControl({ value: '', disabled: true }, Validators.required) :
          new FormControl({ value: this.datePipe.transform(this.today, 'yyyy-MM-ddTHH:mm:ss'), disabled: true }, Validators.required),
      enteredByUser: new FormControl({ value: "NA", disabled: true }, Validators.required)
    });
    this.loadData();
  }

  //ngAfterViewInit() {
  //  if (this.form.get('approvalStateId')?.value == "Needs Approval") {
  //    this.form.get('reasonForRejection')?.enable();
  //  }
  //}

  loadData() {

    // load other objects
    this.loadApprovalStates();
    this.loadCategories();
    this.loadImpacts();
    this.loadSubTypes();

    // retrieve the ID from the 'id' parameter
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');

    this.id = idParam ? +idParam : 0;

    if (this.id) {
      // EDIT MODE

      // fetch the ticket from the server
      var url = environment.baseUrl + '/api/Tickets/' + this.id;
      this.http.get<Ticket>(url).subscribe(result => {
        this.ticket = result;
        this.title = "Edit Ticket # - " + this.ticket.ticketId;

        // update the form with the ticket value
        this.form.patchValue(this.ticket);

        if (this.form.controls['approvalStateId'].value != 2) {
          this.form.get('reasonForRejection')?.disable();
        }

      }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      this.title = "Create a new Ticket";
    }
  }

  loadApprovalStates() {
    var url = environment.baseUrl + '/api/ApprovalStates';
    var params = new HttpParams();

    this.http.get<any>(url, { params }).subscribe(result => {
      this.approvalStates = result.data;
    }, error => console.error(error));
  }

  setApprovalState(event: any) {
    var url = environment.baseUrl + '/api/TicketSubTypes/' + event.value;

    var params = new HttpParams();

    this.http.get<TicketSubType>(url, { params }).subscribe(result => {
      result.needsApproval === "No" ?
        this.form.controls['approvalStateId'].setValue(1) :
        this.form.controls['approvalStateId'].setValue(2)
    }, error => console.error(error));
  }

  loadCategories() {
    var url = environment.baseUrl + '/api/Categories';
    var params = new HttpParams();

    this.http.get<any>(url, { params }).subscribe(result => {
      this.categories = result.data;
    }, error => console.error(error));
  }

  loadImpacts() {
    var url = environment.baseUrl + '/api/Impacts';
    var params = new HttpParams();

    this.http.get<any>(url, { params }).subscribe(result => {
      this.impacts = result.data;
    }, error => console.error(error));
  }

  loadSubTypes() {
    var url = environment.baseUrl + '/api/TicketSubTypes';
    var params = new HttpParams();

    this.http.get<any>(url, { params }).subscribe(result => {
      this.subTypes = result.data;
    }, error => console.error(error));
  }

  loadSubTypesFromCategory(event: any) {
    this.form.controls['subTypeId'].enable();
    this.form.controls['subTypeId'].setValue("");
    var url = environment.baseUrl + '/api/TicketSubTypes/filter/' + event.value;
    var params = new HttpParams();

    this.http.get<any>(url, { params }).subscribe(result => {
      this.subTypes = result;
    }, error => console.error(error));
  }

  approve() {
    var ticket = (this.id) ? this.ticket : <Ticket>{};
    if (ticket) {
      ticket.ticketId = +this.form.controls['ticketId'].value;
      ticket.categoryId = +this.form.controls['categoryId'].value;
      ticket.subTypeId = +this.form.controls['subTypeId'].value;
      ticket.impactId = +this.form.controls['impactId'].value;
      ticket.summary = this.form.controls['summary'].value;
      ticket.reasonForRejection = this.form.controls['reasonForRejection'].value;
      ticket.approvalStateId = +this.form.controls['approvalStateId'].value;
      ticket.approvedBy = this.form.controls['approvedBy'].value;
      ticket.dateEntered = this.form.controls['dateEntered'].value;
      ticket.dateLastUpdated = this.form.controls['dateLastUpdated'].value;
      ticket.enteredByUser = this.form.controls['enteredByUser'].value;

      var url = environment.baseUrl + '/api/Tickets/Approve/' + ticket.ticketId;
      this.http
        .put<Ticket>(url, ticket)
        .subscribe(result => {
          console.log("Ticket " + ticket!.ticketId + " has been approved.");

          // go back to tickets view
          this.router.navigate(['/tickets']);

        }, error => console.error(error));
    }
  }

  reject() {
    var ticket = (this.id) ? this.ticket : <Ticket>{};
    if (ticket) {
      ticket.ticketId = +this.form.controls['ticketId'].value;
      ticket.categoryId = +this.form.controls['categoryId'].value;
      ticket.subTypeId = +this.form.controls['subTypeId'].value;
      ticket.impactId = +this.form.controls['impactId'].value;
      ticket.summary = this.form.controls['summary'].value;
      ticket.reasonForRejection = this.form.controls['reasonForRejection'].value;
      ticket.approvalStateId = +this.form.controls['approvalStateId'].value;
      ticket.approvedBy = this.form.controls['approvedBy'].value;
      ticket.dateEntered = this.form.controls['dateEntered'].value;
      ticket.dateLastUpdated = this.form.controls['dateLastUpdated'].value;
      ticket.enteredByUser = this.form.controls['enteredByUser'].value;

      var url = environment.baseUrl + '/api/Tickets/Reject/' + ticket.ticketId;
      this.http
        .put<Ticket>(url, ticket)
        .subscribe(result => {
          console.log("Ticket " + ticket!.ticketId + " has been rejected.");

          // go back to tickets view
          this.router.navigate(['/tickets']);

        }, error => console.error(error));
    }
  }

  onSubmit() {
    var ticket = (this.id) ? this.ticket : <Ticket>{};
    //var ticket = this.ticket;
    var catId = +this.form.controls['categoryId'].value;
    if (ticket) {
      //ticket.ticketId = +this.form.controls['ticketId'].value;
      ticket.categoryId = catId;
      ticket.subTypeId = +this.form.controls['subTypeId'].value;
      ticket.impactId = +this.form.controls['impactId'].value;
      ticket.summary = this.form.controls['summary'].value;
      ticket.reasonForRejection = this.form.controls['reasonForRejection'].value;
      ticket.approvalStateId = +this.form.controls['approvalStateId'].value;
      ticket.approvedBy = this.form.controls['approvedBy'].value;
      ticket.dateEntered = this.form.controls['dateEntered'].value;
      ticket.dateLastUpdated = this.form.controls['dateLastUpdated'].value;
      ticket.enteredByUser = this.form.controls['enteredByUser'].value;
      //ticket.approvalState = this.approvalStates![+this.form.controls['approvalStateId'].value - 1];
      //ticket.approvalState.approvalStateId = 0;
      //ticket.category = this.categories![catId];
      //ticket.impact = this.impacts![+this.form.controls['impactId'].value - 1];
      //ticket.subType = this.subTypes![+this.form.controls['subTypeId'].value - 1];
      //ticket.subType.category = this.categories![catId];
      //ticket.subType.categoryId = catId;

      if (this.id) {
        // EDIT mode

        var url = environment.baseUrl + '/api/Tickets/' + ticket.ticketId;
        this.http
          .put<Ticket>(url, ticket)
          .subscribe(result => {
            console.log("Ticket " + ticket!.ticketId + " has been updated.");

            // go back to tickets view
            this.router.navigate(['/tickets']);

          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        var url = environment.baseUrl + '/api/Tickets';
        this.http
          .post<Ticket>(url, ticket)
          .subscribe(result => {
            console.log("Ticket " + ticket!.ticketId + " has been created.");
            // go back to tickets view
            this.router.navigate(['/tickets']);
          }, error => console.error(error));
      }
    }
  }
}
