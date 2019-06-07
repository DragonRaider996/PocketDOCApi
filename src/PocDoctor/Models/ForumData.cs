using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class ForumData
    {
        public int Fid { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public int Uid { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
