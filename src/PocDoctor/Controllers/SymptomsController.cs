using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocDoctor.Repositiries;
using Microsoft.AspNetCore.Authorization;
using PocDoctor.Entitiess;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PocDoctor.Controllers
{
    [Route("api/symptoms")]
    public class SymptomsController : Controller
    {
        DatabaseRepo repo;
        public SymptomsController(DatabaseRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("")]
        [Authorize("Bearer")]

        public IActionResult GetMAinSymptoms()
        {
            ICollection<MainSymptoms> symp;
            symp = repo.GetMainSymp();
            return Ok(symp);
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]

        public IActionResult GetOtherSymptoms(int id)
        {
            ICollection<Symptoms> symp;
            symp = repo.GetOtherSymptoms(id);
            if (symp == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(symp);
            }
        }

        [HttpPost("{id}/disease")]
        [Authorize("Bearer")]

        public IActionResult GetDisease(int id, [FromBody]List<int> sympid)
        {
            if (sympid.Count == 0)
            {
                return BadRequest();
            }
            ICollection<Diseases> disease;
            disease = repo.GetDiseaseBySymptoms(id, sympid);
            if (disease == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(disease);
            }
        }
    }
}
