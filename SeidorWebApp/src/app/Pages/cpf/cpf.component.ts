import { Component, OnInit } from '@angular/core';
import { CPF } from 'src/app/Models/cpf';

@Component({
  selector: 'app-cpf',
  templateUrl: './cpf.component.html',
  styleUrls: ['./cpf.component.scss']
})
export class CpfComponent implements OnInit {

  cpf: CPF = new CPF();
  cpfList: CPF[] = [];
  constructor() { }

  ngOnInit(): void {
    for (let i = 0; i <= 5; i++) {
        const cpf: CPF = new CPF();
        cpf.id = i,
        cpf.ownerName = 'Everson de Souza Vieira',
        cpf.document = '112.123.123-33',
        cpf.isBlocked = i % 2 == 0,
        this.cpfList.push(cpf);
    } 
  }

}
