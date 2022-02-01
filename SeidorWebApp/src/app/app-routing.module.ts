import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CpfComponent } from './Pages/cpf/cpf.component';
import { HomeComponent } from './Pages/home/home.component';
import { UserRegisterComponent } from './Pages/user-register/user-register.component';

const routes: Routes = [
  {path:"home", component: HomeComponent, pathMatch: 'full' },
  {path:'user/register', component: UserRegisterComponent},
  {path:"data", component: CpfComponent},
  {path:"**", redirectTo: 'home', pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
