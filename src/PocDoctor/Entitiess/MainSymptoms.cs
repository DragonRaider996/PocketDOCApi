using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class MainSymptoms
    {
        [Column("SID")]
        [Key]
        public int Sid { get; set; }
        [Required]
        [Column("SNAME")]
        [MaxLength(250)]
        public string Sname { get; set; }
    }
}
