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
    public class GroupController : ControllerBase
    {
        readonly Model.LSContext dbCtx;
        readonly ILogger<GroupController> logger;

        public GroupController(Model.LSContext database, ILogger<GroupController> log)
        {
            dbCtx = database;
            logger = log;
        }

        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.Group> Get()
        {
            return dbCtx.Groups.Select(g => new Data.Group(g)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Data.Group Get(string nameId)
        {
            Model.Group gp = dbCtx.Groups.Find(nameId);
            if (gp == null)
                return null;
            return new Data.Group(gp);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody]Data.Group group)
        {
            // TODO: Check if you have MASTER role or smth
            if (dbCtx.Groups.Find(group.ID) == null)
            {
                if (dbCtx.Courses.Find(group.CourseID) == null)
                    return BadRequest();
                dbCtx.Groups.Add(new Model.Group(group));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Data.Group group)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Group gp = dbCtx.Groups.Find(group.ID);
            if (gp == null)
                return NotFound();
            gp = new Model.Group(group);
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
            if (dbCtx.Groups.Find(nameId) == null)
                return NotFound();
            dbCtx.Groups.Remove(new Model.Group{ ID = nameId });
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}