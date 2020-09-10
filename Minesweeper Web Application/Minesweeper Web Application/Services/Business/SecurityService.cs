using Minesweeper_Web_Application.Models;
using Minesweeper_Web_Application.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Business
{
    public class UserManagement
    {
        private static UserManagement _instance;
        public UserModel _loggedUser { get; set; } = new UserModel();

        private UserManagement() { }

        public static UserManagement Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new UserManagement();
                }
                return _instance;
            }
        }

        public void LogOutUser()
        {
            _loggedUser = null;
        }
    }
    public class SecurityService
    {
        SecurityDAO service = new SecurityDAO();
        public bool Register(UserModel user)
        {
            return service.RegisterUser(user);
        }

        public bool CheckUser(UserModel user)
        {
            return service.CheckUsername(user);
        }

        public bool Authenticate(UserLoginModel user)
        {
            return service.FindByUser(user);
        }
    }
}