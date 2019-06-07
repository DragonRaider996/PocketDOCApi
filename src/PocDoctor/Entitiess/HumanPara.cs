using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class HumanPara
    {
        [Column("HAPID")]
        public int Hapid { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Paragraph { get; set; }
    }
}
