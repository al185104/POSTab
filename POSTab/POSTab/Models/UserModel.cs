﻿using POSTab.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSTab.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Pin { get; set; }
    }
}
