using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Prevention
    {
        public Prevention()
        {
            DisPrev = new HashSet<DisPrev>();
        }

        [Column("PID")]
        [Key]
        public int Pid { get; set; }
        [Required]
        [Column("PNAME")]
        [MaxLength(450)]
        public string Pname { get; set; }

        [InverseProperty("P")]
        public virtual ICollection<DisPrev> DisPrev { get; set; }
    }
}
