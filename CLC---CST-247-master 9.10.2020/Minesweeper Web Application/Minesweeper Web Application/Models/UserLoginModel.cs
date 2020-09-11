using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Minesweeper_Web_Application.Models
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "Username exceeded 25 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Password exceeded 25 characters.")]
        public string Password { get; set; }

    }
}