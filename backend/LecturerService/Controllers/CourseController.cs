using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        readonly Model.LSContext dbCtx;

        public CourseController(Model.LSContext database)
        {
            dbCtx = database;
        }

#region GET
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(dbCtx.Courses.Include(c => c.Lecturer).Select(c => new Data.CourseShort(c)).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Get(string courseId)
        {
            Model.Course cs = dbCtx.Courses.Include(c => c.Lecturer).FirstOrDefault(c => c.ID == courseId);
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
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            if (dbCtx.Courses.Find(course.ID) == null)
            {
                course.LecturerID = lc.ID;
                if (lc.RoleTypeID != Data.Role.Admin)
                    course.Accepted = false;
                dbCtx.Courses.Add(new Model.Course(course));
                dbCtx.SaveChanges();
                return Ok();
            }
            return Conflict();
        }

        [HttpPost]
        [Authorize]
        [Route("batch")]
        public IActionResult PostBatch([FromBody]IEnumerable<Data.Course> courses)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null || lc.RoleTypeID != Data.Role.Admin)
                return Unauthorized();
            List<int> overlaps = new();
            for (int i = 0; i < courses.Count(); ++i)
            {
                var course = courses.ElementAt(i);
                if (dbCtx.Courses.Find(course.ID) == null)
                {
                    if (course.LecturerID == null)
                        course.LecturerID = lc.ID;
                    dbCtx.Courses.Add(new Model.Course(course));
                }
                else
                    overlaps.Add(i + 1);
            }
            dbCtx.SaveChanges();
            if (overlaps.Count == 0)
                return Ok();
            return Conflict(overlaps);
        }

        [HttpPost]
        [Authorize]
        [Route("accept")]
        public IActionResult Post([FromBody]Data.CourseMsg msg)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
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
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
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
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(course.ID);
            if (cs == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (cs.Accepted || cs.LecturerID != lc.ID))
                return Unauthorized();
            cs.Update(course);
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{courseId}")]
        public IActionResult Delete(string courseId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Course cs = dbCtx.Courses.Find(courseId);
            if (cs == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (cs.Accepted || cs.LecturerID != lc.ID))
                return Unauthorized();
            dbCtx.Courses.Remove(cs);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}
