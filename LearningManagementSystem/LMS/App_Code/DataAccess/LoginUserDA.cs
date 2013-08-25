using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LMS.BusinessObject;

namespace LMS.DataAccess
{
    /// <summary>
    /// The LoginUserDA authenticates the user.
    /// </summary>
    public class LoginUserDA
    {
        #region Public Methods

        /// <summary>
        /// Authenticate the user.
        /// </summary>
        /// <param name="lUser">Login User to authenticate.</param>
        /// <returns>True if it is valid else false.</returns>
        public static bool IsValidUser(LoginUser lUser)
        {
            bool isRegisteredUser = false;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetLoginUser", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", lUser.Userid);
                myCommand.Parameters.AddWithValue("@id", lUser.Password);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        isRegisteredUser = true;
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return isRegisteredUser;
        }

        /// <summary>
        /// Gets the password of the user.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Password of the user.</returns>
        public static string GetPassword(string id)
        {
            string password = String.Empty;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetPassword", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        password = myDataReader.GetString(myDataReader.GetOrdinal("Password"));
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return password;
        }
        #endregion
    }

}