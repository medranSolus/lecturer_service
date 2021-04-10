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
    public class CourseController : ControllerBase
    {
        readonly Model.LSContext dbCtx;
        readonly ILogger<CourseController> logger;

        public CourseController(Model.LSContext database, ILogger<CourseController> log)
        {
            dbCtx = database;
            logger = log;
        }

#region GET
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(dbCtx.Courses.Select(c => new Data.CourseShort(c)).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Get(string courseId)
        {
            Model.Course cs = dbCtx.Courses.Find(courseId);
            if (cs == null)
                return NotFound();
            return Ok(new Data.Course(cs));
        }
#endregion // GET

#region POST
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Data.Course course)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            if (dbCtx.Courses.Find(course.ID) == null)
            {
                course.LecturerID = lc.ID;
                if (lc.RoleTypeID != Data.Role.Admin)
                    course.Accepted = false;
                dbCtx.Courses.Add(new Model.Course(course));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPost]
        [Authorize]
        [Route("accept")]
        public IActionResult Post([FromBody]Data.CourseMsg msg)
        {
            if (!Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(msg.CourseID);
            Model.CourseMsg csMsg = dbCtx.CoursesToCheck.Find(msg.ID);
            if (cs == null || csMsg == null)
                return NotFound();
            cs.Accepted = true;
            dbCtx.CoursesToCheck.Remove(csMsg);
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Post(string courseId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(courseId);
            if (cs == null)
                return NotFound();
            else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                return Unauthorized();
            else if (cs.Accepted)
                return BadRequest();
            dbCtx.CoursesToCheck.Add(new Model.CourseMsg{ CourseID = courseId });
            dbCtx.SaveChanges();
            return Ok();
        }
#endregion // POST

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Course course)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(course.ID);
            if (cs == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (cs.Accepted || !cs.Accepted && cs.LecturerID != lc.ID))
                return Unauthorized();
            cs = new Model.Course(course);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Delete(string courseId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(courseId);
            if (cs == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (cs.Accepted || !cs.Accepted && cs.LecturerID != lc.ID))
                return Unauthorized();
            dbCtx.Courses.Remove(cs);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}