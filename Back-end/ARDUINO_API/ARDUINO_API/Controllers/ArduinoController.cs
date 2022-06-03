using Microsoft.AspNetCore.Mvc;
using System;
using ARDUINO_API.Repository;
using ARDUINO_API.Dao;

namespace ARDUINO_API.Controllers
{
    [ApiController]
    [Route("api/arduino/[controller]")]
    public class ArduinoController : ControllerBase
    {
        DaoArduino arduino = new DaoArduino();

        private readonly ArduinoRepository _arduinoRepository;

        public ArduinoController()
        {
            _arduinoRepository = new ArduinoRepository();
        }

        // Get
        [HttpGet]
        public ActionResult<IEnumerable<Arduino>> Get()
        {
            return _arduinoRepository.GetArduinos;
        }

        // Get
        [HttpGet, Route("getInfoArduino/{id}")]
        public Arduino GetInfoArduino(int id)
        {
            return arduino.GetInfoArduino(id);
        }

        // Post
        [HttpPost]
        public void Post([FromBody] ArduinoInsert arduinoInsert)
        {
            _arduinoRepository.InserirArduino(arduinoInsert);
        }

    }
}