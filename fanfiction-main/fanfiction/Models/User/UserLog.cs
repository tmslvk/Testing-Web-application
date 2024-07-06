﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using fanfiction.Models.User.Inretfaces;

namespace fanfiction.Models.User
{
    public class UserLog:IActionResult
    {
        [StringLength(20)]
        public string name { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        public string Email { get; set; }

        public string ReturnUrl { get; set; }

        public string Datetime { get; set; }
        public string lang;

        public DateTime GetDate() { return Convert.ToDateTime(Datetime); }
    }
}
