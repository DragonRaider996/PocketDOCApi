using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Symptoms
    {
        public Symptoms()
        {
            DisSymp = new HashSet<DisSymp>();
        }

        [Column("SID")]
        [Key]
        public int Sid { get; set; }
        [Required]
        [Column("SNAME")]
        [MaxLength(250)]
        public string Sname { get; set; }

        [InverseProperty("S")]
        public virtual ICollection<DisSymp> DisSymp { get; set; }
    }
}
