using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class Hospital
    {
        [Column("HID")]
        [Key]
        public int Hid { get; set; }
        [Required]
        [Column("HNAME")]
        [MaxLength(150)]
        public string Hname { get; set; }
        [Required]
        [Column("HADDRESS")]
        [MaxLength(350)]
        public string Haddress { get; set; }
        [Required]
        [Column("HAREA")]
        [MaxLength(100)]
        public string Harea { get; set; }
        [Required]
        [Column("HCONTACT")]
        [MaxLength(50)]
        public string Hcontact { get; set; }
        [Required]
        [Column("HLAT")]
        [MaxLength(100)]
        public string Hlat { get; set; }
        [Required]
        [Column("HLONG")]
        [MaxLength(100)]
        public string Hlong { get; set; }
        [Required]
        [Column("HIMAGE")]
        [MaxLength(250)]
        public string Himage { get; set; }
    }
}
