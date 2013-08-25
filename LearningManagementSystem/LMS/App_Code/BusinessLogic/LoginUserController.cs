using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LMS.BusinessObject;
using LMS.DataAccess;


namespace LMS.BusinessLogic
{
    public class LoginUserController
    {
        #region Public Properties

        /// <summary>
        /// Authenticate the user.
        /// </summary>
        /// <param name="lUser">Login User to authenticate.</param>
        /// <returns>True if it is valid else false.</returns>
        public static bool IsValidUser(LoginUser lUser)
        {

            return true;// LoginUserDA.IsValidUser(lUser);
        }

        /// <summary>
        /// Gets the password of the user.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Password of the user.</returns>
        public static string GetPassword(string id)
        {
            return "test";// LoginUserDA.GetPassword(id);
        }
        #endregion
    }
}