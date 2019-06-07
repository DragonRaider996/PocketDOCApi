using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class CommentPost
    {
        [Required]
        public int Fid { get; set; }

        [Required]
        public int Uid { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}
