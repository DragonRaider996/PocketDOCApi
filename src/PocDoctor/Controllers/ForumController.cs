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
    [Route("api/forum")]
    public class ForumController : Controller
    {
        DatabaseRepo repo;
    
        public ForumController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        [Authorize("Bearer")]
        public IActionResult GetAllQuestion()
        {
            return Ok(repo.GetAllForum());
        }

        [HttpPost("")]
        [Authorize("Bearer")]
        public IActionResult PostQuestion([FromBody]ForumData data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (data.Uid < 0)
            {
                return StatusCode(417);
            }
            else if (data.Question.Length < 1 || data.Question.Length>1400 || data.Description.Length>1400)
            {
                return StatusCode(417);
            }
            else
            {
                string temp = repo.PostData(data);
                if (temp.Equals("Success"))
                {
                    Reply obj = new Reply
                    {
                        Message = temp
                    };
                    return Ok(obj);
                }
                else
                {
                    return BadRequest();
                }
            }

        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]

        public IActionResult GetSpecificForum(int id)
        {
            SpecificForum forum = repo.GetSpecificForum(id);
            if (forum == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(forum);
            }
        }


        [HttpPost("{id}/comments")]
        [Authorize("Bearer")]

        public IActionResult PostComments([FromBody]CommentPost comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                if (comment.Fid < 0)
                {
                    return StatusCode(417);
                }
                else if (comment.Uid < 0)
                {
                    return StatusCode(417);
                }
                else if (comment.Comments.Length < 1 || comment.Comments.Length > 1400)
                {
                    return StatusCode(417);
                }
                else
                {
                    string temp = repo.PostComment(comment);
                    if (temp.Equals("Invalid User ID"))
                    {
                        return StatusCode(417);
                    }
                    else if (temp.Equals("Invalid Question"))
                    {
                        return StatusCode(417);
                    }
                    else if (temp.Equals("Invalid User"))
                    {
                        return Unauthorized();
                    }
                    else
                    {
                        temp = "Success";
                        Reply obj = new Reply {
                            Message = temp
                        };
                        return Ok(obj);
                    }
                }
            }
        }

    }
}
