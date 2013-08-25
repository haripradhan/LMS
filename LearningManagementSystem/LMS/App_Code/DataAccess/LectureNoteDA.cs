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
    /// The LectureNoteDA class interacts with database to retrive and store information about LectureNote. 
    /// </summary>
    public class LectureNoteDA
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of LectureNote. 
        /// </summary>
        /// <param name="lectureID">A unique ID of the lecture.</param>
        /// <returns>A LectureNote if it matches, otherwise null.</returns>
        public static LectureNote GetItem(int lectureID)
        {
            LectureNote myLectureNote = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spSelectSingleLectureNote", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@lid", lectureID);

                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.Read())
                    {
                        myLectureNote = FillRecord(myDataReader);
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myLectureNote;
        }

        /// <summary>
        /// Gets a list of LectureNotes
        /// </summary>
        /// <param name="cid">A unique course ID.</param>
        /// <returns>List of LectureNotes</returns>
        public static LectureNoteList GetItem(string courseID)
        {
            LectureNoteList myLectureNoteList = null;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spGetLectureNoteByCourse", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@courseID", courseID);
                myConnection.Open();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    if (myDataReader.HasRows)
                    {
                        myLectureNoteList = new LectureNoteList();
                        while (myDataReader.Read())
                        {
                            myLectureNoteList.Add(FillRecord(myDataReader));
                        }
                    }
                    myDataReader.Close();
                }
                myConnection.Close();
            }
            return myLectureNoteList;
        }

        /// <summary>
        /// Saves an LectureNote in the database.
        /// </summary>
        /// <param name="myLectureNote">The LectureNote to store.</param>
        /// <returns>The new LectureNote id if the LectureNote is new in the database or the existing ID when an record was updated.</returns>
        public static int Save(LectureNote myLectureNote)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spInsertUpdateLectureNote", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@courseID", myLectureNote.CourseId);
                myCommand.Parameters.AddWithValue("@ltitle", myLectureNote.LTitle);
                myCommand.Parameters.AddWithValue("@lfilelocation", myLectureNote.LFileLocation);
                
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
        /// Deletes an LectureNote from the database.
        /// </summary>
        /// <param name="lectureID">A unique lecture ID.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        public static bool Delete(int lectureID)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppSettings.ConnectionString))
            {
                SqlCommand myCommand = new SqlCommand("spDeleteLectureNote", myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@lectureID", lectureID);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            return result > 0;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Creates an instance of LectureNote from the data record in the database.
        /// </summary>
        /// <param name="myRecord">Single row of record.</param>
        /// <returns>An LectureNote.</returns>
        private static LectureNote FillRecord(IDataRecord myRecord)
        {
            LectureNote myLectureNote = new LectureNote();
            //myLectureNote.CourseId = myRecord.GetString(myRecord.GetOrdinal("CourseID"));
            myLectureNote.LectureId = myRecord.GetInt32(myRecord.GetOrdinal("LectureID"));
            myLectureNote.LTitle = myRecord.GetString(myRecord.GetOrdinal("LTitle"));
            myLectureNote.LFileLocation = myRecord.GetString(myRecord.GetOrdinal("LFileLocation"));
            return myLectureNote;
        }
        #endregion
    }
}