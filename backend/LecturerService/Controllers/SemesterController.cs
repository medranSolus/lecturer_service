using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SemesterController : ControllerBase
    {
        readonly Model.LSContext dbCtx;

        public SemesterController(Model.LSContext database)
        {
            dbCtx = database;
        }

#region GET
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(dbCtx.Semesters.ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("id")]
        public IActionResult GetIDs()
        {
            return Ok(dbCtx.Semesters.Select(l => l.ID).ToArray());
        }

        [HttpGet]
        [Authorize]
        [Route("{semesterId}")]
        public IActionResult Get(string semesterId)
        {
            Model.Semester sm = dbCtx.Semesters.Find(semesterId);
            if (sm == null)
                return NotFound();
            return Ok(sm);
        }
#endregion // GET

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Model.Semester semester)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
                return Unauthorized();
            if (dbCtx.Semesters.Find(semester.ID) == null)
            {
                dbCtx.Semesters.Add(semester);
                dbCtx.SaveChanges();
                return Ok();
            }
            return Conflict();
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Model.Semester semester)
        {
            if (!Data.Security.IsAdmin(HttpContext, dbCtx))
                return Unauthorized();
            Model.Semester sm = dbCtx.Semesters.Find(semester.ID);
            if (sm == null)
                return NotFound();
            sm.Update(semester);
            dbCtx.SaveChanges();
            return Ok();
        }
    }
}
