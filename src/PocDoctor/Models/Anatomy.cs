using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class Anatomy
    {
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public List<string> Paragraph { get; set; }
    }
}
