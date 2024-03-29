import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ConnectionService } from 'angular-connection-service';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MTAIntranetAngular';

  hasNetworkConnection: boolean = true;
  hasInternetAccess: boolean = true;

  constructor(private connectionService: ConnectionService) {
    this.connectionService.monitor().subscribe((currentState: any) => {
      this.hasNetworkConnection = currentState.hasNetworkConnection;
      this.hasInternetAccess = currentState.hasInternetAccess;
    });
  }

  public isOnline() {
    return this.hasNetworkConnection && this.hasInternetAccess;
  }
}
