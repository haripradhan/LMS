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
    /// The StudentDA class interacts with database to retrive and store information about student. 
    /// </summary>
    static class StudentDA
    {

        #region Public Methods
        /// <summary>
        /// Gets an instance of Student. 
        /// </summary>
        /// <param name="sid">The unique id of the student.</param>
        /// <returns>A Student if ID matches, otherwise null.</returns>
        public static Student GetItem(string sid)
        {
            Student myStudent = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectSingleStudent",myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@sid", sid);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myStudent = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myStudent;
        }

        /// <summary>
        /// Gets a list of Students
        /// </summary>
        /// <returns>List of Students</returns>
        public static StudentList GetItem()
        {
            StudentList myStudentList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectStudentList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myStudentList = new StudentList();
                        while (myDataReader.Read())
                        {
                            myStudentList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myStudentList;
        }
    
        /// <summary>
        /// Saves a student in the database.
        /// </summary>
        /// <param name="myStudent">The student to store.</param>
        /// <returns>The new ID if the student is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Student myStudent)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSaveStudent",myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@SID", myStudent.SId);
                myCommand.Parameters.AddWithValue("@Password", myStudent.Password);
                myCommand.Parameters.AddWithValue("@FName", myStudent.FName);
                if (String.IsNullOrEmpty(myStudent.MI))
                {
                    myCommand.Parameters.AddWithValue("@MI", DBNull.Value);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@MI", myStudent.MI);    
                }
                myCommand.Parameters.AddWithValue("@LName", myStudent.LName);
                myCommand.Parameters.AddWithValue("@Street", myStudent.Street);
                myCommand.Parameters.AddWithValue("@City", myStudent.City);
                myCommand.Parameters.AddWithValue("@State", myStudent.State);
                myCommand.Parameters.AddWithValue("@Zipcode", myStudent.Zipcode);

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
        /// Deletes a student from the database.
        /// </summary>
        /// <param name="sid">The ID of the studen to delete.</param>
        /// <returns>True if the student was successfully deleted, or false otherwise.</returns>
        public static bool Delete(string sid)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand   myCommand = new SqlCommand("spDeleteSingleStudent",myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@SID", sid);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates an instance of Student from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A student</returns>
        private static Student FillRecord(IDataRecord myRecord)
        {
            Student myStudent = new Student();
            myStudent.SId = myRecord.GetString(myRecord.GetOrdinal("SID"));
            myStudent.Password = myRecord.GetString(myRecord.GetOrdinal("Password"));
            myStudent.FName = myRecord.GetString(myRecord.GetOrdinal("Fname"));
            if (!myRecord.IsDBNull(myRecord.GetOrdinal("MI")))
            {
                myStudent.MI = myRecord.GetString(myRecord.GetOrdinal("MI"));    
            }
            myStudent.LName = myRecord.GetString(myRecord.GetOrdinal("Lname"));
            myStudent.Street = myRecord.GetString(myRecord.GetOrdinal("Street"));
            myStudent.City = myRecord.GetString(myRecord.GetOrdinal("City"));
            myStudent.State = myRecord.GetString(myRecord.GetOrdinal("State"));
            myStudent.Zipcode = myRecord.GetString(myRecord.GetOrdinal("Zipcode"));
            return myStudent;
        }
        #endregion
    }
}