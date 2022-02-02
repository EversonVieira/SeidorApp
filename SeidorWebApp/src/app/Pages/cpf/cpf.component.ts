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

  remove(cpf:CPF){
    this.cpfService.delete(cpf).subscribe(response => {
      this.ShowNotifications(response);
      if(response.isValid){
        this.findAll();
        this.modalService.dismissAll();
      }
    });
  }
}
