﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PF.Mobile.App.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
