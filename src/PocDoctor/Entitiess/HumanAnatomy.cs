using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class HumanAnatomy
    {
        [Column("HAID")]
        [Key]
        public int Haid { get; set; }
        [Column("HATOPIC")]
        [MaxLength(150)]
        public string Hatopic { get; set; }
    }
}
