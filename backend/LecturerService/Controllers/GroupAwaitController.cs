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
    public class GroupAwaitController : ControllerBase
    {
        readonly Model.LSContext dbCtx;
        readonly ILogger<GroupAwaitController> logger;

        public GroupAwaitController(Model.LSContext database, ILogger<GroupAwaitController> log)
        {
            dbCtx = database;
            logger = log;
        }

#region GET
        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.Group> Get()
        {
            return dbCtx.PendingGroups.Select(g => new Data.Group(g)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Data.Group Get(string nameId)
        {
            Model.Group gp = dbCtx.PendingGroups.Find(nameId);
            if (gp == null)
                return null;
            return new Data.Group(gp);
        }
#endregion // GET
#region POST
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Data.Group group)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            if (dbCtx.PendingGroups.Find(group.ID) == null)
            {
                Model.Course cs = dbCtx.Courses.Find(group.CourseID);
                if (cs == null)
                    return BadRequest();
                else if (cs.LecturerID != lc.ID && lc.RoleTypeID != Data.Role.Admin)
                    return Unauthorized();
                dbCtx.PendingGroups.Add(new Model.Group(group));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }
#endregion // POST
#region PUT
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Data.Group group)
        {
            Model.Lecturer lc = Data.Security.GetLecturer(HttpContext.User.Identity, dbCtx);
            if (lc == null)
                return Unauthorized();
            Model.Group gp = dbCtx.PendingGroups.Find(group.ID);
            if (gp == null)
                return NotFound();
            else if (lc.RoleTypeID != Data.Role.Admin && gp.Course.LecturerID != lc.ID)
                return Unauthorized();
            gp = new Model.Group(group);
            dbCtx.SaveChanges();
            // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
            return Ok();
        }
#endregion // PUT

        [HttpDelete]
        [Authorize]
        [Route("{nameId}")]
        public IActionResult Delete(string nameId)
        {
            if (!Data.Security.IsAdmin(HttpContext.User.Identity, dbCtx))
                return Unauthorized();
            Model.Group gp = dbCtx.PendingGroups.Find(nameId);
            if (gp == null)
                return NotFound();
            dbCtx.PendingGroups.Remove(gp);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}