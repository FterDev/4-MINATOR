﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourMinator.Auth.Persistence.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}