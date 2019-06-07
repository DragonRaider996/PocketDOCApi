using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class FirstAidAns
    {
        public FirstAidAns()
        {
            FirstAidRel = new HashSet<FirstAidRel>();
        }

        [Column("AID")]
        [Key]
        public int Aid { get; set; }
        [Required]
        [Column("ANSWER")]
        [MaxLength(350)]
        public string Answer { get; set; }

        [InverseProperty("A")]
        public virtual ICollection<FirstAidRel> FirstAidRel { get; set; }
    }
}
