using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.BusinessObject;
using LMS.DataAccess;
using LMS.BusinessObject.List;

namespace LMS.Presentation
{
    class SubmissionController
    {
        /// <summary>
        /// Transfer submission information to submission DAL.
        /// </summary>
        /// <param name="mySubmission">The Submission to store.</param>
        /// <returns>The new ID if the Submission is new in the database or the existing ID when an record was updated.</returns>

        public static int Save(Submission mySubmission)
        {
            return SubmissionDA.Save(mySubmission);
        }

        /// <summary>
        /// Gets the submission list of the assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the assignment.</param>
        /// <returns>List of submission of the assignment.</returns>
        public static SubmissionList GetItem(int assignmentId)
        {
            return SubmissionDA.GetItem(assignmentId);
        }


        /// <summary>
        /// Gets a list of Submissions of an assignment.
        /// </summary>
        /// <param name="courseId"> ID of the course.</param>
        /// <returns>List of Submissions.</returns>

        public static SubmissionList GetItemByCourse(string courseId)
        {
            return SubmissionDA.GetItemByCourse(courseId);

        }

        /// <summary>
         /// Saves a Submission in the database.
         /// </summary>
         /// <param name="mySubmissionList">The Submissionlist to store.</param>
         /// <returns>The new ID if the Submission is new in the database or the existing ID when an record was updated.</returns>
         public static int Save(SubmissionList mySubmissionList)
         {
             return SubmissionDA.Save(mySubmissionList);
         }

         /// <summary>
         /// Gets a list of Submissions of an assignment by the student.
         /// </summary>
         /// <param name="sid"> Unique id of the student.</param>
         /// <param name="courseId">Unique id of the course.</param>
         /// <returns>List of Submissions.</returns>
         public static SubmissionList GetItem(string sid, string courseId)
         {
            return SubmissionDA.GetItem(sid, courseId);
         }


         /// <summary>
         /// Get an assignment of a student
         /// </summary>
         /// <param name="sid">Unique id of student</param>
         /// <param name="assignmentId">Particular assigment submitted</param>
         /// <returns>An instance of submission</returns>
         public static Submission GetItemByStudentAssignment(string sid, int assignmentId)
         {
             return SubmissionDA.GetItemByStudentAssignment(sid, assignmentId);
         }

    }
}
