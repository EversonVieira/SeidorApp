import { Component, OnInit } from '@angular/core';
import { throws } from 'assert';
import { User } from 'src/app/Models/user';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseSeidorComponent implements OnInit {

  public User:User = new User(); 
  constructor() { 
    super();
  }

  ngOnInit(): void {
  }

  doLogin(){
    this.loginService.doLogin(this.User).subscribe(response => {
      console.log(response);
      if(response.hasResponseData){
        localStorage.setItem("Session", <string> response.data);
      }
      this.ShowNotifications(response);
    })
  }

}
