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
    public class AssignmentController
    {
        #region Public Methods

        /// <summary>
        /// Gets an instance of Assignment. 
        /// </summary>
        /// <param name="assignmentID">A unique ID of assignment.</param>
        /// <returns>An Assignment if it matches, otherwise null.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Assignment GetItem(int assignmentID)
        {
            return AssignmentDA.GetItem(assignmentID);
        }

        /// <summary>
        /// Gets a list of Assignments
        /// </summary>
        /// <returns>List of Assignments</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static AssignmentList GetItem(string courseID)
        {
            return AssignmentDA.GetItem(courseID);
        }

        /// <summary>
        /// Saves an assignment in the database.
        /// </summary>
        /// <param name="myAssignment">The assignment to store.</param>
        /// <returns>The new assignment id if the assignment is new in the database or the existing ID when an record was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Update | DataObjectMethodType.Insert, true)]

        public static int Save(Assignment myAssignment)
        {
            return AssignmentDA.Save(myAssignment);
        }

        /// <summary>
        /// Deletes an assignment from the database.
        /// </summary>
        /// <param name="assignmentID">An assignment ID.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(int assignmentID)
        {
            return AssignmentDA.Delete(assignmentID);
        }
        #endregion
    }
}