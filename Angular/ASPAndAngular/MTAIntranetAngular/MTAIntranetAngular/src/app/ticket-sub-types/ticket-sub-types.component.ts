import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

import { TicketSubType } from './ticket-sub-type';
import { MatSort } from '@angular/material/sort';

import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Category } from '../categories/category';
import { TicketSubTypeService } from './ticket-sub-type.service';

@Component({
  selector: 'app-ticket-sub-types',
  templateUrl: './ticket-sub-types.component.html',
  styleUrls: ['./ticket-sub-types.component.scss']
})
export class TicketSubTypesComponent implements OnInit {
  public displayedColumns: string[] = [
    'ticketSubTypeId',
    'category',
    'name',
    'description',
    'needsApproval',
    'cclist'];

  public ticketSubTypes!: MatTableDataSource<TicketSubType>;
  public categories!: Category[];

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "ticketSubTypeId";
  public defaultSortOrder: "asc" | "desc" = "asc";

  defaultFilterColumn: string = "name";
  filterQuery?: string;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  @ViewChild(MatSort)
  sort!: MatSort;

  filterTextChanged: Subject<string> = new Subject<string>();

  constructor(private ticketSubTypeService: TicketSubTypeService) {

  }

  ngOnInit() {
    this.loadData();
  }

  loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    var sortColumn = (this.sort)
      ? this.sort.active
      : this.defaultSortColumn;

    var sortOrder = (this.sort)
      ? this.sort.direction
      : this.defaultSortOrder;

    var filterColumn = (this.filterQuery)
      ? this.defaultFilterColumn
      : null;

    var filterQuery = (this.filterQuery)
      ? this.filterQuery
      : null;

    this.ticketSubTypeService.getData(
      event.pageIndex,
      event.pageSize,
      sortColumn,
      sortOrder,
      filterColumn,
      filterQuery)

      .subscribe(result => {
        console.log(result);
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.ticketSubTypes = new MatTableDataSource<TicketSubType>(result.data);
      }, error => console.error(error));
  }

  // debounce filter text changes
  onFilterTextChanged(filterText: string) {
    if (this.filterTextChanged.observers.length === 0) {
      this.filterTextChanged.pipe(debounceTime(1000), distinctUntilChanged())
        .subscribe(query => {
          this.loadData(query);
        });
    }
    this.filterTextChanged.next(filterText);
  }

  //loadSubTypes() {
  //  this.http.get<TicketSubType[]>(environment.baseUrl + 'api/ticketsubtypes')
  //    .subscribe(result => {
  //      this.ticketSubTypes = result.data;
  //    }, error => console.error(error));
  //}
}

