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

        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.CourseShort> Get()
        {
            return dbCtx.PendingCourses.Select(c => new Data.CourseShort(c)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Data.Course Get(string nameId)
        {
            Model.Course cs = dbCtx.PendingCourses.Find(nameId);
            if (cs == null)
                return null;
            return new Data.Course(cs);
        }

        [HttpPost]
        [Authorize]
        [Route("{nameId}")]
        public IActionResult Post(string nameId)
        {
            if (!Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return Unauthorized();
            Model.Course cs = dbCtx.PendingCourses.Find(nameId);
            if (cs == null)
                return BadRequest();
            dbCtx.PendingCourses.Remove(cs);
            dbCtx.Courses.Add(cs);
            foreach (var gp in dbCtx.PendingGroups.Where(g => g.CourseID == cs.ID))
            {
                dbCtx.PendingGroups.Remove(gp);
                dbCtx.Groups.Add(gp);
            }
            dbCtx.SaveChanges();
            return Conflict();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Data.Course course)
        {
            if (dbCtx.PendingCourses.Find(course.ID) == null)
            {
                dbCtx.PendingCourses.Add(new Model.Course(course));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

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
        [Route("{nameId}")]
        public IActionResult Delete(string nameId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.PendingCourses.Find(nameId);
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