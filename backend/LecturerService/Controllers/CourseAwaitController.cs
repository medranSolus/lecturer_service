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
    public class CourseAwaitController : ControllerBase
    {
        readonly Model.LSContext dbCtx;
        readonly ILogger<CourseAwaitController> logger;

        public CourseAwaitController(Model.LSContext database, ILogger<CourseAwaitController> log)
        {
            dbCtx = database;
            logger = log;
        }

#region GET
        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.CourseShort> Get()
        {
            return dbCtx.PendingCourses.Select(c => new Data.CourseShort(c)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{courseId}")]
        public Data.Course Get(string courseId)
        {
            Model.Course cs = dbCtx.PendingCourses.Find(courseId);
            if (cs == null)
                return null;
            return new Data.Course(cs);
        }
#endregion // GET

#region POST
        [HttpPost]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Post(string courseId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.PendingCourses.Find(courseId);
            if (cs == null)
                return NotFound();
            else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                return Unauthorized();
            dbCtx.CoursesToCheck.Add(new Model.CourseMsg{ CourseID = courseId });
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Data.Course course)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            if (dbCtx.PendingCourses.Find(course.ID) == null)
            {
                course.LecturerID = lc.ID;
                dbCtx.PendingCourses.Add(new Model.Course(course));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }
#endregion // POST

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Course course)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.PendingCourses.Find(course.ID);
            if (cs == null)
                return NotFound();
            else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
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
            Model.Course cs = dbCtx.PendingCourses.Find(courseId);
            if (cs == null)
                return NotFound();
            else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                return Unauthorized();
            dbCtx.PendingCourses.Remove(cs);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}