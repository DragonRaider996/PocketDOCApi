using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class Verification
    {
        [Required]
        public int Uid { get; set; }
        [Required]
        public int Code { get; set; }
    }
}
