import { Injectable } from '@angular/core';
import { DTO_RegisterUser } from '../Models/DTO_RegisterUser';
import { User } from '../Models/user';
import { BaseResponse } from '../Nedesk/Core/Models/base-response';
import { HttpService } from '../Shared/http.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private baseUrl:string = 'login/';
  constructor(private httpService: HttpService) { }

  doLogin(user: User) {
    return this.httpService.post<BaseResponse<string>>(`${this.baseUrl}login`, user);
  }

  doLogout() {
    return this.httpService.get<BaseResponse<boolean>>(`${this.baseUrl}logout`);
  }

  validateSession() {
    return this.httpService.get<BaseResponse<User>>(`${this.baseUrl}validateSession`);
  }

}
