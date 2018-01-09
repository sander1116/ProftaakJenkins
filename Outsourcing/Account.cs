﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outsourcing_F_Reizen.Models
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Account(string username, string password)
        {
            this.Username = username;
            this.Password = PasswordManager.Hash(password);
        }
    }
}