using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Diseases
    {
        public Diseases()
        {
            DisPrev = new HashSet<DisPrev>();
            DisRem = new HashSet<DisRem>();
            DisSymp = new HashSet<DisSymp>();
        }

        [Column("DID")]
        [Key]
        public int Did { get; set; }
        [Required]
        [Column("DNAME")]
        [MaxLength(150)]
        public string Dname { get; set; }
        [Required]
        [Column("DINFO")]
        [MaxLength(1500)]
        public string Dinfo { get; set; }

        [InverseProperty("D")]
        public virtual ICollection<DisPrev> DisPrev { get; set; }
        [InverseProperty("D")]
        public virtual ICollection<DisRem> DisRem { get; set; }
        [InverseProperty("D")]
        public virtual ICollection<DisSymp> DisSymp { get; set; }
    }
}
