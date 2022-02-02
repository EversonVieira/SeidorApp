import { Injectable } from '@angular/core';
import { CPF } from '../Models/cpf';
import { BaseResponse } from '../Nedesk/Core/Models/base-response';
import { ListResponse } from '../Nedesk/Core/Models/list-response';
import { HttpService } from '../Nedesk/Core/Services/http.service';

@Injectable({
  providedIn: 'root'
})
export class CPFService {

  private baseUrl:string = 'cpf/';
  constructor(private httpService: HttpService) { }

  insert(cpf: CPF) {
    return this.httpService.post<BaseResponse<number>>(this.baseUrl, cpf);
  }

  update(cpf: CPF) {
    return this.httpService.put<BaseResponse<number>>(this.baseUrl, cpf);
  }

  delete(cpf:CPF){
    return this.httpService.delete<BaseResponse<boolean>>(`${this.baseUrl}?cpfId=${cpf.id}`)
  }

  findByDocument(document: string) {
    return this.httpService.get<ListResponse<CPF>>(`${this.baseUrl}/findByDocument?document=${document}}`);
  }

  findByBlockStatus(isBlocked: boolean) {
    return this.httpService.get<ListResponse<CPF>>(`${this.baseUrl}/findByDocument?isBlocked=${isBlocked}}`);
  }
  findAll(){
    return this.httpService.get<ListResponse<CPF>>(`${this.baseUrl}`);
  }
}
