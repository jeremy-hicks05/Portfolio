import { Component } from '@angular/core';
import { HttpClient, HttpParams, HttpRequest, HttpXhrBackend } from '@angular/common/http';
import { Field } from "./field";

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.scss']
})
export class FieldsComponent {

  public myFields!: Field[];

  constructor() {

    const httpClient = new HttpClient(new HttpXhrBackend({
      build: () => new XMLHttpRequest()
    }));

    var fields = httpClient
      .get<any>("https://mtadev.mta-flint.net:50443/api/fields",
        {withCredentials: true})
      .subscribe(data => {
        this.myFields = data.Fields;
        console.log(data.Fields);
      });
  }
}
