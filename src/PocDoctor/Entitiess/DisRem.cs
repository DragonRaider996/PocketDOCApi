using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class DisRem
    {
        [Column("DID")]
        public int Did { get; set; }
        [Column("RID")]
        public int Rid { get; set; }

        [ForeignKey("Did")]
        [InverseProperty("DisRem")]
        public virtual Diseases D { get; set; }
        [ForeignKey("Rid")]
        [InverseProperty("DisRem")]
        public virtual Remedies R { get; set; }
    }
}
