#define Sensor2 11
#define Sensor3 10

// sensor de nivel
short sensor2 = 1;
short sensor3 = 1;
int nivelinicial = 0;

// sensor de fluxo
const int interrupcao_sensor = 0; 
const int sensor = 2;
float fluxo = 0;
float volume = 0;
float volume_total = 0;

//definicao da variavel de contagem de voltas
unsigned long contador = 0;

//definicao do fator de calibracao para conversao do valor lido
const float fator_calibracao = 4.5;

//definicao da variavel de intervalo de tempo
unsigned long tempo_antes = 0;


void setup() {

  Serial.begin(9600);

  //pinMode(4, OUTPUT);
  //pinMode(7, OUTPUT);
  //pinMode(8, OUTPUT);

  pinMode(sensor, INPUT_PULLUP);

  pinMode(Sensor2, INPUT);
  pinMode(Sensor3, INPUT);
}

void loop() {
  
  //executa a contagem de pulsos uma vez por segundo
  if ((millis() - tempo_antes) > 1000) {

    //desabilita a interrupcao para realizar a conversao do valor de pulsos
    detachInterrupt(interrupcao_sensor);

    //conversao do valor de pulsos para L/min
    fluxo = ((1000.0 / (millis() - tempo_antes)) * contador) / fator_calibracao;

    //calculo do volume em L passado pelo sensor
    volume = fluxo / 60;

    volume_total += volume;

    contador = 0;

    //atualizacao da variavel tempo_antes
    tempo_antes = millis();

    //contagem de pulsos do sensor
    attachInterrupt(interrupcao_sensor, contador_pulso, FALLING);

  }

  sensor2 = digitalRead(Sensor2);
  sensor3 = digitalRead(Sensor3);
  
  //if ((volume_total >= 0.5)){
  if ((sensor2 == 0) && (sensor3 == 0)) {
    Serial.print("Bueiro com mais de 65% da capacidade|");
    //digitalWrite(4, LOW);
    //digitalWrite(7, LOW);
    //digitalWrite(8, HIGH);
    Serial.print(sensor2);
    Serial.print("|");
    Serial.print(sensor3);
    Serial.print("|");
    // Serial.print("Fluxo de: ");
    Serial.print(fluxo);
    Serial.print("|");
    // Serial.print("Volume: ");
    Serial.print(volume_total);
    Serial.print("|");
    Serial.println();
  }
  //}

  //if ((volume_total >= 0.2 && volume_total < 0.5)){
  if ((sensor2 == 1) && (sensor3 == 0)) {
    Serial.print("Bueiro entre 34 a 65% da capacidade|");
    //digitalWrite(4, LOW);
    //digitalWrite(7, HIGH);
    //digitalWrite(8, LOW);
    Serial.print(sensor2);
    Serial.print("|");
    Serial.print(sensor3);
    Serial.print("|");
    // Serial.print("Fluxo de: ");
    Serial.print(fluxo);
    Serial.print("|");
    // Serial.print("Volume: ");
    Serial.print(volume_total);
    Serial.print("|");
    Serial.println();
  }
  //}

  if ((sensor2 == 1) && (sensor3 == 1)) {
    Serial.print("Bueiro com menos de 33% da capacidade|");
    //digitalWrite(4, HIGH);
    //digitalWrite(7, LOW);
    //digitalWrite(8, LOW);
    Serial.print(sensor2);
    Serial.print("|");
    Serial.print(sensor3);
    Serial.print("|");
    //Serial.print("Fluxo de: ");
    Serial.print(fluxo);
    Serial.print("|");
    // Serial.print("Volume: ");
    Serial.print(volume_total);
    Serial.print("|");
    Serial.println();
  }


  delay(5000);

}

//funcao chamada pela interrupcao para contagem de pulsos
void contador_pulso() {

  contador++;

}
