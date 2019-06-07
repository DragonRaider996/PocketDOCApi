using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class HumanRelation
    {
        [Column("HAID")]
        public int Haid { get; set; }
        [Column("HAPID")]
        public int Hapid { get; set; }
    }
}
