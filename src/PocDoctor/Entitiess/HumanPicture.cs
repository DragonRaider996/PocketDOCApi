using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class HumanPicture
    {
        [Column("SRNO")]
        [Key]
        public int Srno { get; set; }
        [Column("HAID")]
        public int Haid { get; set; }
        [Required]
        [Column("IMAGE")]
        [MaxLength(150)]
        public string Image { get; set; }
    }
}
