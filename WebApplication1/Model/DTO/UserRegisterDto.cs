﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.DTO
{
    public class UserRegisterDto
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Gender { get; set; }

    }
}
