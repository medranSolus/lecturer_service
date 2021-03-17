using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LecturerController : ControllerBase
    {
        readonly ILogger<LecturerController> logger;

        public LecturerController(ILogger<LecturerController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<Models.Lecturer> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 1).Select(index => new Models.Lecturer
            {
                ID = "TEST",
                Name = "DateTime.Now.AddDays(index)",
                Surname = "rng.Next(-20, 55)",
                Password = "Summaries[rng.Next(Summaries.Length)]",
                Mail = "dotsrom",
                Phone = "42069"
            })
            .ToArray();
        }

        [HttpGet]
        //[Authorize]
        public Models.Lecturer Get(string nameId)
        {
            return new Models.Lecturer
            {
                ID = nameId,
                Name = "DateTime.Now.AddDays(index)",
                Surname = "rng.Next(-20, 55)",
                Password = "Summaries[rng.Next(Summaries.Length)]",
                Mail = "dotsrom",
                Phone = "42069"
            };
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody]Models.Lecturer lecturer)
        {
            return Ok();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Models.Lecturer lecturer)
        {
            return Ok();
        }

        [HttpDelete]
        //[Authorize]
        public IActionResult Delete(string nameId)
        {
            return Ok();
        }
    }
}