import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService, ApiResult } from '../services/base.service';
import { Observable, map } from 'rxjs';

import { Category } from './category';
import { Apollo, gql } from 'apollo-angular';
/*import { Country } from './../countries/country';*/

@Injectable({
  providedIn: 'root',
})
export class CategoryService
  extends BaseService<Category> {
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
  //): Observable<ApiResult<Category>> {
  //  var url = this.getUrl("api/Categories");
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

  //  return this.http.get<ApiResult<Category>>(url, { params });
  //}

  //get(id: number): Observable<Category> {
  //  var url = this.getUrl("api/Categories/" + id);
  //  return this.http.get<Category>(url);
  //}

  getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Category>> {
    return this.apollo
      .query({
        query: gql`
          query GetCategoriesApiResult(
            $pageIndex: Int!,
            $pageSize: Int!,
            $sortColumn: String,
            $sortOrder: String,
            $filterColumn: String,
            $filterQuery: String) {
            categoriesApiResult(
              pageIndex: $pageIndex
              pageSize: $pageSize
              sortColumn: $sortColumn
              sortOrder: $sortOrder
              filterColumn: $filterColumn
              filterQuery: $filterQuery
              ) {
            data {
              categoryId
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
        result.data.categoriesApiResult));
  }

  get(id: number): Observable<Category> {
    return this.apollo
      .query({
        query: gql`
          query GetCategoryById($id: Int!) {
          categories(where: { categoryId: { eq: $id } }) {
          nodes {
              categoryId
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
        result.data.categories.nodes[0]));
  }

  //put(item: Category): Observable<Category> {
  //  var url = this.getUrl("api/Categories/" + item.categoryId);
  //  return this.http.put<Category>(url, item);
  //}

  put(input: Category): Observable<Category> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation UpdateCategory($category: CategoryDTOInput!) {
          updateCategory(categoryDTO: $category) {
              categoryId
              name
              }
            }
          }
        `,
        variables: {
          category: input
        }
      }).pipe(map((result: any) =>
        result.data.updateCategory));
  }

  //post(item: Category): Observable<Category> {
  //  var url = this.getUrl("api/Categories");
  //  return this.http.post<Category>(url, item);
  //}

  post(item: Category): Observable<Category> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation AddCategory($category: CategoryDTOInput!) {
          addCategory(categoryDTO: $category) {
            categoryId
            name
          }
        }
        `,
        variables: {
          category: item
        }
      }).pipe(map((result: any) =>
        result.data.addCategory));
  }



  getCategories(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<Category>> {
    var url = this.getUrl("api/Categories");
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
    var url = this.getUrl("api/Categories/IsDupeCategory");
    return this.http.post<boolean>(url, item);
  }
}
