import { Component, OnInit } from '@angular/core';
import { DTO_RegisterUser, DTO_UpdateUser } from 'src/app/Models/DTO_RegisterUser';
import { User } from 'src/app/Models/user';
import { UserService } from 'src/app/Services/user.service';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';
import { CurrentUser } from 'src/app/Shared/base-seidor-component/current-user';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.scss']
})
export class UserUpdateComponent extends BaseSeidorComponent implements OnInit {

  public User:DTO_UpdateUser = <DTO_UpdateUser> CurrentUser.getUser(); 

  constructor(private userService:UserService) {
    super();
   }

  ngOnInit(): void {
  }

  save(){
    this.userService.update(this.User).subscribe(response => {
      this.ShowNotifications(response);
    })
  }
}
