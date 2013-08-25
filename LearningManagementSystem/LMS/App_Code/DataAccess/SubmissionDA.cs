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
    /// The SubmissionDA class interacts with database to retrive and store information about Submission. 
    /// </summary>
    public class SubmissionDA
    {

        #region Public Methods

        /// <summary>
        /// Gets a list of Submissions of an assignment by the student.
        /// </summary>
        /// <param name="sid"> Unique id of the student.</param>
        /// <param name="courseId">Unique id of the course.</param>
        /// <returns>List of Submissions.</returns>
        public static SubmissionList GetItem(string sid, string courseId)
        {
            SubmissionList mySubmissionList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetGradeGroupByAssignmentForStudent", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@SID", sid);
                myCommand.Parameters.AddWithValue("@CourseID", courseId);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        mySubmissionList = new SubmissionList();
                        while (myDataReader.Read())
                        {
                            mySubmissionList.Add(FillRecordByStudent(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return mySubmissionList;
        }

        /// <summary>
        /// Gets an instance of Submission of an assignment by a student
        /// </summary>
        /// <param name="sid">Unique id of a student</param>
        /// <param name="assignmentId">Particular assignment</param>
        /// <returns>A Submission if IDs match, otherwise null</returns>
        public static Submission GetItemByStudentAssignment(string sid, int assignmentId)
        {
            Submission mySubmission = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetSubmissionByStudentAssignment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@sid", sid);
                myCommand.Parameters.AddWithValue("@assignmentID", assignmentId);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        mySubmission = FillRecordByStudentAssignment(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }

            return mySubmission;
        }

        /// <summary>
        /// Gets a list of Submissions of an assignment.
        /// </summary>
        /// <param name="courseId"> ID of the course.</param>
        /// <returns>List of Submissions.</returns>

        public static SubmissionList GetItemByCourse(string courseId)
        {
            SubmissionList mySubmissionList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetGradeGroupByCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@courseID", courseId);
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        mySubmissionList = new SubmissionList();
                        while (myDataReader.Read())
                        {
                            mySubmissionList.Add(FillRecordByCourse(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return mySubmissionList;
        }

        /// <summary>
        /// Gets a list of Submissions of an assignment.
        /// </summary>
        /// <param name="assignmentId"> ID of an assignment.</param>
        /// <returns>List of Submissions.</returns>

        public static SubmissionList GetItem(int assignmentId)
        {
            SubmissionList mySubmissionList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetSubmissionAssignmentByAssignment", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@assignmentID", assignmentId);
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        mySubmissionList = new SubmissionList();
                        while (myDataReader.Read())
                        {
                            mySubmissionList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return mySubmissionList;
        }


        /// <summary>
        /// Saves a Submission in the database.
        /// </summary>
        /// <param name="mySubmissionList">The Submissionlist to store.</param>
        /// <returns>The new ID if the Submission is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(SubmissionList mySubmissionList)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spUpdateAssignmentSubmissionGrade", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                foreach (Submission mySubmission in mySubmissionList)
                {
                    myCommand.Parameters.AddWithValue("@sid", mySubmission.Student.SId);
                    myCommand.Parameters.AddWithValue("@assignmentID", mySubmission.Assignment.AssignmentId);
                    myCommand.Parameters.AddWithValue("@grade", mySubmission.Grade);

                    //DbParameter retValue = myCommand.CreateParameter();
                    //retValue.Direction = ParameterDirection.ReturnValue;
                    //myCommand.Parameters.Add(retValue);
                    myCommand.ExecuteNonQuery();
                    //result = Convert.ToInt32(retValue.Value);    

                    myCommand.Parameters.Clear();
                }

                myConnection.Close();
            }
            return result;
        }


        /// <summary>
        /// Saves a Submission in the database.
        /// </summary>
        /// <param name="mySubmission">The Submission to store.</param>
        /// <returns>The new ID if the Submission is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Submission mySubmission)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spInsertUpdateAssignmentSubmissionGrade", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@sid", mySubmission.Student.SId);
                myCommand.Parameters.AddWithValue("@assignmentID", mySubmission.Assignment.AssignmentId);
                myCommand.Parameters.AddWithValue("@submissiondate", mySubmission.SubmissionDate);
                myCommand.Parameters.AddWithValue("@filelocation", mySubmission.FileLocation);
                myCommand.Parameters.AddWithValue("@grade", mySubmission.Grade);


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
        /// Deletes a Submission from the database.
        /// </summary>
        /// <param name="sid">The ID of the studen to delete.</param>
        /// <returns>True if the Submission was successfully deleted, or false otherwise.</returns>
        public static bool Delete(string iid)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteSingleSubmission", myConnection);
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
        /// Creates an instance of Submission from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A Submission</returns>
        private static Submission FillRecordByStudent(IDataRecord myRecord)
        {
            return new Submission
                {
                    Assignment =
                        {
                            ATitle = myRecord.GetString(myRecord.GetOrdinal("ATitle"))
                        },
                    Grade = myRecord.GetDecimal(myRecord.GetOrdinal("Grade"))

                };

        }

        /// <summary>
        /// Creates an instance of Submission from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A Submission</returns>
        private static Submission FillRecordByStudentAssignment(IDataRecord myRecord)
        {
            return new Submission
            {
                Student =
                {
                    SId = myRecord.GetString(myRecord.GetOrdinal("SID")),

                },
                Assignment =
                    {
                        AssignmentId = myRecord.GetInt32(myRecord.GetOrdinal("AssignmentID")),
                        ATitle = myRecord.GetString(myRecord.GetOrdinal("ATitle"))
                    },
                SubmissionDate = myRecord.GetDateTime(myRecord.GetOrdinal("SubmissionDate")),
                FileLocation = myRecord.GetString(myRecord.GetOrdinal("FileLocation")),
                Grade = myRecord.GetDecimal(myRecord.GetOrdinal("Grade")),

            };

        }


        /// <summary>
        /// Creates an instance of Submission from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A Submission</returns>
        private static Submission FillRecordByCourse(IDataRecord myRecord)
        {
            return new Submission
            {
                Student =
                {
                    SId = myRecord.GetString(myRecord.GetOrdinal("SID")),
                    FName = myRecord.GetString(myRecord.GetOrdinal("FName")),
                    MI = myRecord.GetString(myRecord.GetOrdinal("MI")),
                    LName = myRecord.GetString(myRecord.GetOrdinal("Lname"))
                },

                Grade = myRecord.GetDecimal(myRecord.GetOrdinal("Grade"))
            };

        }


        /// <summary>
        /// Creates an instance of Submission from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>A Submission</returns>
        private static Submission FillRecord(IDataRecord myRecord)
        {
            return new Submission
                {
                    Student =
                        {
                            SId = myRecord.GetString(myRecord.GetOrdinal("SID")),
                            FName = myRecord.GetString(myRecord.GetOrdinal("FName")),
                            MI = myRecord.GetString(myRecord.GetOrdinal("MI")),
                            LName = myRecord.GetString(myRecord.GetOrdinal("Lname"))
                        },
                    Assignment =
                        {
                            ATitle =  myRecord.GetString(myRecord.GetOrdinal("ATitle"))
                        },
                    SubmissionDate = myRecord.GetDateTime(myRecord.GetOrdinal("SubmissionDate")),
                    FileLocation = myRecord.GetString(myRecord.GetOrdinal("FileLocation")),
                    Grade = myRecord.GetDecimal(myRecord.GetOrdinal("Grade"))
                };

        }

        #endregion
    }
}