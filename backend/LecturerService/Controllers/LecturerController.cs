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

        public LecturerController(Model.LSContext database, ILogger<LecturerController> logger)
        {
            dbCtx = database;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<Model.Lecturer> Get()
        {
            return dbCtx.Lecturers.ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Model.Lecturer Get(string nameId)
        {
            return dbCtx.Lecturers.Find(new Model.Lecturer{ ID = nameId });
        }

        [HttpPost]
        //[Authorize]
        [Route("{hash}")]
        public IActionResult Post(string hash, [FromBody]Model.Lecturer lecturer)
        {
            // TODO: Check if you have MASTER role or smth
            if (String.IsNullOrEmpty(hash))
                return BadRequest();
            if (dbCtx.Lecturers.Find(lecturer) == null)
            {
                dbCtx.Lecturers.Add(lecturer);
                dbCtx.Passwords.Add(new Model.Password{ ID = lecturer.ID, Hash = hash });
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Model.Lecturer lecturer)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturer);
            if (lc == null)
                return NotFound();
            lc = lecturer;
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }

        [HttpPut]
        //[Authorize]
        [Route("pass")]
        public IActionResult Put([FromBody]Data.Password password)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Password pass = dbCtx.Passwords.Find(password);
            if (pass == null)
                return NotFound();
            pass.Hash = password.Hash;
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
            Model.Lecturer lc = new Model.Lecturer{ ID = nameId };
            if (dbCtx.Lecturers.Find(lc) == null)
                return NotFound();
            dbCtx.Lecturers.Remove(lc);
            Model.Course cs = dbCtx.Courses.FirstOrDefault(c => c.LecturerID == nameId);
            if (cs != null)
                cs.LecturerID = null;
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}