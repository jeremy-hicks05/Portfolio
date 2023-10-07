import { Component } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {

  windowsCurrentUser!: string;
  userHasRole!: boolean;

  constructor() { }

  //constructor(userService: UserService) {
    //userService.get().subscribe(username => {
    //  this.windowsCurrentUser = username;
    //  //alert(this.windowsCurrentUser);
    //});

    //userService.hasRole("ITS Users").subscribe(role => {
    //  this.userHasRole = role == "True";
    //  //alert(role);
    //});
  //}
}
