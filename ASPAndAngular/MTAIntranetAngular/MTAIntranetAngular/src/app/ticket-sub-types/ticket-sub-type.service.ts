import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';

import { TicketSubType } from './ticket-sub-type';
import { Apollo, gql } from 'apollo-angular';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class TicketSubTypeService
  extends BaseService<TicketSubType> {
  constructor(
    http: HttpClient,
    private apollo: Apollo) {
    super(http);
  }

  //getData(
  //  pageIndex: number,
  //  pageSize: number,
  //  sortColumn: string,
  //  sortOrder: string,
  //  filterColumn: string | null,
  //  filterQuery: string | null
  //): Observable<ApiResult<TicketSubType>> {
  //  var url = this.getUrl("api/TicketSubTypes");
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

  //  return this.http.get<ApiResult<TicketSubType>>(url, { params });
  //}

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<TicketSubType>> {
    return this.apollo
      .query({
        query: gql`
          query GetTicketSubTypesApiResult(
            $pageIndex: Int!,
            $pageSize: Int!,
            $sortColumn: String,
            $sortOrder: String,
            $filterColumn: String,
            $filterQuery: String) {
            ticketSubTypesApiResult(
              pageIndex: $pageIndex
              pageSize: $pageSize
              sortColumn: $sortColumn
              sortOrder: $sortOrder
              filterColumn: $filterColumn
              filterQuery: $filterQuery) {
            data {
              ticketSubTypeId
              categoryId
              name
              description
              needsApproval
              cclist
              categoryName
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
        result.data.ticketSubTypesApiResult));
  }

  //get(id: number): Observable<TicketSubType> {
  //  var url = this.getUrl("api/TicketSubTypes/" + id);
  //  return this.http.get<TicketSubType>(url);
  //}

  get(id: number): Observable<TicketSubType> {
    return this.apollo
      .query({
        query: gql`
          query GetTicketSubTypeById($id: Int!) {
          subTypes(where: { ticketSubTypeId: { eq: $id } }) {
          nodes {
              ticketSubTypeId
              categoryId
              name
              description
              needsApproval
              ccList
            }
          }
        }
        `,
        variables: {
          id
        }
      })
      .pipe(map((result: any) =>
        result.data.subTypes.nodes[0]));
  }

  //put(item: TicketSubType): Observable<TicketSubType> {
  //  var url = this.getUrl("api/TicketSubTypes/" + item.ticketSubTypeId);
  //  return this.http.put<TicketSubType>(url, item);
  //}

  put(input: TicketSubType): Observable<TicketSubType> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation UpdateTicketSubType($ticketSubType: TicketSubTypeDTOInput!) {
          updateTicketSubType(ticketSubTypeDTO: $ticketSubType) {
              ticketSubTypeId
              categoryId
              name
              description
              needsApproval
              ccList
              }
            }
          }
        `,
        variables: {
          ticketSubType: input
        }
      }).pipe(map((result: any) =>
        result.data.updateTicketSubType));
  }

  //post(item: TicketSubType): Observable<TicketSubType> {
  //  var url = this.getUrl("api/TicketSubTypes");
  //  return this.http.post<TicketSubType>(url, item);
  //}

  post(item: TicketSubType): Observable<TicketSubType> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation AddTicketSubType($ticketSubType: TicketSubTypeDTOInput!) {
          addTicketSubType(ticketSubTypeDTO: $ticketSubType) {
            ticketSubTypeId
            categoryId
              name
              description
              needsApproval
              ccList
          }
        }
        `,
        variables: {
          ticketSubType: item
        }
      }).pipe(map((result: any) =>
        result.data.addTicketSubType));
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<TicketSubType>> {
    var url = this.getUrl("api/TicketSubTypes");
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
    var url = this.getUrl("api/Tickets/IsDupeTicketSubType");
    return this.http.post<boolean>(url, item);
  }
}
