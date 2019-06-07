using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Remedies
    {
        public Remedies()
        {
            DisRem = new HashSet<DisRem>();
        }

        [Column("RID")]
        [Key]
        public int Rid { get; set; }
        [Required]
        [Column("RNAME")]
        [MaxLength(450)]
        public string Rname { get; set; }

        [InverseProperty("R")]
        public virtual ICollection<DisRem> DisRem { get; set; }
    }
}
