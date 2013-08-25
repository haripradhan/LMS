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
    /// The AssignmentDA class interacts with database to retrive and store information about assignment. 
    /// </summary>
    public class AssignmentDA
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of Assignment. 
        /// </summary>
        /// <param name="assignmentID">A unique ID of assignment.</param>
        /// <returns>An Assignment if it matches, otherwise null.</returns>
        public static Assignment GetItem(int assignmentID)
        {
            Assignment myAssignment = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectSingleAssignment",myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@assignmentID", assignmentID);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myAssignment = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myAssignment;
        }

        /// <summary>
        /// Gets a list of Assignments
        /// </summary>
        /// <returns>List of Assignments</returns>
        public static AssignmentList GetItem(string courseID)
        {
            AssignmentList myAssignmentList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetAssignmentByCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@courseID", courseID);
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myAssignmentList = new AssignmentList();
                        while (myDataReader.Read())
                        {
                            myAssignmentList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myAssignmentList;
        }
    
        /// <summary>
        /// Saves an assignment in the database.
        /// </summary>
        /// <param name="myAssignment">The assignment to store.</param>
        /// <returns>The new assignment id if the assignment is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Assignment myAssignment)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spInsertUpdateAssignment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@assignmentID", myAssignment.AssignmentId);
                myCommand.Parameters.AddWithValue("@courseID", myAssignment.CourseId);
                myCommand.Parameters.AddWithValue("@atitle", myAssignment.ATitle);
                myCommand.Parameters.AddWithValue("afilelocation", myAssignment.AFileLocation);
                myCommand.Parameters.AddWithValue("@duedate", myAssignment.DueDate);
                myCommand.Parameters.AddWithValue("@assigneddate", myAssignment.AssignedDate);
                
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
        /// Deletes an assignment from the database.
        /// </summary>
        /// <param name="assignmentID">An assignment ID.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        public static bool Delete(int assignmentID)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteAssignment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@assignmentID", assignmentID);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates an instance of assignment from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>An assignment.</returns>
        private static Assignment FillRecord(IDataRecord myRecord)
        {
            Assignment myAssignment = new Assignment();
            myAssignment.AssignmentId = myRecord.GetInt32(myRecord.GetOrdinal("AssignmentID"));
            myAssignment.ATitle = myRecord.GetString(myRecord.GetOrdinal("ATitle"));
            myAssignment.AFileLocation = myRecord.GetString(myRecord.GetOrdinal("AFileLocation"));
            myAssignment.DueDate = myRecord.GetDateTime(myRecord.GetOrdinal("DueDate"));
            myAssignment.AssignedDate = myRecord.GetDateTime(myRecord.GetOrdinal("AssignedDate"));
            return myAssignment;
        }
        #endregion
    }
}
