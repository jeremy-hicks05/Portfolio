import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';

import { Impact } from './impact';
import { Apollo, gql } from 'apollo-angular';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class ImpactService
  extends BaseService<Impact> {
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
  //): Observable<ApiResult<Impact>> {
  //  var url = this.getUrl("api/Impacts");
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

  //  return this.http.get<ApiResult<Impact>>(url, { params });
  //}

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Impact>> {
    return this.apollo
      .query({
        query: gql`
          query GetImpactsApiResult(
            $pageIndex: Int!,
            $pageSize: Int!,
            $sortColumn: String,
            $sortOrder: String,
            $filterColumn: String,
            $filterQuery: String) {
            impactsApiResult(
              pageIndex: $pageIndex
              pageSize: $pageSize
              sortColumn: $sortColumn
              sortOrder: $sortOrder
              filterColumn: $filterColumn
              filterQuery: $filterQuery
              ) {
            data {
              impactId
              description
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
        result.data.impactsApiResult));
  }

  //get(id: number): Observable<Impact> {
  //  var url = this.getUrl("api/Impacts/" + id);
  //  return this.http.get<Impact>(url);
  //}

  get(id: number): Observable<Impact> {
    return this.apollo
      .query({
        query: gql`
          query GetImpactById($id: Int!) {
          impacts(where: { impactId: { eq: $id } }) {
          nodes {
              impactId
              description
            }
          }
        }
        `,
        variables: {
          id
        }
      })
      .pipe(map((result: any) =>
        result.data.impacts.nodes[0]));
  }

  //put(item: Impact): Observable<Impact> {
  //  var url = this.getUrl("api/Impacts/" + item.impactId);
  //  return this.http.put<Impact>(url, item);
  //}

  put(input: Impact): Observable<Impact> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation UpdateImpact($impact: ImpactDTOInput!) {
          updateImpact(impactDTO: $impact) {
              impactId
              description
              }
            }
          }
        `,
        variables: {
          impact: input
        }
      }).pipe(map((result: any) =>
        result.data.updateImpact));
  }

  //post(item: Impact): Observable<Impact> {
  //  var url = this.getUrl("api/Impacts");
  //  return this.http.post<Impact>(url, item);
  //}

  post(item: Impact): Observable<Impact> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation AddImpact($impact: ImpactDTOInput!) {
          addImpact(impactDTO: $impact) {
            impactId
            description
          }
        }
        `,
        variables: {
          impact: item
        }
      }).pipe(map((result: any) =>
        result.data.addImpact));
  }

  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Impact>> {
    var url = this.getUrl("api/Impacts");
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
    var url = this.getUrl("api/Impacts/IsDupeImpact");
    return this.http.post<boolean>(url, item);
  }
}
