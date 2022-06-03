import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ArduinoService } from 'src/app/services/arduino.service';

@Component({
  selector: 'app-teste',
  templateUrl: './teste.component.html',
  styleUrls: ['./teste.component.css'],
})
export class TesteComponent implements OnInit {

  lstArduinoApi: any[] = [];
  lstArduino: any[] = [];
  src: string = '';
  circleColor: string = '';
  // tempo: boolean = false;

  filterEnderecos = new FormControl('');

  constructor(private arduinoService: ArduinoService) { }

  ngOnInit(): void {
    this.getArduino();
  }

  clickNumber(src: string, situacao: string) {
    this.src = src;

    switch(situacao) {
      case 'Bueiro com menos de 33% da capacidade':
        this.circleColor = 'success'
        break
      case 'Bueiro entre 34 a 65% da capacidade':
        this.circleColor = 'warning'
        break
      case 'Bueiro com mais de 65% da capacidade':
        this.circleColor = 'danger'
        break
    }
    // this.testeLoop();
  }

  // testeLoop() {
  //   this.tempo = !this.tempo;
  //   while(this.tempo) {
  //     setInterval(function() {
  //       console.log("Passou aqui")
  //     },2000
  //   )}
  // }

  refreshLstArduino() {
    let listaAux = this.lstArduinoApi;
    if(this.filterEnderecos.value) {
      listaAux = this.lstArduinoApi.filter(obj => {
        const term = this.filterEnderecos.value.toLowerCase();
        return (obj.ard_endereco != undefined ? obj.ard_endereco.toString().toLowerCase().includes(term) : false);
      });
    }
    this.lstArduino = listaAux
  }

  getArduino() {
    this.arduinoService.getArduino().subscribe((lstArduino: any[]) => {
      this.lstArduinoApi = lstArduino;
      this.refreshLstArduino();
      console.log(this.lstArduinoApi)
    });
  }

  getArduinoById(id: number) {
    this.arduinoService.getArduinoById(id).subscribe((response) => {
      this.lstArduinoApi.forEach((element) => {
        this.getArduino();
      });
    });
  }

}
