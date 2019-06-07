using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Forum
    {
        [Column("FID")]
        [Key]
        public int Fid { get; set; }
        [Column("UID")]
        public int Uid { get; set; }
        [Required]
        [Column("QUESTION")]
        [MaxLength(1500)]
        public string Question { get; set; }
        [Required]
        [Column("DESCRIPTION")]
        [MaxLength(1500)]
        public string Description { get; set; }
    }
}
