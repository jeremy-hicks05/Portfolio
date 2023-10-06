import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable } from 'rxjs';

import { Ticket } from './ticket';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class TicketService
  extends BaseService<Ticket> {
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
  ): Observable<ApiResult<Ticket>> {
    var url = this.getUrl("/api/Tickets");
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

    return this.http.get<ApiResult<Ticket>>(url, { params });
  }

  get(id: number): Observable<Ticket> {
    var url = this.getUrl("/api/Tickets/" + id);
    return this.http.get<Ticket>(url);
  }

  put(item: Ticket): Observable<Ticket> {
    var url = this.getUrl("/api/Tickets/" + item.ticketId);
    return this.http.put<Ticket>(url, item);
  }

  post(item: Ticket): Observable<Ticket> {
    var url = this.getUrl("/api/Tickets");
    return this.http.post<Ticket>(url, item);
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Ticket>> {
    var url = this.getUrl("/api/Tickets");
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

    return this.http.get<ApiResult<Ticket>>(url, { params });
  }

  isDupeTicket(item: Ticket): Observable<boolean> {
    var url = this.getUrl("/api/Tickets/IsDupeTicket");
    return this.http.post<boolean>(url, item);
  }
}
