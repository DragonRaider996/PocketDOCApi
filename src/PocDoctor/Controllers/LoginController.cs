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
    [Route("api/login")]
    public class LoginController : Controller
    {
        DatabaseRepo repo;
        public LoginController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("")]
        public IActionResult Login([FromBody]Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                UserResponse response = repo.ValidateUser(login);

                if (response == null)
                {
                    response = new UserResponse();
                    response.RefreshToken = "InvalidUsername";
                    response.UID = 0;
                    return BadRequest(response);
                }

                else if (response.RefreshToken.Equals("Email Validation Required"))
                {
                    response.UID = 0;
                    return StatusCode(422);
                }

                else
                {
                    return Ok(response);
                }
            }
        }

    }
}
