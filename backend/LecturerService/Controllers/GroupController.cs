using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        readonly Model.LSContext dbCtx;

        public GroupController(Model.LSContext database)
        {
            dbCtx = database;
        }

#region GET
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(dbCtx.Groups.Include(g => g.Course).Select(g => new Data.Group(g)).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{groupId}")]
        public IActionResult Get(string groupId)
        {
            Model.Group gp = dbCtx.Groups.Include(g => g.Course).FirstOrDefault(g => g.ID == groupId);
            if (gp == null)
                return NotFound();
            return Ok(new Data.Group(gp));
        }

        [HttpGet]
        [Authorize]
        [Route("lecturer")]
        public IActionResult GetLecturer()
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            return Ok(dbCtx.Groups.Where(g => g.LecturerID == lc.ID).Include(g => g.Course).Select(g => new Data.Group(g)).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("course/{courseId}")]
        public IActionResult GetCourse(string courseId)
        {
            return Ok(dbCtx.Groups.Where(g => g.CourseID == courseId).Include(g => g.Course).Select(g => new Data.Group(g)).ToArray());
        }
#endregion // GET

#region POST
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Data.Group group)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            if (dbCtx.Groups.Find(group.ID) == null)
            {
                Model.Course cs = dbCtx.Courses.Find(group.CourseID);
                if (cs == null)
                    return BadRequest();
                else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                    return Unauthorized();
                dbCtx.Groups.Add(new Model.Group(group));
                dbCtx.SaveChanges();
                return Ok();
            }
            return Conflict();
        }

        [HttpPost]
        [Authorize]
        [Route("batch")]
        public IActionResult PostBatch([FromBody]IEnumerable<Data.Group> groups)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
                return Unauthorized();
            List<KeyValuePair<int, bool>> overlaps = new();
            for (int i = 0; i < groups.Count(); ++i)
            {
                var group = groups.ElementAt(i);
                if (dbCtx.Groups.Find(group.ID) == null)
                {
                    Model.Course cs = dbCtx.Courses.Find(group.CourseID);
                    if (cs == null)
                        overlaps.Add(new(i + 1, true));
                    else
                        dbCtx.Groups.Add(new Model.Group(group));
                }
                else
                    overlaps.Add(new(i + 1, false));
            }
            dbCtx.SaveChanges();
            if (overlaps.Count == 0)
                return Ok();
            return Conflict(overlaps);
        }

        [HttpPost]
        [Authorize]
        [Route("accept")]
        public IActionResult Post([FromBody]Data.GroupMsg msg)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Group gp = dbCtx.Groups.Include(g => g.Course).FirstOrDefault(g => g.ID == msg.GroupID);
            Model.GroupMsg gpMsg = dbCtx.GroupNotification.Find(msg.ID);
            if (gp == null || gpMsg == null)
                return NotFound();
            else if (gp.Course.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                return Unauthorized();
            gp.LecturerID = msg.LecturerID;
            dbCtx.GroupNotification.RemoveRange(dbCtx.GroupNotification.Where(m => m.GroupID == msg.GroupID));
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("{groupId}")]
        public IActionResult Post(string groupId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Group gp = dbCtx.Groups.Find(groupId);
            if (gp == null)
                return NotFound();
            dbCtx.GroupNotification.Add(new Model.GroupMsg{ GroupID = groupId, LecturerID = lc.ID });
            dbCtx.SaveChanges();
            return Ok();
        }
#endregion // POST

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Group group)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Group gp = dbCtx.Groups.Include(g => g.Course).FirstOrDefault(g => g.ID == group.ID);
            if (gp == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (gp.Course.Accepted || gp.Course.LecturerID != lc.ID))
                return Unauthorized();
            gp.Update(group);
            dbCtx.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("{groupId}")]
        public IActionResult Delete(string groupId)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Group gp = dbCtx.Groups.Include(g => g.Course).FirstOrDefault(g => g.ID == groupId);
            if (gp == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && (gp.Course.Accepted || gp.Course.LecturerID != lc.ID))
                return Unauthorized();
            dbCtx.Groups.Remove(gp);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}
