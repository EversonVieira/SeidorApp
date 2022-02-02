import { Component, OnInit } from '@angular/core';
import { DTO_RegisterUser } from 'src/app/Models/DTO_RegisterUser';
import { User } from 'src/app/Models/user';
import { UserService } from 'src/app/Services/user.service';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent extends BaseSeidorComponent implements OnInit {

  public User:DTO_RegisterUser = new DTO_RegisterUser(); 


  constructor(private userService:UserService) {
    super();
  }

  ngOnInit(): void {
  }

  save(){
    if(this.User.password != this.User.confirmPassword){
      this.toastrService.warning("Senhas não são iguais!");
      return;
    }

    this.userService.insert(this.User).subscribe(response => {
      this.ShowNotifications(response);
      if(<number> response.data > 0){
        this.router.navigateByUrl("''");
      }
    })
  }
}
