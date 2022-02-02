import { Component, OnInit } from '@angular/core';
import { BaseSeidorComponent } from '../Shared/base-seidor-component/base-seidor.component';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent extends BaseSeidorComponent implements OnInit {

  constructor() {
    super();
    setInterval(() => {
      this.verifyCurrentUser()
    }, 1000);
   }

  ngOnInit(): void {
  }

}
