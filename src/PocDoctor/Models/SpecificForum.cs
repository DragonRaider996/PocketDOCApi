using PocDoctor.Entitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class SpecificForum
    {
        public int Fid { get; set; }
        public string Question { get; set; }

        public string Description { get; set; }

        public List<Comments> Comments { get; set; }


    }
}
