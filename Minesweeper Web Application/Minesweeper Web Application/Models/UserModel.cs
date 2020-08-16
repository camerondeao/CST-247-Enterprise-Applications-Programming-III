using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Minesweeper_Web_Application.Models
{
    public enum Gender
    {
        Male, Female
    }

    public class UserModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "First name exceeded 25 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Last name exceeded 25 characters.")]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "State name exceeded 25 characters.")]
        public string State { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Email Address exceeded 50 characters.")]
        [EmailAddress]
        public string EmailAddress {get; set;}
        [Required]
        [StringLength(25, ErrorMessage = "Username exceeded 25 characters.")]
        public string UserName { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Password exceeded 25 characters.")]
        public string Password { get; set; }
    }
}