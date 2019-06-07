using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class FirstAidRel
    {
        [Column("FAID")]
        public int Faid { get; set; }
        [Column("AID")]
        public int Aid { get; set; }

        [ForeignKey("Aid")]
        [InverseProperty("FirstAidRel")]
        public virtual FirstAidAns A { get; set; }
        [ForeignKey("Faid")]
        [InverseProperty("FirstAidRel")]
        public virtual FirstAidQues Fa { get; set; }
    }
}
