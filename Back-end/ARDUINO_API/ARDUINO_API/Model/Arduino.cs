namespace ARDUINO_API
{
    public class Arduino
    {
        public int? ard_id { get; set; }
        public String? ard_sensorUm { get; set; }
        public String? ard_sensorDois { get; set; }
        public String? ard_fluxo { get; set; } // Numero
        public String? ard_volumeTotal { get; set; } // Numero
        public String? ard_endereco { get; set; }
        public String? ard_capacidade { get; set; }
        public String? ard_src { get; set; }
        public DateTime? ard_date { get; set; }
    }
}