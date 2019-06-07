using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;
using PocDoctor.Entitiess;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/hospital")]
    public class HospitalController : Controller
    {
        DatabaseRepo repo;

        public HospitalController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("{area}")]
        [Authorize("Bearer")]
        public IActionResult GetHospital(String area)
        {
            List<Hospital> hospital = repo.HospByArea(area);
            if (hospital == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(hospital);
            }
        }
    }
}
