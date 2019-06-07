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
    [Route("api/register")]
    public class RegisterController : Controller
    {
        DatabaseRepo repo;

        public RegisterController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("")]
        public IActionResult Register([FromBody]Register register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {

                if (register.UserName.Length > 25)
                {
                    return BadRequest();
                }

                else if ((register.Password.Length > 15) || (register.Password.Length < 6))
                {
                    return BadRequest();
                }

                else { 
                    UidwithMessage message = repo.Register(register);

                    if (message.Message.Equals("Ok"))
                    {
                        return Ok(message);
                    }
                    else if (message.Message.Equals("Invalid Email"))
                    {
                        return BadRequest();
                    }
                    else if (message.Message.Equals("Username Already Exists !!!"))
                    {
                        return StatusCode(406);
                    }
                    else
                    {
                        return StatusCode(422);
                    }
                }
            }

        }

        [HttpPost("verify")]
        public IActionResult Verify([FromBody]Verification verify)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            { 
                UidwithMessage ans = repo.VerifyKey(verify);
                if (ans.Message.Equals("True"))
                {
                    return Ok(ans);
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
