using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class FacebookEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
    public class GoogleEntity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
    }
}
