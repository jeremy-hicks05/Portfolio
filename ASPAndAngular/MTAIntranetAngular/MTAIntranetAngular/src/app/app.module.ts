import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HealthCheckComponent } from './health-check/health-check.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AngularMaterialModule } from './angular-material.module';

import { CategoriesComponent } from './categories/categories.component';
import { FieldsComponent } from './fields/fields.component';
import { TicketsComponent } from './tickets/tickets.component';
import { ImpactsComponent } from './impacts/impacts.component';
import { TicketSubTypesComponent } from './ticket-sub-types/ticket-sub-types.component';
import { ApprovalStatesComponent } from './approval-states/approval-states.component';
import { TicketEditComponent } from './tickets/ticket-edit.component';
import { ApprovalStateEditComponent } from './approval-states/approval-state-edit.component';
import { CategoryEditComponent } from './categories/category-edit.component';
import { ImpactEditComponent } from './impacts/impact-edit.component';
import { TicketSubTypeEditComponent } from './ticket-sub-types/ticket-sub-type-edit.component';
import { windAuthenticationInterceptor } from './common/windowAuthenticationInterceptor';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import {
  ConnectionServiceModule,
  ConnectionServiceOptions,
  ConnectionServiceOptionsToken
} from 'angular-connection-service';

import { DatePipe } from '@angular/common';

import { APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    HealthCheckComponent,
    CategoriesComponent,
    FieldsComponent,
    TicketsComponent,
    ImpactsComponent,
    TicketSubTypesComponent,
    ApprovalStatesComponent,
    TicketEditComponent,
    ApprovalStateEditComponent,
    CategoryEditComponent,
    ImpactEditComponent,
    TicketSubTypeEditComponent
  ],
  imports: [
    ApolloModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    ReactiveFormsModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      registrationStrategy: 'registerWhenStable:30000'
    }),
    ConnectionServiceModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: windAuthenticationInterceptor,
      multi: true
    },
    DatePipe,
    {
      provide: ConnectionServiceOptionsToken,
      useValue: <ConnectionServiceOptions>{
        heartbeatUrl: environment.baseUrl + 'api/heartbeat',

      }
    },
    {
      provide: APOLLO_OPTIONS,
      useFactory: (httpLink: HttpLink) => {
        return {
          cache: new InMemoryCache({
            addTypename: false
          }),
          link: httpLink.create({
            uri: environment.baseUrl + 'api/graphql',
          }),
          defaultOptions: {
            watchQuery: { fetchPolicy: 'no-cache' },
            query: { fetchPolicy: 'no-cache' }
          }
        };
      },
      deps: [HttpLink],
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
