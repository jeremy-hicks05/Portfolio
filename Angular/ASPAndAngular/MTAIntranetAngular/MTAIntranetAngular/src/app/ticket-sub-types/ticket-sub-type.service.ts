import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable } from 'rxjs';

import { TicketSubType } from './ticket-sub-type';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class TicketSubTypeService
  extends BaseService<TicketSubType> {
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
  ): Observable<ApiResult<TicketSubType>> {
    var url = this.getUrl("/api/TicketSubTypes");
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

    return this.http.get<ApiResult<TicketSubType>>(url, { params });
  }

  get(id: number): Observable<TicketSubType> {
    var url = this.getUrl("/api/TicketSubTypes/" + id);
    return this.http.get<TicketSubType>(url);
  }

  put(item: TicketSubType): Observable<TicketSubType> {
    var url = this.getUrl("/api/TicketSubTypes/" + item.ticketSubTypeId);
    return this.http.put<TicketSubType>(url, item);
  }

  post(item: TicketSubType): Observable<TicketSubType> {
    var url = this.getUrl("/api/TicketSubTypes");
    return this.http.post<TicketSubType>(url, item);
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<TicketSubType>> {
    var url = this.getUrl("/api/TicketSubTypes");
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

    return this.http.get<ApiResult<TicketSubType>>(url, { params });
  }

  isDupeTicket(item: TicketSubType): Observable<boolean> {
    var url = this.getUrl("/api/Tickets/IsDupeTicketSubType");
    return this.http.post<boolean>(url, item);
  }
}
