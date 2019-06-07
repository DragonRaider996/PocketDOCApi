using PocDoctor.Entitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class FirstAidData
    {
        public FirstAidQues Topic { get; set; }

        public List<FirstAidAns> Ans { get; set; }
    }
}
