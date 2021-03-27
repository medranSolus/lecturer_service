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
        public IEnumerable<Data.Course> Get()
        {
            return dbCtx.Courses.Select(c => new Data.Course(c)).ToArray();
        }

        [HttpGet]
        //[Authorize]
        [Route("{nameId}")]
        public Model.Course Get(string nameId)
        {
            return dbCtx.Courses.Find(new Model.Course{ ID = nameId });
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody]Model.Course course)
        {
            // TODO: Check if you have MASTER role or smth
            if (dbCtx.Courses.Find(course) == null)
            {
                dbCtx.Courses.Add(course);
                dbCtx.SaveChanges();
                // Maybe check if correct save (no errors when adding model without all required fields on, etc, dunno)
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        //[Authorize]
        public IActionResult Put([FromBody]Model.Course course)
        {
            // TODO: Check if you have MASTER role or smth
            Model.Course cs = dbCtx.Courses.Find(course);
            if (cs == null)
                return NotFound();
            cs = course;
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
            Model.Course cs = new Model.Course{ ID = nameId };
            if (dbCtx.Courses.Find(cs) == null)
                return NotFound();
            dbCtx.Courses.Remove(cs);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}