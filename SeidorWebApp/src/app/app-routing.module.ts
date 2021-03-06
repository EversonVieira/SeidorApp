import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CpfComponent } from './Pages/cpf/cpf.component';
import { HomeComponent } from './Pages/home/home.component';
import { LogComponent } from './Pages/logPage/log.component';
import { UserRegisterComponent } from './Pages/user-register/user-register.component';
import { UserUpdateComponent } from './Pages/user-update/user-update.component';

const routes: Routes = [
  {path:"home", component: HomeComponent, pathMatch: 'full' },
  {path:'user/register', component: UserRegisterComponent},
  {path:'user/update', component: UserUpdateComponent},
  {path:"data", component: CpfComponent},
  {path:"log", component: LogComponent},
  {path:"**", redirectTo: 'home', pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
