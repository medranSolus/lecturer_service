using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LecturerController : ControllerBase
    {
        readonly Model.LSContext dbCtx;

        public LecturerController(Model.LSContext database)
        {
            dbCtx = database;
        }

#region GET
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(dbCtx.Lecturers.Select(l => new Data.Lecturer(l)).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{lecturerId}")]
        public IActionResult Get(string lecturerId)
        {
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturerId);
            if (lc == null)
                return NotFound();
            return Ok(new Data.Lecturer(lc));
        }

        [HttpGet]
        [Authorize]
        [Route("notify")]
        public IActionResult GetNotifications()
        {
            if (Data.Security.IsAdmin(HttpContext, dbCtx))
                return Ok(dbCtx.CoursesToCheck.Include(msg => msg.Course).Select(msg => new Data.CourseMsgInfo(msg)).ToArray());
            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("notify/{lecturerId}")]
        public IActionResult GetNotifications(string lecturerId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc != null && lc.ID == lecturerId)
                return Ok(dbCtx.GroupNotification.Include(msg => msg.Group).Include(msg => msg.Group.Course)
                    .Where(msg => msg.Group.Course.LecturerID == lecturerId).Select(msg => new Data.GroupMsgInfo(msg)).ToArray());
            return Unauthorized();
        }
#endregion // GET

        [HttpPost]
        [Authorize]
        [Route("{password}")]
        public IActionResult Post(string password, [FromBody]Data.Lecturer lecturer)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
                return Unauthorized();
            if (string.IsNullOrEmpty(password))
                return BadRequest();
            if (dbCtx.Lecturers.Find(lecturer.ID) == null)
            {
                dbCtx.Lecturers.Add(new Model.Lecturer(lecturer));
                dbCtx.Passwords.Add(new Model.Password{ ID = lecturer.ID, Pass = Data.Security.GetHash(password) });
                dbCtx.SaveChanges();
                return Ok();
            }
            return Conflict();
        }

#region PUT
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Lecturer lecturer)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null || lc.ID != lecturer.ID)
                Unauthorized();
            if (lc.RoleTypeID != Data.Role.Admin)
                lecturer.RoleTypeID = lc.RoleTypeID;
            lc.Update(lecturer);
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("pass")]
        public IActionResult Put([FromBody]Data.Password password)
        {
            Model.Lecturer caller = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (caller == null || caller.ID != password.ID)
                return Unauthorized();
            if (string.IsNullOrEmpty(password.Pass))
                return BadRequest();
            Model.Password pass = dbCtx.Passwords.Find(password.ID);
            pass.Pass = Data.Security.GetHash(password.Pass);
            dbCtx.SaveChanges();
            return Ok();
        }
#endregion // PUT

        [HttpDelete]
        [Authorize]
        [Route("{lecturerId}")]
        public IActionResult Delete(string lecturerId)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
                return Unauthorized();
            Model.Lecturer lc = dbCtx.Lecturers.Find(lecturerId);
            if (lc == null)
                return NotFound();
            dbCtx.Lecturers.Remove(lc);
            foreach (var cs in dbCtx.Courses.Where(c => c.LecturerID == lecturerId))
                cs.LecturerID = null;
            foreach (var gp in dbCtx.Groups.Where(c => c.LecturerID == lecturerId))
                gp.LecturerID = null;
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}
