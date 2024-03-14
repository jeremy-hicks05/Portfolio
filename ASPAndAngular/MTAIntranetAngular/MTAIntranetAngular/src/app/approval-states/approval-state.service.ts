import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';
import { Apollo, gql } from 'apollo-angular';

import { ApprovalState } from './approval-state';

@Injectable({
  providedIn: 'root',
})
export class ApprovalStateService
  extends BaseService<ApprovalState> {
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
  //): Observable<ApiResult<ApprovalState>> {
  //  var url = this.getUrl("api/ApprovalStates");
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

  //  return this.http.get<ApiResult<ApprovalState>>(url, { params });
  //}



  //get(id: number): Observable<ApprovalState> {
  //  var url = this.getUrl("api/ApprovalStates/" + id);
  //  return this.http.get<ApprovalState>(url);
  //}

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<ApprovalState>> {
    return this.apollo
      .query({
        query: gql`
          query GetApprovalStatesApiResult(
          $pageIndex: Int!,
          $pageSize: Int!,
          $sortColumn: String,
          $sortOrder: String,
          $filterColumn: String,
          $filterQuery: String) {
          approvalStatesApiResult(
            pageIndex: $pageIndex
            pageSize: $pageSize
            sortColumn: $sortColumn
            sortOrder: $sortOrder
            filterColumn: $filterColumn
            filterQuery: $filterQuery) {
              data {
                approvalStateId
                name
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
        result.data.approvalStatesApiResult));
  }

  get(id: number): Observable<ApprovalState> {
    return this.apollo
      .query({
        query: gql`
          query GetApprovalStateById($id: Int!) {
          approvalStates(where: { approvalStateId: { eq: $id } }) {
          nodes {
              approvalStateId
              name
            }
          }
        }
        `,
        variables: {
          id
        }
      })
      .pipe(map((result: any) =>
        result.data.approvalStates.nodes[0]));
  }


  put(input: ApprovalState): Observable<ApprovalState> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation UpdateApprovalState($approvalState: ApprovalStateDTOInput!) {
          updateApprovalState(approvalStateDTO: $approvalState) {
              approvalStateId
              name
              }
            }
          }
        `,
        variables: {
          approvalState: input
        }
      }).pipe(map((result: any) =>
        result.data.updateApprovalState));
  }

  //post(item: ApprovalState): Observable<ApprovalState> {
  //  var url = this.getUrl("api/ApprovalStates");
  //  return this.http.post<ApprovalState>(url, item);
  //}

  post(item: ApprovalState): Observable<ApprovalState> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation AddApprovalState($approvalState: ApprovalStateDTOInput!) {
          addApprovalState(approvalStateDTO: $approvalState) {
            approvalStateId
            name
          }
        }
        `,
        variables: {
          approvalState: item
        }
      }).pipe(map((result: any) =>
        result.data.addApprovalState));
  }

  getApprovalStates(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<ApprovalState>> {
    var url = this.getUrl("api/ApprovalStates");
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

    return this.http.get<ApiResult<ApprovalState>>(url, { params });
  }

  isDupeApprovalState(item: ApprovalState): Observable<boolean> {
    var url = this.getUrl("api/ApprovalStates/IsDupeApprovalState");
    return this.http.post<boolean>(url, item);
  }
}
