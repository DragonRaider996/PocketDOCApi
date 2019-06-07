using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class DisSymp
    {
        [Column("DID")]
        public int Did { get; set; }
        [Column("SID")]
        public int Sid { get; set; }

        [ForeignKey("Did")]
        [InverseProperty("DisSymp")]
        public virtual Diseases D { get; set; }
        [ForeignKey("Sid")]
        [InverseProperty("DisSymp")]
        public virtual Symptoms S { get; set; }
    }
}
