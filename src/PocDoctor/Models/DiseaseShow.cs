using PocDoctor.Entitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class DiseaseShow
    {
        public Diseases disease { get; set; }

        public ICollection<Symptoms> symptoms { get; set; }

        public ICollection<Remedies> remedies { get; set; }

        public ICollection<Prevention> prevention { get; set; }

    }
}
