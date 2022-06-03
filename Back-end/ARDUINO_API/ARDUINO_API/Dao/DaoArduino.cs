using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ARDUINO_API.Dao
{
    public class DaoArduino
    {
        string conexao = @"Data Source=DESKTOP-5UQ61KL\SQLEXPRESS;Initial Catalog=ARDUINO_API;Integrated Security=True";

        public List<Arduino> GetArduinos()
        {
            List<Arduino> arduinos = new List<Arduino>();

            using (SqlConnection comm = new SqlConnection(conexao))
            {
                comm.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from Arduino", comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader != null)
                        {
                            while(reader.Read())
                            {
                                var arduino = new Arduino();
                                arduino.ard_id = (int)reader[0];
                                arduino.ard_sensorUm = reader[1].ToString();
                                arduino.ard_sensorDois = reader[2].ToString();
                                arduino.ard_fluxo = reader[3].ToString();
                                arduino.ard_volumeTotal = reader[4].ToString();
                                arduino.ard_endereco = reader[5].ToString();
                                arduino.ard_capacidade = reader[6].ToString();
                                arduino.ard_date = (DateTime)reader[7];
                                arduino.ard_src = reader[8].ToString();
                                arduinos.Add(arduino);
                            }
                        }
                    }
                }
            }
            return arduinos;
        }

        public Arduino GetInfoArduino(int id)
        {
            // Atualizar dados do Arduino
            atualizarArduino(id);
            var arduino = new Arduino();
            using (SqlConnection comm = new SqlConnection(conexao))
            {
                comm.Open();
                // Após atualização, select no id
                using (SqlCommand cmd = new SqlCommand("Select * from Arduino where ard_id = " + id, comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                arduino.ard_id = (int)reader[0];
                                arduino.ard_sensorUm = reader[1].ToString();
                                arduino.ard_sensorDois = reader[2].ToString();
                                arduino.ard_fluxo = reader[3].ToString();
                                arduino.ard_volumeTotal = reader[4].ToString();
                                arduino.ard_endereco = reader[5].ToString();
                                arduino.ard_capacidade = reader[6].ToString();
                                arduino.ard_date = (DateTime)reader[7];
                            }
                        }
                    }
                }

                // Aciona a procedure atualizando a tabela de log
                using (SqlCommand cmd = new SqlCommand("" +
                    " EXEC UpdateLog " +
                    " @ard_log_sensorUm = '" + arduino.ard_sensorUm + "', " +
                    " @ard_log_sensorDois = '" + arduino.ard_sensorDois + "', " +
                    " @ard_log_fluxo = '" + arduino.ard_fluxo + "', " +
                    " @ard_log_volumeTotal = '" + arduino.ard_volumeTotal + "', " +
                    " @ard_log_endereco = '" + arduino.ard_endereco + "', " +
                    " @ard_log_capacidade = '" + arduino.ard_capacidade + "', " +
                    " @ard_log_date = '" + arduino.ard_date + "', " +
                    " @ard_log_id_arduino = " + arduino.ard_id + ";", comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

            }
            return arduino;
        }

        public void atualizarArduino(int id)
        {
            String[] strings = GetArduino();

            using (SqlConnection comm = new SqlConnection(conexao))
            {
                comm.Open();
                using (SqlCommand cmd = new SqlCommand(" " +
                                    " update Arduino set " +
                                    " ard_sensorUm = '" + strings[1] + "', " +
                                    " ard_sensorDois = '" + strings[2] + "', " +
                                    " ard_fluxo = '" + strings[3] + "', " +
                                    " ard_volumeTotal = '" + strings[4] + "', " +
                                    " ard_capacidade = '" + strings[0] + "'," +
                                    " ard_date = '" + DateTime.Now +
                                    "' where ard_id = " + id, comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InserirArduino(ArduinoInsert arduinoInsert)
        {
            
            String[] strings = GetArduino();
            Arduino arduino = new Arduino();

            using (SqlConnection comm = new SqlConnection(conexao))
            {
                // Insert na tabela arduino
                comm.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Arduino (ard_sensorUm, ard_sensorDois, ard_fluxo, ard_volumeTotal, ard_endereco, ard_capacidade, ard_date, ard_src) " +
                    "VALUES (@SENSORUM,@SENSORDOIS,@FLUXO,@VOLUMETOTAL,@ENDERECO, @CAPACIDADE, '" + DateTime.Now + "', '" + arduinoInsert.ard_src + "')", comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@SENSORUM", strings[1]);
                    cmd.Parameters.AddWithValue("@SENSORDOIS", strings[2]);
                    cmd.Parameters.AddWithValue("@FLUXO", strings[3]);
                    cmd.Parameters.AddWithValue("@VOLUMETOTAL", strings[4]);
                    cmd.Parameters.AddWithValue("@ENDERECO", arduinoInsert.ard_endereco);
                    cmd.Parameters.AddWithValue("@CAPACIDADE", strings[0]);
                    cmd.ExecuteNonQuery();
                }

                // Select na tabela para pegar o último dado inserido
                using (SqlCommand cmd = new SqlCommand("select top 1 * from Arduino order by ard_id desc", comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                arduino.ard_id = (int)reader[0];
                                arduino.ard_sensorUm = reader[1].ToString();
                                arduino.ard_sensorDois = reader[2].ToString();
                                arduino.ard_fluxo = reader[3].ToString();
                                arduino.ard_volumeTotal = reader[4].ToString();
                                arduino.ard_endereco = reader[5].ToString();
                                arduino.ard_capacidade = reader[6].ToString();
                                arduino.ard_date = (DateTime)reader[7];
                            }
                        }
                    }
                }

                // Aciona a procedure atualizando a tabela de log
                using (SqlCommand cmd = new SqlCommand("" +
                    " EXEC UpdateLog " +
                    " @ard_log_sensorUm = '" + arduino.ard_sensorUm + "', " +
                    " @ard_log_sensorDois = '" + arduino.ard_sensorDois + "', " +
                    " @ard_log_fluxo = '" + arduino.ard_fluxo + "', " +
                    " @ard_log_volumeTotal = '" + arduino.ard_volumeTotal + "', " +
                    " @ard_log_endereco = '" + arduino.ard_endereco + "', " +
                    " @ard_log_capacidade = '" + arduino.ard_capacidade + "', " +
                    " @ard_log_date = '" + arduino.ard_date + "', " +
                    " @ard_log_id_arduino = " + arduino.ard_id + ";", comm))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public String[] GetArduino()
        {
            SerialPort myport = new SerialPort();
            myport.BaudRate = (9600);
            myport.PortName = "COM3";
            myport.Open();
            string data_rx = myport.ReadLine();
            myport.Close();

            String[] strings = data_rx.Split("|");

            // Pegando separado
            string capacidade = strings[0];
            string sensorUm = strings[1];
            string sensorDois = strings[2];
            string fluxo = strings[3];
            string volumeTotal = strings[4];

            Console.WriteLine("\nAqui a capacidade: " + capacidade);
            Console.WriteLine("Aqui o fluxo: " + fluxo);
            Console.WriteLine("Aqui o volume Total: " + volumeTotal);
            Console.WriteLine("Aqui o Sensor um: " + sensorUm);
            Console.WriteLine("Aqui o Sensor dois: " + sensorDois);
            return strings;

        }
    }
}
