import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'MTAIntranetAngular';

  //windowsCurrentUser!: string;

  constructor() {
    //userservice.get().subscribe(username => {
    //  this.windowsCurrentUser = username;
    //});
  }
}
