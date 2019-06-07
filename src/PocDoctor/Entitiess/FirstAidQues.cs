using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class FirstAidQues
    {
        public FirstAidQues()
        {
            FirstAidRel = new HashSet<FirstAidRel>();
        }

        [Column("FAID")]
        [Key]
        public int Faid { get; set; }
        [Required]
        [Column("FAQUESTION")]
        [MaxLength(350)]
        public string Faquestion { get; set; }

        [InverseProperty("Fa")]
        public virtual ICollection<FirstAidRel> FirstAidRel { get; set; }
    }
}
