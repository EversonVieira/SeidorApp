import { Component, OnInit } from '@angular/core';
import { CPF } from 'src/app/Models/cpf';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CPFService } from 'src/app/Services/cpf.service';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';
import { basename } from 'path';
@Component({
  selector: 'app-cpf',
  templateUrl: './cpf.component.html',
  styleUrls: ['./cpf.component.scss']
})
export class CpfComponent extends BaseSeidorComponent implements OnInit {

  public cpf: CPF = new CPF();
  public cpfList: CPF[] = [];
  public cpfCount:number = 0;

  constructor(private modalService: NgbModal, private cpfService: CPFService) {
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
  createModal(content: any) {
    this.cpf = new CPF();
    this.modalService.open(content);
  }

  editModal(cpf: CPF, content: any) {
    Object.assign(this.cpf, cpf);
    this.modalService.open(content);
  }
  closeModal() {
    this.modalService.dismissAll();
  }
  save() {
    if (this.cpf.id <= 0) {
      this.cpfService.insert(this.cpf).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.findAll();
          this.modalService.dismissAll();
        }
      });
    }
    else {
      this.cpf.modifiedOn = new Date(new Date().toUTCString());
      this.cpfService.update(this.cpf).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.findAll();
          this.modalService.dismissAll();
        }
      });
    }
  }

  openRemoveModal(cpf:CPF, content:any){
    this.cpf = Object.assign(cpf);
    this.modalService.open(content);
  }
  removeCpf(){
    this.cpfService.delete(this.cpf).subscribe(response => {
      this.ShowNotifications(response);
      if(response.isValid){
        this.findAll();
        this.modalService.dismissAll();
      }
    });
  }
}
