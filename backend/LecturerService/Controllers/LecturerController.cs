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
        readonly Data.LSContext dbCtx;
        readonly ILogger<LecturerController> logger;

        public LecturerController(Data.LSContext database, ILogger<LecturerController> logger)
        {
            dbCtx = database;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.Lecturer> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new Data.Lecturer
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
        [Route("{nameId}")]
        public Data.Lecturer Get(string nameId)
        {
            return dbCtx.Lecturers.FirstOrDefault(lc => lc.ID == nameId);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody]Data.Lecturer lecturer)
        {
            return Ok();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Data.Lecturer lecturer)
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