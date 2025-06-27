using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class LapTimeController : Controller
    {
        private readonly ILogger<LapTimeController> _logger;
        private readonly IDatabaseService _service;

        public LapTimeController(IDatabaseService service, ILogger<LapTimeController> logger)
        {
            _logger = logger;
            _service = service;
        }


        /// <summary>
        /// Retrieve all lap times and send it to the frontend
        /// </summary>
        /// <returns>
        /// Returns 200 OK with a list of lapTime on success,
        /// or 500 Internal Server Error if an exception occurs.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLapTimes()
        {
            try
            {
                LapTime[] lapTimes = await _service.FetchContent();
                LapTime? lapTime = lapTimes[0];
                return Ok(lapTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching lap times.");
                return StatusCode(500, "An error occurred while fetching Lap Times.");
            }
        }
    }
}