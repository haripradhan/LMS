using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using LMS.BusinessObject.List;
using LMS.BusinessObject;
using LMS.DataAccess;

namespace LMS.BusinessLogic
{
    /// <summary>
    /// The InstructorController class is implemented to manages the information about the instructor.
    /// </summary>
    [DataObjectAttribute()]
    public class InstructorController
    {

        #region Public Methods
        
        /// <summary>
        /// Gets an instructor. 
        /// </summary>
        /// <param name="iid">A unique id of the instructor.</param>
        /// <returns>An instructor.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Instructor GetItem(string iid)
        {
            return InstructorDA.GetItem(iid);
        }

        /// <summary>
        /// Gets a list of Instructors
        /// </summary>
        /// <returns>List of Instructors</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static InstructorList GetItem()
        {
            return InstructorDA.GetItem();
        }

        /// <summary>
        /// Saves a Instructor in the database.
        /// </summary>
        /// <param name="myInstructor">The Instructor to store.</param>
        /// <returns>The new ID if the Instructor is new in the database or the existing ID when an record was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert | DataObjectMethodType.Update, true)]
        public static int Save(Instructor myInstructor)
        {
            return InstructorDA.Save(myInstructor);
        }

        /// <summary>
        /// Deletes a Instructor from the database.
        /// </summary>
        /// <param name="iid">The ID of the studen to delete.</param>
        /// <returns>True if the Instructor was successfully deleted, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(string iid)
        {
            return InstructorDA.Delete(iid);
        }

        #endregion


    }
}