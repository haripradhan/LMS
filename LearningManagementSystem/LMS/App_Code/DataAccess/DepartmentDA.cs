using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LMS.BusinessObject;
using LMS.BusinessObject.List;

namespace LMS.DataAccess
{

    /// <summary>
    /// The DepartmentDA class interacts with database to retrive and store information about the department. 
    /// </summary>
    public class DepartmentDA
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of Department. 
        /// </summary>
        /// <param name="dno">Department number.</param>
        /// <returns>A Department if it matches, otherwise null.</returns>
        public static Department GetItem(int dno)
        {
            Department myDepartment = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectSingleDepartment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DNo", dno);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myDepartment = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myDepartment;
        }

        /// <summary>
        /// Gets a list of Departments
        /// </summary>
        /// <returns>List of Departments</returns>
        public static DepartmentList GetItem()
        {
            DepartmentList myDepartmentList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectDepartmentList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myDepartmentList = new DepartmentList();
                        while (myDataReader.Read())
                        {
                            myDepartmentList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myDepartmentList;
        }

        /// <summary>
        /// Saves an Department in the database.
        /// </summary>
        /// <param name="myDepartment">The Department to store.</param>
        /// <returns>The new Department id if the Department is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Department myDepartment)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSaveDepartment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DNo", myDepartment.DNo);
                myCommand.Parameters.AddWithValue("@DName", myDepartment.DName);
                myCommand.Parameters.AddWithValue("@DLocation", myDepartment.DLocation);
                
                DbParameter retValue = myCommand.CreateParameter();
                retValue.Direction = ParameterDirection.ReturnValue;
                myCommand.Parameters.Add(retValue);

                myConnection.Open();
                myCommand.ExecuteNonQuery();
                result = Convert.ToInt32(retValue.Value);
                myConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// Deletes an Department from the database.
        /// </summary>
        /// <param name="AFileLocation">A file location of an Department.</param>
        /// <returns>True if the department was successfully deleted, or false otherwise.</returns>
        public static bool Delete(int dno)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteSingleDepartment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@DNo", dno);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates an instance of Department from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>An Department.</returns>
        private static Department FillRecord(IDataRecord myRecord)
        {
            Department myDepartment = new Department();
            myDepartment.DNo = myRecord.GetInt32(myRecord.GetOrdinal("DNo"));
            myDepartment.DName = myRecord.GetString(myRecord.GetOrdinal("DName"));
            myDepartment.DLocation = myRecord.GetString(myRecord.GetOrdinal("DLocation"));
            return myDepartment;
        }
        #endregion
    }
}