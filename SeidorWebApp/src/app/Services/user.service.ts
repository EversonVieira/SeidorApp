import { Injectable } from '@angular/core';
import { User } from '../Models/user';
import { BaseResponse } from '../Nedesk/Core/Models/base-response';
import { HttpService } from '../Nedesk/Core/Services/http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }

  insert(user: User) {
    return this.httpService.post<BaseResponse<number>>('user', user);
  }
  update(user: User) {
    return this.httpService.put<BaseResponse<boolean>>('user', user);
  }
}
