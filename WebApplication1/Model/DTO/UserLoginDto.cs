﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.DTO
{
    public class UserLoginDto
    {   [Required]
        public string UserName { get; set; }

        
        [Required]
        public string Password { get; set; }
    }
}
