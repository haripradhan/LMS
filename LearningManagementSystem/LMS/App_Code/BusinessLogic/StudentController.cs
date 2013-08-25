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
    public class StudentController
    {

        #region Public Methods
        /// <summary>
        /// Gets an Student. 
        /// </summary>
        /// <param name="sid">A unique id of the Student.</param>
        /// <returns>An Student.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Student GetItem(string sid)
        {
            return StudentDA.GetItem(sid);
        }

        /// <summary>
        /// Gets a list of Students
        /// </summary>
        /// <returns>List of Students</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static StudentList GetItem()
        {
            return StudentDA.GetItem();
        }

        /// <summary>
        /// Saves a Student in the database.
        /// </summary>
        /// <param name="myStudent">The Student to store.</param>
        /// <returns>The new ID if the Student is new in the database or the existing ID when an record was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert | DataObjectMethodType.Update, true)]
        public static int Save(Student myStudent)
        {
            return StudentDA.Save(myStudent);
        }

        /// <summary>
        /// Deletes a Student from the database.
        /// </summary>
        /// <param name="sid">The ID of the studen to delete.</param>
        /// <returns>True if the Student was successfully deleted, or false otherwise.</returns>
       [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(string sid)
        {
            return StudentDA.Delete(sid);
        }

        #endregion

    }
}