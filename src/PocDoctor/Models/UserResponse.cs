using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocDoctor.Models
{
    public class UserResponse
    {
        public int UID { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
    }
}
