import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';

import { environment } from '../../environments/environment';

import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator, PageEvent } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort';

import { Ticket } from './ticket';
import { TicketService } from './ticket.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {
  public displayedColumns: string[] =
    ['ticketId',
      'categoryId',
      'subTypeId',
      'impactId',
      'summary',
      'reasonForRejection',
      'approvalStateId',
      'approvedby',
      'dateentered',
      'datelastupdated'
    ];
  public tickets!: MatTableDataSource<Ticket>;

  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "ticketId";
  public defaultSortOrder: "asc" | "desc" = "asc";

  defaultFilterColumn: string = "summary";
  filterQuery?: string;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  @ViewChild(MatSort)
  sort!: MatSort;

  constructor(private ticketService: TicketService) {
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

    this.ticketService.getData(
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
        this.tickets = new MatTableDataSource<Ticket>(result.data);
      }, error => console.error(error));
  }
}
