import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';

import { Ticket } from './ticket';
import { Apollo, gql } from 'apollo-angular';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class TicketService
  extends BaseService<Ticket> {
  constructor(
    http: HttpClient,
  private apollo : Apollo) {
    super(http);
  }

  //getData(
  //  pageIndex: number,
  //  pageSize: number,
  //  sortColumn: string,
  //  sortOrder: string,
  //  filterColumn: string | null,
  //  filterQuery: string | null
  //): Observable<ApiResult<Ticket>> {
  //  var url = this.getUrl("api/Tickets");
  //  var params = new HttpParams()
  //    .set("pageIndex", pageIndex.toString())
  //    .set("pageSize", pageSize.toString())
  //    .set("sortColumn", sortColumn)
  //    .set("sortOrder", sortOrder);

  //  if (filterColumn && filterQuery) {
  //    params = params
  //      .set("filterColumn", filterColumn)
  //      .set("filterQuery", filterQuery);
  //  }

  //  return this.http.get<ApiResult<Ticket>>(url, { params });
  //}

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Ticket>> {
    return this.apollo
      .query({
        query: gql`
          query GetTicketsApiResult(
            $pageIndex: Int!,
            $pageSize: Int!,
            $sortColumn: String,
            $sortOrder: String,
            $filterColumn: String,
            $filterQuery: String) {
            ticketsApiResult(
              pageIndex: $pageIndex
              pageSize: $pageSize
              sortColumn: $sortColumn
              sortOrder: $sortOrder
              filterColumn: $filterColumn
              filterQuery: $filterQuery
              ) {
            data {
              ticketId
              categoryId
              subTypeId
              impactId
              summary
              reasonForRejection
              approvalStateId
              approvedBy
              dateEntered
              dateLastUpdated
              enteredByUser
              categoryName
              subTypeName
              impactName
              approvalStateName
            },
            pageIndex
            pageSize
            totalCount
            totalPages
            sortColumn
            sortOrder
            filterColumn
            filterQuery
          }
        }
        `,
        variables: {
          pageIndex,
          pageSize,
          sortColumn,
          sortOrder,
          filterColumn,
          filterQuery
        }
      })
      .pipe(map((result: any) =>
        result.data.ticketsApiResult));
  }

  //get(id: number): Observable<Ticket> {
  //  var url = this.getUrl("api/Tickets/" + id);
  //  return this.http.get<Ticket>(url);
  //}

  get(id: number): Observable<Ticket> {
    return this.apollo
      .query({
        query: gql`
          query GetTicketById($id: Int!) {
          tickets(where: { ticketId: { eq: $id } }) {
          nodes {
              ticketId
              categoryId
              subTypeId
              impactId
              summary
              reasonForRejection
              approvalStateId
              approvedBy
              dateEntered
              dateLastUpdated
              enteredByUser
              categoryName
              subTypeName
              impactName
              approvalStateName
            }
          }
        }
        `,
        variables: {
          id
        }
      })
      .pipe(map((result: any) =>
        result.data.tickets.nodes[0]));
  }

  //put(item: Ticket): Observable<Ticket> {
  //  var url = this.getUrl("api/Tickets/" + item.ticketId);
  //  return this.http.put<Ticket>(url, item);
  //}

  // TODO: Add category, subType, impact, and approvalState names??
  put(input: Ticket): Observable<Ticket> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation UpdateTicket($ticket: TicketDTOInput!) {
          updateTicket(ticketDTO: $ticket) {
              ticketId
              categoryId
              subTypeId
              impactId
              summary
              reasonForRejection
              approvalStateId
              approvedBy
              dateEntered
              dateLastUpdated
              enteredByUser
              }
            }
          }
        `,
        variables: {
          ticket: input
        }
      }).pipe(map((result: any) =>
        result.data.updateTicket));
  }

  //post(item: Ticket): Observable<Ticket> {
  //  var url = this.getUrl("api/Tickets");
  //  return this.http.post<Ticket>(url, item);
  //}

  post(item: Ticket): Observable<Ticket> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation AddTicket($ticket: TicketDTOInput!) {
          addTicket(ticketDTO: $ticket) {
            ticketId
            categoryId
              subTypeId
              impactId
              summary
              reasonForRejection
              approvalStateId
              approvedBy
              dateEntered
              dateLastUpdated
              enteredByUser
          }
        }
        `,
        variables: {
          ticket: item
        }
      }).pipe(map((result: any) =>
        result.data.addTicket));
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Ticket>> {
    var url = this.getUrl("api/Tickets");
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
    var url = this.getUrl("api/Tickets/IsDupeTicket");
    return this.http.post<boolean>(url, item);
  }
}
