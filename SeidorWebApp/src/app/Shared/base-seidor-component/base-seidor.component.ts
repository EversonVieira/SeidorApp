import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/Models/user';
import { BaseResponse, IBaseResponse } from 'src/app/Nedesk/Core/Models/base-response';
import { MessageTypeEnum } from 'src/app/Nedesk/Core/Models/message-type-enum';
import { LoginService } from 'src/app/Services/login.service';
import { AppInjector } from '../Injector/app-injector';
import { CurrentUser } from './current-user';

@Component({
  selector: 'app-base-seidor',
  templateUrl: './base-seidor.component.html',
  styleUrls: ['./base-seidor.component.scss']
})
export class BaseSeidorComponent {

  private static doingVerification:boolean = false;
  private currentUser: User;
  protected loginService: LoginService;
  protected toastrService: ToastrService;
  public hasUserLogged: boolean = false;
  public router: Router;
  constructor() {
    const injector = AppInjector.getInjector()

    this.toastrService = injector.get(ToastrService);
    this.loginService = injector.get(LoginService);
    this.router = injector.get(Router);
    this.currentUser = CurrentUser.getUser();
    this.verifyCurrentUser();
  }

  async verifyCurrentUser() {
    if(BaseSeidorComponent.doingVerification) return;
    
    BaseSeidorComponent.doingVerification = true;
    await this.validateUser();

    this.hasUserLogged = this.currentUser.id > 0;
    if (!this.hasUserLogged) {
      console.log(this.router.url);
      if (!this.router.url.includes('user/register') && !this.router.url.includes('home') && this.router.url != '/') {
        this.toastrService.warning("Faça login para acessar essa página");
        this.router.navigateByUrl("''");
      }
    }

    BaseSeidorComponent.doingVerification = false;

  }
  async validateUser() {
    if (localStorage.getItem("Session")) {
      if (this.currentUser.id <= 0) {
        try {
          let response = <BaseResponse<User>>await this.loginService.validateSession().toPromise()
          if (response.hasResponseData) {
            CurrentUser.setUser(<User>response.data);
          }
          else {
            localStorage.removeItem("Session");
            window.location.reload();
            CurrentUser.setUser(new User());
          }
        }
        catch {
          this.handleRequestError();
        }

      }
    }
    else {
      if (this.currentUser.id > 0) {
        CurrentUser.setUser(new User());
      }
    }
  }
  handleRequestError() {
    this.toastrService.error("Um erro inesperado ocorreu ao tentar realizar a requisição");
  }
  ShowNotifications(response: IBaseResponse) {
    response.messages.forEach(element => {

      switch (element.messageType) {
        case MessageTypeEnum.Information:
          this.toastrService.info(element.text);
          break;
        case MessageTypeEnum.Sucess:
          this.toastrService.success(element.text);
          break;
        case MessageTypeEnum.Caution:
        case MessageTypeEnum.Validation:
        case MessageTypeEnum.Warning:
          this.toastrService.warning(element.text);
          break;

        case MessageTypeEnum.Error:
        case MessageTypeEnum.Exception:
        case MessageTypeEnum.FatalErrorException:
          this.toastrService.error(element.text);
          break;

        default:
          this.toastrService.show(element.text);
      }
    });
  }
}
