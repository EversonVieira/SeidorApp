import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl: string = "";

  constructor(private httpClient: HttpClient) {
    this.baseUrl = "localhost:";
  }

  getHeaders(){
      let headers:HttpHeaders = new HttpHeaders();

      headers = headers.set('Access-Control-Allow-Origin', environment.production ? 'http://munisaude.eastus.cloudapp.azure.com':'*');
      headers = headers.set('Access-Control-Allow-Methods', 'GET,POST,OPTIONS,DELETE,PUT');
      headers = headers.set("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, x-client-key, x-client-token, x-client-secret, Authorization");
      headers = headers.set('Content-Type', 'application/json');
      headers = headers.set('Session', localStorage.getItem('Session') || '')
      return headers;
  }
  post<T>(url: string, data: any) {
    return this.httpClient.post<T>(this.baseUrl + url, data, {
        headers: this.getHeaders()
    });
  }
  get<T>(url: string) {
    return this.httpClient.get<T>(this.baseUrl + url, {
        headers: this.getHeaders()
    });
  };
  patch<T>(url: string, data: any) {
    return this.httpClient.patch<T>(this.baseUrl + url, data,  {
        headers: this.getHeaders()
    });
  }
  put<T>(url: string, data: any) {
    return this.httpClient.put<T>(this.baseUrl + url, data,  {
        headers: this.getHeaders()
    });
  }
  delete<T>(url: string) {
    return this.httpClient.delete<T>(`${this.baseUrl}${url}`, {
        headers: this.getHeaders()
    });
  }

}

