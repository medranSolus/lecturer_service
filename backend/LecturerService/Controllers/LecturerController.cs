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

#region GET
        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.Lecturer> Get()
        {
            return dbCtx.Lecturers.Select(l => new Data.Lecturer(l)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{lecturerId}")]
        public Data.Lecturer Get(string lecturerId)
        {
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturerId);
            if (lc == null)
                return null;
            return new Data.Lecturer(lc);
        }

        [HttpGet]
        //[Authorize]
        [Route("notify")]
        public IEnumerable<Data.CourseMsgInfo> GetNotifications()
        {
            if (Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return dbCtx.CoursesToCheck.Select(msg => new Data.CourseMsgInfo(msg)).ToArray();
            return null;
        }

        [HttpGet]
        //[Authorize]
        [Route("notify/{lecturerId}")]
        public IEnumerable<Data.GroupMsgInfo> GetNotifications(string lecturerId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc != null && lc.ID == lecturerId)
                return dbCtx.GroupNotification.Where(msg => msg.LecturerID == lecturerId).Select(msg => new Data.GroupMsgInfo(msg)).ToArray();
            return null;
        }
#endregion // GET

        [HttpPost]
        [Authorize]
        [Route("{pass}")]
        public IActionResult Post(string pass, [FromBody]Data.Lecturer lecturer)
        {
            if (!Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return Unauthorized();
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

#region PUT
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Lecturer lecturer)
        {
            Model.Lecturer caller = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (caller == null || caller.ID != lecturer.ID)
                return Unauthorized();
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturer.ID);
            if (lc == null)
                return NotFound();
            lc = new Model.Lecturer(lecturer);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("pass")]
        public IActionResult Put([FromBody]Data.Password password)
        {
            Model.Lecturer caller = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (caller == null || caller.ID != password.ID)
                return Unauthorized();
            if (String.IsNullOrEmpty(password.Pass))
                return BadRequest();
            Model.Password pass = dbCtx.Passwords.Find(password.ID);
            if (pass == null)
                return NotFound();
            pass.Pass = Data.Security.GetHash(password.Pass);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }
#endregion // PUT

        [HttpDelete]
        [Authorize]
        [Route("{lecturerId}")]
        public IActionResult Delete(string lecturerId)
        {
            if (!Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return Unauthorized();
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturerId);
            if (lc == null)
                return NotFound();
            dbCtx.Lecturers.Remove(lc);
            foreach (var cs in dbCtx.Courses.Where(c => c.LecturerID == lecturerId))
                cs.LecturerID = null;
            foreach (var cs in dbCtx.PendingCourses.Where(c => c.LecturerID == lecturerId))
                cs.LecturerID = null;
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}