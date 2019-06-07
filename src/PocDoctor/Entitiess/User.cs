using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocDoctor.Entitiess
{
    public partial class User
    {
        [Column("UID")]
        [Key]
        public int Uid { get; set; }
        [Required]
        [Column("UNAME")]
        [MaxLength(150)]
        public string Uname { get; set; }
        [Required]
        [Column("EMAIL")]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        [Column("VERIFIED")]
        [MaxLength(50)]
        public string Verified { get; set; }
        [Required]
        [Column("PASSWORD")]
        [MaxLength(250)]
        public string Password { get; set; }
        [Required]
        [Column("ROLE")]
        [MaxLength(50)]
        public string Role { get; set; }

        [InverseProperty("U")]
        public virtual Token Token { get; set; }
        [InverseProperty("U")]
        public virtual VerifyCode VerifyCode { get; set; }
    }
}
