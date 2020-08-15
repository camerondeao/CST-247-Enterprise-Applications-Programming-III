using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Business
{
    public class SecurityService
    {
        SecurityDAO service = new SecurityDAO();
        public bool Register(UserModel user)
        {
            return service.RegisterUser(user);
        }

        public bool Authenticate(UserLoginModel user)
        {
            return service.FindByUser(user);
        }
    }
}