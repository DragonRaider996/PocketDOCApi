using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class VerifyCode
    {
        [Column("UID")]
        [Key]
        public int Uid { get; set; }
        public int Code { get; set; }

        [ForeignKey("Uid")]
        [InverseProperty("VerifyCode")]
        public virtual User U { get; set; }
    }
}
