import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable } from 'rxjs';

import { Impact } from './impact';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class ImpactService
  extends BaseService<Impact> {
  constructor(
    http: HttpClient) {
    super(http);
  }

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Impact>> {
    var url = this.getUrl("/api/Impacts");
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterColumn && filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }

    return this.http.get<ApiResult<Impact>>(url, { params });
  }

  get(id: number): Observable<Impact> {
    var url = this.getUrl("/api/Impacts/" + id);
    return this.http.get<Impact>(url);
  }

  put(item: Impact): Observable<Impact> {
    var url = this.getUrl("/api/Impacts/" + item.impactId);
    return this.http.put<Impact>(url, item);
  }

  post(item: Impact): Observable<Impact> {
    var url = this.getUrl("/api/Impacts");
    return this.http.post<Impact>(url, item);
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Impact>> {
    var url = this.getUrl("/api/Impacts");
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterColumn && filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }

    return this.http.get<ApiResult<Impact>>(url, { params });
  }

  isDupeApprovalState(item: Impact): Observable<boolean> {
    var url = this.getUrl("/api/Impacts/IsDupeImpact");
    return this.http.post<boolean>(url, item);
  }
}
