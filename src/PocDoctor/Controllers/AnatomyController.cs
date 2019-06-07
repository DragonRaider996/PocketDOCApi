using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;
using PocDoctor.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/anatomy")]
    public class AnatomyController : Controller
    {

        DatabaseRepo repo;

        public AnatomyController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        public IActionResult GetAllAnatomy()
        {
            return Ok(repo.GetHumanAnatomy());
        }

        [HttpGet("{id}")]
        public IActionResult GetSpecificAnantomy(int id)
        {
            Anatomy anatomy = repo.GetSpecificAnatomy(id);
            if (anatomy == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(anatomy);
            }
        }
    }
}
