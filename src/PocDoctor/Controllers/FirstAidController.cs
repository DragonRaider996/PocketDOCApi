using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;
using PocDoctor.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/firstaid")]
    public class FirstAidController : Controller
    {
        DatabaseRepo repo;
        public FirstAidController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        [Authorize("Bearer")]
        public IActionResult GetTopic()
        {
            return Ok(repo.FirstAidTopic());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public IActionResult GetSpecifiFirstAid(int id)
        {
            FirstAidData data;
            data = repo.GetFirstAid(id);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
