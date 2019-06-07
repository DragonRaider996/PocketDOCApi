using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Token
    {
        [Column("UID")]
        [Key]
        public int Uid { get; set; }
        [Required]
        [Column("Token")]
        [MaxLength(500)]
        public string Token1 { get; set; }

        [ForeignKey("Uid")]
        [InverseProperty("Token")]
        public virtual User U { get; set; }
    }
}
