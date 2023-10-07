import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable } from 'rxjs';

import { Category } from './category';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class CategoryService
  extends BaseService<Category> {
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
  ): Observable<ApiResult<Category>> {
    var url = this.getUrl("/api/Categories");
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

    return this.http.get<ApiResult<Category>>(url, { params });
  }

  get(id: number): Observable<Category> {
    var url = this.getUrl("/api/Categories/" + id);
    return this.http.get<Category>(url);
  }

  put(item: Category): Observable<Category> {
    var url = this.getUrl("/api/Categories/" + item.categoryId);
    return this.http.put<Category>(url, item);
  }

  post(item: Category): Observable<Category> {
    var url = this.getUrl("/api/Categories");
    return this.http.post<Category>(url, item);
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Category>> {
    var url = this.getUrl("/api/Categories");
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

    return this.http.get<ApiResult<Category>>(url, { params });
  }

  isDupeApprovalState(item: Category): Observable<boolean> {
    var url = this.getUrl("/api/Categories/IsDupeCategory");
    return this.http.post<boolean>(url, item);
  }
}
