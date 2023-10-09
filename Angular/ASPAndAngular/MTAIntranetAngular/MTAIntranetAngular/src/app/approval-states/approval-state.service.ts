import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';

import { ApprovalState } from './approval-state';
import { Apollo, gql } from 'apollo-angular';
/*import { Country } from './../countries/country';*/

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

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<ApprovalState>> {
    var url = this.getUrl("/api/ApprovalStates");
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

  //get(id: number): Observable<ApprovalState> {
  //  var url = this.getUrl("/api/ApprovalStates/" + id);
  //  return this.http.get<ApprovalState>(url);
  //}

  get(id: number): Observable<ApprovalState> {
    return this.apollo
      .query({
        query: gql`
        query GetApprovalStateById($approvalStateId: Int!)
        {approvalStates(where: { approvalStateId: { eq: $approvalStateId } }) {
          nodes {
            approvalStateId
            name
          }
        }
      }`,
        variables: {
          id
        }
      })
      .pipe(map((result: any) =>
        result.data.approvalStates.nodes[0]));
  }

  put(item: ApprovalState): Observable<ApprovalState> {
    var url = this.getUrl("/api/ApprovalStates/" + item.approvalStateId);
    return this.http.put<ApprovalState>(url, item);
  }

  post(item: ApprovalState): Observable<ApprovalState> {
    var url = this.getUrl("/api/ApprovalStates");
    return this.http.post<ApprovalState>(url, item);
  }

  getApprovalStates(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<ApprovalState>> {
    var url = this.getUrl("/api/ApprovalStates");
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
    var url = this.getUrl("/api/ApprovalStates/IsDupeApprovalState");
    return this.http.post<boolean>(url, item);
  }
}
