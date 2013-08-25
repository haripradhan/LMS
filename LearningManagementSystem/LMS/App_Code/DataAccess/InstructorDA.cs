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
    /// The InstructorDA class interacts with database to retrive and store information about Instructor. 
    /// </summary>
    public class InstructorDA
    {

        #region Public Methods

        /// <summary>
        /// Gets an instance of Instructor. 
        /// </summary>
        /// <param name="iid">The unique id of the Instructor.</param>
        /// <returns>A Instructor if ID matches, otherwise null.</returns>
        public static Instructor GetItem(string iid)
        {
            Instructor myInstructor = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetInstructor", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@IID", iid);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myInstructor = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myInstructor;
        }

        /// <summary>
        /// Gets a list of Instructors
        /// </summary>
        /// <returns>List of Instructors</returns>
        public static InstructorList GetItem()
        {
            InstructorList myInstructorList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectInstructorList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myInstructorList = new InstructorList();
                        while (myDataReader.Read())
                        {
                            myInstructorList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myInstructorList;
        }

        /// <summary>
        /// Saves a Instructor in the database.
        /// </summary>
        /// <param name="myInstructor">The Instructor to store.</param>
        /// <returns>The new ID if the Instructor is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Instructor myInstructor)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSaveInstructor", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@IID", myInstructor.IId);
                myCommand.Parameters.AddWithValue("@Password", myInstructor.Password);
                myCommand.Parameters.AddWithValue("@FName", myInstructor.FName);
                if (String.IsNullOrEmpty(myInstructor.MI))
                {
                    myCommand.Parameters.AddWithValue("@MI", DBNull.Value);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@MI", myInstructor.MI);
                }
                myCommand.Parameters.AddWithValue("@LName", myInstructor.LName);
                myCommand.Parameters.AddWithValue("@Street", myInstructor.Street);
                myCommand.Parameters.AddWithValue("@City", myInstructor.City);
                myCommand.Parameters.AddWithValue("@State", myInstructor.State);
                myCommand.Parameters.AddWithValue("@Zipcode", myInstructor.Zipcode);

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
        /// Deletes a Instructor from the database.
        /// </summary>
        /// <param name="sid">The ID of the studen to delete.</param>
        /// <returns>True if the Instructor was successfully deleted, or false otherwise.</returns>
        public static bool Delete(string iid)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteSingleInstructor", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@IID", iid);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates an instance of Instructor from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A Instructor</returns>
        private static Instructor FillRecord(IDataRecord myRecord)
        {
            Instructor myInstructor = new Instructor();
            myInstructor.IId = myRecord.GetString(myRecord.GetOrdinal("IID"));
            myInstructor.Password = myRecord.GetString(myRecord.GetOrdinal("Password"));
            myInstructor.FName = myRecord.GetString(myRecord.GetOrdinal("Fname"));
            if (!myRecord.IsDBNull(myRecord.GetOrdinal("MI")))
            {
                myInstructor.MI = myRecord.GetString(myRecord.GetOrdinal("MI"));
            }
            myInstructor.LName = myRecord.GetString(myRecord.GetOrdinal("Lname"));
            myInstructor.Street = myRecord.GetString(myRecord.GetOrdinal("Street"));
            myInstructor.City = myRecord.GetString(myRecord.GetOrdinal("City"));
            myInstructor.State = myRecord.GetString(myRecord.GetOrdinal("State"));
            myInstructor.Zipcode = myRecord.GetString(myRecord.GetOrdinal("Zipcode"));
            return myInstructor;
        }

        #endregion
    }
}