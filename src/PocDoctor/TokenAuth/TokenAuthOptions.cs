﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace PocDoctor.TokenAuth
{
    public class TokenAuthOptions
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }
}
