import { Component, OnInit } from '@angular/core';
import { find } from 'rxjs';
import { CPF } from 'src/app/Models/cpf';
import { CPFService } from 'src/app/Services/cpf.service';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.scss']
})
export class LogComponent extends BaseSeidorComponent implements OnInit {

  public cpfList: CPF[] = [];
  public cpfCount:number = 0;


  constructor(private cpfService: CPFService) {
    super();
   }

  ngOnInit(): void {
    this.findAll();
  }

  findAll(){
    this.cpfService.findAll().subscribe(response => {
      if(response.hasResponseData){
        this.cpfList = <CPF[]> response.data;
      }
      if(!response.hasAnyMessages){
        this.findCountAll();  
      }
      this.ShowNotifications(response);
    });
    
  }

  findCountAll(){
    this.cpfService.findContAll().subscribe(response => {
      if(response.hasResponseData){
        this.cpfCount = <number> response.data;
      }
      this.ShowNotifications(response);
    });
  }
}
