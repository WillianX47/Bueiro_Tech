using ARDUINO_API.Dao;

namespace ARDUINO_API.Repository
{
    public class ArduinoRepository
    {
        private readonly DaoArduino _daoArduino;

        public ArduinoRepository()
        {
            _daoArduino = new DaoArduino();
        }

        public List<Arduino> GetArduinos 
        {
            get
            {
                return _daoArduino.GetArduinos();
            }
        }

        public void InserirArduino(ArduinoInsert arduinoInsert)
        {
            _daoArduino.InserirArduino(arduinoInsert);
        }
    }
}
