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
        readonly Model.LSContext dbCtx;
        readonly ILogger<LecturerController> logger;

        public LecturerController(Model.LSContext database, ILogger<LecturerController> log)
        {
            dbCtx = database;
            logger = log;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.Lecturer> Get()
        {
            return dbCtx.Lecturers.Select(l => new Data.Lecturer(l)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Data.Lecturer Get(string nameId)
        {
            Model.Lecturer lc = dbCtx.Lecturers.Find(nameId);
            if (lc == null)
                return null;
            return new Data.Lecturer(lc);
        }

        [HttpPost]
        //[Authorize]
        [Route("{pass}")]
        public IActionResult Post(string pass, [FromBody]Data.Lecturer lecturer)
        {
            // TODO: Check if you have MASTER role or smth
            if (String.IsNullOrEmpty(pass))
                return BadRequest();
            if (dbCtx.Lecturers.Find(lecturer.ID) == null)
            {
                dbCtx.Lecturers.Add(new Model.Lecturer(lecturer));
                dbCtx.Passwords.Add(new Model.Password{ ID = lecturer.ID, Pass = Data.Security.GetHash(pass) });
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Data.Lecturer lecturer)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturer.ID);
            if (lc == null)
                return NotFound();
            lc = new Model.Lecturer(lecturer);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }

        [HttpPut]
        //[Authorize]
        [Route("pass")]
        public IActionResult Put([FromBody]Data.Password password)
        {
            if (String.IsNullOrEmpty(password.Pass))
                return BadRequest();
            // TODO: Check if you have MASTER role or smth
            Model.Password pass = dbCtx.Passwords.Find(password.ID);
            if (pass == null)
                return NotFound();
            pass.Pass = Data.Security.GetHash(password.Pass);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }

        [HttpDelete]
        //[Authorize]
        [Route("{nameId}")]
        public IActionResult Delete(string nameId)
        {
            // TODO: Check if you have MASTER role or smth
            if (dbCtx.Lecturers.Find(nameId) == null)
                return NotFound();
            dbCtx.Lecturers.Remove(new Model.Lecturer{ ID = nameId });
            Model.Course cs = dbCtx.Courses.FirstOrDefault(c => c.LecturerID == nameId);
            if (cs != null)
                cs.LecturerID = null;
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}