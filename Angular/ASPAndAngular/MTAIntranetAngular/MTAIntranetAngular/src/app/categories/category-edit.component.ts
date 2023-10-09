import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from './../../environments/environment';

import { Category } from './category';

@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit{
  // the view title
  title?: string;
  // the form model
  form!: FormGroup;
  // the approval state object to edit or create
  category?: Category;

  // the approval state object id, as fetched from the active route:
  // it's NULL when we're adding a new approval state
  // and not NULL when we're editing an existing one
  categoryId?: number;

  // the approval states array for the select
  categories?: Category[];

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {

  }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required)
    }, null, this.isDupeCategory());
    this.loadData();
  }

  loadData() {

    // load countries
    this.loadCategories();

    // retrieve the ID from the 'id' parameter
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.categoryId = idParam ? +idParam : 0;
    if (this.categoryId) {
      // fetch the city from the server
      var url = environment.baseUrl + 'api/Categories/' + this.categoryId;
      this.http.get<Category>(url).subscribe(result => {
        this.category = result;
        this.title = "Edit - " + this.category.name;
        // update the form with the city value
        this.form.patchValue(this.category);
      }, error => console.error(error));
    }
    else {
      // ADD NEW NODE

      this.title = "Create a new Category";
    }
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
    var category = (this.categoryId) ? this.category : <Category>{};
    if (category) {
      //approvalState.approvalStateId = +this.form.controls['id'].value;
      category.name = this.form.controls['name'].value;

      if (this.categoryId) {

        var url = environment.baseUrl + 'api/Categories/' + category.categoryId;
        this.http
          .put<Category>(url, category)
          .subscribe(result => {
            console.log("Category " + category!.categoryId + " has been updated.");
            // go back to approvalSates view
            this.router.navigate(['/categories']);
          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        var url = environment.baseUrl + 'api/Categories';
        this.http
          .post<Category>(url, category)
          .subscribe(result => {
            console.log("Category " + result.name + " has been created.");

            // go back to cities view
            this.router.navigate(['/categories']);
          }, error => console.error(error));
      }
    }
  }

  isDupeCategory(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } |
      null> => {
      var category = <Category>{};
      category.categoryId = (this.categoryId) ? this.categoryId : 0;
      category.name = this.form.controls['name'].value;

      var url = environment.baseUrl + 'api/Categories/IsDupeCategory';
      return this.http.post<boolean>(url, category).pipe(map(result => {
        return (result ? { isDupeCategory: true } : null);
      }));
    }
  }
}
