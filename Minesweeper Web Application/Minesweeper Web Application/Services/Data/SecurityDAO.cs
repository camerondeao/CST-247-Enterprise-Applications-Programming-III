using Minesweeper_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Data
{
    public class SecurityDAO
    {
        //This method is where we'll register users into the database.
        public bool RegisterUser(UserModel user)
        {
            return true;
        }

        //This method is where we'll find potential users in the database.
        public bool FindByUser(UserModel user)
        {
            return true;
        }
    }
}