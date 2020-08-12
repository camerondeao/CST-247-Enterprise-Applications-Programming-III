
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Models
{
    public enum Gender
    {
        Male, Female
    }

    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string EmailAddress {get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}