import { Component, OnInit } from '@angular/core';
import { CPF } from 'src/app/Models/cpf';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CPFService } from 'src/app/Services/cpf.service';
import { BaseSeidorComponent } from 'src/app/Shared/base-seidor-component/base-seidor.component';
import { basename } from 'path';

export enum CpfFilterEnum{
  Nenhum = 0,
  CPF = 1,
  Status = 2
}

@Component({
  selector: 'app-Cpf',
  templateUrl: './Cpf.component.html',
  styleUrls: ['./Cpf.component.scss']
})


export class CpfComponent extends BaseSeidorComponent implements OnInit {

  public Cpf: CPF = new CPF();
  public CpfList: CPF[] = [];
  public CpfCount:number = 0;
  public selectedFilter:CpfFilterEnum = 0;

  public selectedStatus:boolean = false;
  public documentToFind:string = '';
  

  constructor(private modalService: NgbModal, private CpfService: CPFService) {
    super();
  }

  ngOnInit(): void {
    this.findAll();
  }

  

  findAll(){
    this.CpfService.findAll().subscribe(response => {
      if(response.hasResponseData){
        this.CpfList = <CPF[]> response.data;
      }
      if(!response.hasAnyMessages){
        this.findCountAll();  
      }
      this.ShowNotifications(response);
    });
    
  }

  findCountAll(){
    this.CpfService.findContAll().subscribe(response => {
      if(response.hasResponseData){
        this.CpfCount = <number> response.data;
      }
      this.ShowNotifications(response);
    });
  }
  createModal(content: any) {
    this.Cpf = new CPF();
    this.modalService.open(content);
  }

  editModal(Cpf: CPF, content: any) {
    Object.assign(this.Cpf, Cpf);
    this.modalService.open(content);
  }

  closeModal() {
    this.modalService.dismissAll();
  }

  save() {
    if (this.Cpf.id <= 0) {
      this.Cpf.createdBy = this.currentUserName;
      this.Cpf.modifiedBy = this.currentUserName;
      this.CpfService.insert(this.Cpf).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.findAll();
          this.modalService.dismissAll();
        }
      });
    }
    else {
      this.Cpf.modifiedBy = this.currentUserName;
      this.Cpf.modifiedOn = new Date(new Date().toUTCString());
      this.CpfService.update(this.Cpf).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.findAll();
          this.modalService.dismissAll();
        }
      });
    }
  }

  openRemoveModal(Cpf:CPF, content:any){
    this.Cpf = Object.assign(Cpf);
    this.modalService.open(content);
  }

  removeCpf(){
    this.CpfService.delete(this.Cpf).subscribe(response => {
      this.ShowNotifications(response);
      if(response.isValid){
        this.findAll();
        this.modalService.dismissAll();
      }
    });
  }

  findByFilter(){
    if(this.selectedFilter == CpfFilterEnum.CPF){
      this.CpfService.findByDocument(this.documentToFind).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.CpfList = <CPF[]>response.data;
          this.CpfCount = this.CpfList.length;
        }
      });
    }
    else if(this.selectedFilter == CpfFilterEnum.Status){
      this.CpfService.findByBlockStatus(this.selectedStatus).subscribe(response => {
        this.ShowNotifications(response);
        if(response.isValid){
          this.CpfList = <CPF[]>response.data;
          this.CpfCount = this.CpfList.length;
        }
      });
    }

  }

  clearFilter(){
    this.findAll();
    this.selectedFilter = CpfFilterEnum.Nenhum;
    this.documentToFind = '';
    this.selectedStatus = false;
  }
}
