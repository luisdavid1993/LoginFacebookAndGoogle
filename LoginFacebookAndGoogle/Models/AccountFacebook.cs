﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginFacebookAndGoogle.Models
{
    public class AccountFacebook
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Locale { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}