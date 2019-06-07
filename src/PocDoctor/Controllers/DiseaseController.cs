using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;
using Microsoft.AspNetCore.Authorization;
using PocDoctor.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/diseases")]
    public class DiseaseController : Controller
    {
        DatabaseRepo repo;
        public DiseaseController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        [Authorize("Bearer")]
        public IActionResult GetAllDisease()
        {
            return Ok(repo.GetAllDisease());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]

        public IActionResult GetSpecificDisease(int id)
        {
            DiseaseShow show;
            show = repo.GetSpecificDisease(id);
            if (show == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(show);
            }
        }
    }
}
