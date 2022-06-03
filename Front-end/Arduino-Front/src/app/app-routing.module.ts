import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InfoBueiroComponent } from './pages/info-bueiro/info-bueiro.component';
import { InicialComponent } from './pages/inicial/inicial.component';
import { TesteComponent } from './pages/teste/teste.component';

const routes: Routes = [
  { path: '', redirectTo: 'inicial', pathMatch: 'full' },
  { path: "inicial", component: InicialComponent },
  { path: "teste", component: TesteComponent },
  { path: "teste/info-bueiro/:ard_id", component: InfoBueiroComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
