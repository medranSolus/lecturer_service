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

        [HttpGet]
        //[Authorize]
        public IEnumerable<Data.CourseShort> Get()
        {
            return dbCtx.Courses.Select(c => new Data.CourseShort(c)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Data.Course Get(string nameId)
        {
            Model.Course cs = dbCtx.Courses.Find(nameId);
            if (cs == null)
                return null;
            return new Data.Course(cs);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody]Data.Course course)
        {
            // TODO: Check if you have MASTER role or smth
            if (dbCtx.Courses.Find(course.ID) == null)
            {
                dbCtx.Courses.Add(new Model.Course(course));
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Data.Course course)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Course cs = dbCtx.Courses.Find(course.ID);
            if (cs == null)
                return NotFound();
            cs = new Model.Course(course);
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
            if (dbCtx.Courses.Find(nameId) == null)
                return NotFound();
            dbCtx.Courses.Remove(new Model.Course{ ID = nameId });
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}