using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class ForumComments
    {
        [Column("SRNO")]
        [Key]
        public int Srno { get; set; }
        [Column("FID")]
        public int Fid { get; set; }
        [Column("UID")]
        public int Uid { get; set; }
        [Required]
        [Column("COMMENTS")]
        [MaxLength(1500)]
        public string Comments { get; set; }
    }
}
