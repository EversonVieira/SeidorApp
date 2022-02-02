import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/user';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';
import { CurrentUser } from 'src/app/Shared/base-seidor-component/current-user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseSeidorComponent implements OnInit {

  public CurrentUser: User = new User()
  constructor() {
    super();
   }

  ngOnInit(): void {
    this.CurrentUser = CurrentUser.getUser();
  }

  doLogout(){
    localStorage.removeItem("Session");
    this.loginService.doLogout();
  }

  manageAccount(){
    this.router.navigateByUrl("user/update")
  }

}
