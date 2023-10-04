import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketEditComponent } from './tickets/ticket-edit.component';
import { HealthCheckComponent } from './health-check/health-check.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryEditComponent } from './categories/category-edit.component';
import { ImpactsComponent } from './impacts/impacts.component';
import { ImpactEditComponent } from './impacts/impact-edit.component';
import { ApprovalStatesComponent } from './approval-states/approval-states.component';
import { ApprovalStateEditComponent } from './approval-states/approval-state-edit.component';
import { TicketSubTypesComponent } from './ticket-sub-types/ticket-sub-types.component';
import { TicketSubTypeEditComponent } from './ticket-sub-types/ticket-sub-type-edit.component';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'tickets', component: TicketsComponent },
  { path: 'ticket/:id', component: TicketEditComponent },
  { path: 'mtaIntranet/ticket/:id', component: TicketEditComponent },
  { path: 'ticket', component: TicketEditComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'category/:id', component: CategoryEditComponent },
  { path: 'category', component: CategoryEditComponent },
  { path: 'impacts', component: ImpactsComponent },
  { path: 'impact/:id', component: ImpactEditComponent },
  { path: 'impact', component: ImpactEditComponent },
  { path: 'approvalStates', component: ApprovalStatesComponent },
  { path: 'approvalState/:id', component: ApprovalStateEditComponent },
  { path: 'approvalState', component: ApprovalStateEditComponent },
  { path: 'ticketSubTypes', component: TicketSubTypesComponent },
  { path: 'ticketSubType/:id', component: TicketSubTypeEditComponent },
  { path: 'ticketSubType', component: TicketSubTypeEditComponent },
  { path: 'health-check', component: HealthCheckComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
