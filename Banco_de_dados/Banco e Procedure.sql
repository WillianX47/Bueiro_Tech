CREATE DATABASE ARDUINO_API

USE ARDUINO_API

CREATE TABLE Arduino
(
	ard_id int not null identity(1,1),
	ard_sensorUm varchar(255),
	ard_sensorDois varchar(255),
	ard_fluxo varchar(255),
	ard_volumeTotal varchar(255),
	ard_endereco varchar(255),
	ard_capacidade varchar(255),
	ard_date datetime,
)

CREATE TABLE ArduinoLog
(
	ard_log_id int not null identity(1,1),
	ard_log_sensorUm varchar(255),
	ard_log_sensorDois varchar(255),
	ard_log_fluxo varchar(255),
	ard_log_volumeTotal varchar(255),
	ard_log_endereco varchar(255),
	ard_log_capacidade varchar(255),
	ard_log_date datetime,
	ard_log_id_arduino int
)

select top 1 * from Arduino order by ard_id desc

SELECT * FROM ArduinoLog
where ard_log_id_arduino = 4

select 
ard_id,
ard_sensorUm,
ard_sensorDois,
ard_fluxo,
ard_volumeTotal,
ard_endereco,
ard_capacidade,
FORMAT (ard_date, 'dd/MM/yyyy hh:mm')
from Arduino;

/* Procedure

CREATE PROCEDURE UpdateLog 
@ard_log_sensorUm varchar(255),
@ard_log_sensorDois varchar(255),
@ard_log_fluxo varchar(255),
@ard_log_volumeTotal varchar(255),
@ard_log_endereco varchar(255),
@ard_log_capacidade varchar(255),
@ard_log_date datetime,
@ard_log_id_arduino int

AS

INSERT INTO ArduinoLog
(ard_log_sensorUm,
ard_log_sensorDois,
ard_log_fluxo,
ard_log_volumeTotal,
ard_log_endereco,
ard_log_capacidade,
ard_log_date,
ard_log_id_arduino)
VALUES (@ard_log_sensorUm,
@ard_log_sensorDois,
@ard_log_fluxo,
@ard_log_volumeTotal,
@ard_log_endereco,
@ard_log_capacidade,
@ard_log_date,
@ard_log_id_arduino)


*/

/* Exec Procedure

EXEC UpdateLog
@ard_log_sensorUm = '3',
@ard_log_sensorDois = '3',
@ard_log_fluxo = '',
@ard_log_volumeTotal = '',
@ard_log_endereco = '',
@ard_log_capacidade = '',
@ard_log_date = '',
@ard_log_id_arduino = 2;

*/
