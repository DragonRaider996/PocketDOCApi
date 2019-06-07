using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/doctor")]
    public class DoctorController : Controller
    {
        DatabaseRepo repo;
        public DoctorController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("{id}")]
        public IActionResult ConvertToDoc(int id)
        {
            string temp = repo.ConvertDoc(id);
            if (temp == "Invalid User ID")
            {
                return BadRequest();
            }
            else
            {
                return StatusCode(201);
            }
        }
    }
}
