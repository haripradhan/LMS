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
    /// The CourseDA class interacts with database to retrive and store information about the course. 
    /// </summary>
    public class CourseDA
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of Course. 
        /// </summary>
        /// <param name="courseID">The ID of the course.</param>
        /// <returns>A Course if it matches, otherwise null.</returns>
        public static Course GetItem(string courseID)
        {
            Course myCourse = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectSingleCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@CourseID", courseID);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myCourse = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myCourse;
        }

        /// <summary>
        /// Gets a list of Courses
        /// </summary>
        /// <returns>List of Courses</returns>
        public static CourseList GetItem()
        {
            CourseList myCourseList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectCourseList", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myCourseList = new CourseList();
                        while (myDataReader.Read())
                        {
                            myCourseList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myCourseList;
        }

        /// <summary>
        /// Gets list of courses taken by a student.
        /// </summary>
        /// <param name="id">A unique student id.</param>
        /// <param name="isStudent">Boolean flag for student. True if user is a student else false.</param>
        /// <returns>A list of courses.</returns>
        public static CourseList GetItem(string id, bool isStudent)
        {
            CourseList myCourseList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                //SqlCommand cmd = new SqlCommand("Select * from Course where CourseID = 'cid01'",myConnection);
                string callProcedure = isStudent ?
                    "spGetEnrolledCourseListByStudent" : "spGetEnrolledCourseListByInstructor";
                SqlCommand myCommand = new SqlCommand(callProcedure, myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@id", id);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myCourseList = new CourseList();
                        while (myDataReader.Read())
                        {
                            myCourseList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myCourseList;
        }
        /// <summary>
        /// Saves an Course in the database.
        /// </summary>
        /// <param name="myCourse">The Course to store.</param>
        /// <returns>The new Course id if the Course is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(Course myCourse)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSaveCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@CourseID", myCourse.CourseId);
                myCommand.Parameters.AddWithValue("@CName", myCourse.CName);
                myCommand.Parameters.AddWithValue("@CreditHours", myCourse.CreditHours);
                myCommand.Parameters.AddWithValue("@Level", myCourse.Level);
                myCommand.Parameters.AddWithValue("@Semester", myCourse.Semester);
                myCommand.Parameters.AddWithValue("@Year", myCourse.Year);
                myCommand.Parameters.AddWithValue("@DNo", myCourse.DNo);
                myCommand.Parameters.AddWithValue("@IID", myCourse.IId);


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
        /// Deletes an Course from the database.
        /// </summary>
        /// <param name="courseID">The ID of the course.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        public static bool Delete(string courseID)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteSingleCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@CourseID", courseID);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates an instance of Course from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>An Course.</returns>
        private static Course FillRecord(IDataRecord myRecord)
        {
            Course myCourse = new Course();
            myCourse.CourseId = myRecord.GetString(myRecord.GetOrdinal("CourseID"));
            myCourse.CName = myRecord.GetString(myRecord.GetOrdinal("CName"));
            //myCourse.CreditHours = myRecord.GetFloat(myRecord.GetOrdinal("CreditHours"));
            //myCourse.Level = myRecord.GetInt32(myRecord.GetOrdinal("Level"));
            //myCourse.Semester = myRecord.GetString(myRecord.GetOrdinal("Semester"));
            //myCourse.Year = myRecord.GetString(myRecord.GetOrdinal("Year"));
            //myCourse.DNo = myRecord.GetInt32(myRecord.GetOrdinal("DNo"));
            //myCourse.IId = myRecord.GetString(myRecord.GetOrdinal("IID"));
            myCourse.Instructor.FName = myRecord.GetString(myRecord.GetOrdinal("FName"));
            myCourse.Instructor.MI = myRecord.GetString(myRecord.GetOrdinal("MI"));
            myCourse.Instructor.LName = myRecord.GetString(myRecord.GetOrdinal("LName"));
            return myCourse;
        }
        #endregion
    }
}