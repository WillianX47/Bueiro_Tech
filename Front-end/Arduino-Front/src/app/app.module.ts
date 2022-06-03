import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { InicialComponent } from './pages/inicial/inicial.component';
import { TesteComponent } from './pages/teste/teste.component';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { InfoBueiroComponent } from './pages/info-bueiro/info-bueiro.component';
import { SafePipeService } from './services/safe-pipe.service';

@NgModule({
  declarations: [
    AppComponent,
    InicialComponent,
    TesteComponent,
    InfoBueiroComponent,
    SafePipeService
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
