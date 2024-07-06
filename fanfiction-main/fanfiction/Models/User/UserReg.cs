﻿using System;
using System.ComponentModel.DataAnnotations;
using fanfiction.Models.User.Inretfaces;

namespace fanfiction.Models.User
{
    public class UserReg:IActionResult
    {
        [StringLength(20)]
        public string name { get; set; }

        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }
        public  string lang { get; set; }

        public bool CheckPasswords()
        {
            return Password != PasswordConfirm;
        }

        public string Datetime { get; set; }

        public DateTime GetDate() { return Convert.ToDateTime(Datetime);}
    }
}