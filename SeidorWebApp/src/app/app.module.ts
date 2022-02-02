import { Injector, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Layout/header/header.component';
import { SideMenuComponent } from './Layout/side-menu/side-menu.component';
import { LayoutComponent } from './Layout/layout.component';
import { HomeComponent } from './Pages/home/home.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseSeidorComponent } from './Shared/base-seidor-component/base-seidor.component';
import { AppInjector } from './Shared/Injector/app-injector';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { NgxUiLoaderHttpModule, NgxUiLoaderModule, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { UserRegisterComponent } from './Pages/user-register/user-register.component';
import { CpfComponent } from './Pages/cpf/cpf.component';
import { UserUpdateComponent } from './Pages/user-update/user-update.component';
import { LogComponent } from './Pages/log/log.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SideMenuComponent,
    LayoutComponent,
    HomeComponent,
    BaseSeidorComponent,
    UserRegisterComponent,
    CpfComponent,
    UserUpdateComponent,
    LogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    AppRoutingModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    NgxUiLoaderModule,
    NgxUiLoaderRouterModule,
    NgxUiLoaderHttpModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(injector: Injector) {
    AppInjector.setInjector(injector);
  }
}