using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS
{
    public class LoginUser
    {
        #region properties

        private string _password = string.Empty;
        private string _userRoles = string.Empty;
        private string _username = string.Empty;
        public Int64 Userid { get; set; }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        //private string email = string.Empty;
        //public string Email
        //{
        //    get { return email; }
        //    set { email = value; }
        //}
        //private string securityQuestion = string.Empty;
        //public string SecurityQuestion
        //{
        //    get { return securityQuestion; }
        //    set { securityQuestion = value; }
        //}
        //private string answer = string.Empty;
        //public string Answer
        //{
        //    get { return answer; }
        //    set { answer = value; }
        //}

        public string UserRoles
        {
            get { return _userRoles; }
            set { _userRoles = value; }
        }

        #endregion
    }
}
