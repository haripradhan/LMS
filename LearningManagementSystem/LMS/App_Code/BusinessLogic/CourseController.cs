using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using LMS.BusinessObject;
using LMS.BusinessObject.List;
using LMS.DataAccess;

namespace LMS.BusinessLogic
{
    [DataObjectAttribute()]
    public class CourseController
    {

        #region Public Methods

        /// <summary>
        /// Gets an instance of Course. 
        /// </summary>
        /// <param name="courseID">The ID of the course.</param>
        /// <returns>A Course if it matches, otherwise null.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Course GetItem(string courseID)
        {
            return CourseDA.GetItem(courseID);
        }

        /// <summary>
        /// Gets a list of Courses
        /// </summary>
        /// <returns>List of Courses</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static CourseList GetItem()
        {
            return CourseDA.GetItem();
        }

        /// <summary>
        /// Gets list of courses taken by a student.
        /// </summary>
        /// <param name="id">A unique student or instructor id.</param>
        /// <param name="isStudent">Boolean flag for student. True if user is a student else false.</param>
        /// <returns>A list of courses.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static CourseList GetItem(string id, bool isStudent)
        {
            return CourseDA.GetItem(id, isStudent);
        }
        /// <summary>
        /// Saves an Course in the database.
        /// </summary>
        /// <param name="myCourse">The Course to store.</param>
        /// <returns>The new Course id if the Course is new in the database or the existing ID when an record was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert | DataObjectMethodType.Update, true)]
        public static int Save(Course myCourse)
        {
            return CourseDA.Save(myCourse);
        }

        /// <summary>
        /// Deletes an Course from the database.
        /// </summary>
        /// <param name="courseID">The ID of the course.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(string courseID)
        {
            return CourseDA.Delete(courseID);
        }

        #endregion

    }
}