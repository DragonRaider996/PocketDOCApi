using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class DisPrev
    {
        [Column("DID")]
        public int Did { get; set; }
        [Column("PID")]
        public int Pid { get; set; }

        [ForeignKey("Did")]
        [InverseProperty("DisPrev")]
        public virtual Diseases D { get; set; }
        [ForeignKey("Pid")]
        [InverseProperty("DisPrev")]
        public virtual Prevention P { get; set; }
    }
}
