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
    /// <summary>
    /// The LectureNoteController class interacts with LectureNote data access layer to retrive and store information about LectureNote. 
    /// </summary>
    [DataObjectAttribute()]
    public class LectureNoteController
    {
        #region Public Methods
        /// <summary>
        /// Gets an instance of LectureNote. 
        /// </summary>
        /// <param name="lectureID">A unique ID of the lecture.</param>
        /// <returns>A LectureNote if it matches, otherwise null.</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LectureNote GetItem(int lectureID)
        {
            return LectureNoteDA.GetItem(lectureID);
        }

        /// <summary>
        /// Gets a list of LectureNotes
        /// </summary>
        /// <param name="cid">A unique course ID.</param>
        /// <returns>List of LectureNotes</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static LectureNoteList GetItem(string cid)
        {
            return LectureNoteDA.GetItem(cid);
        }

        /// <summary>
        /// Saves an LectureNote in the database
        /// </summary>
        /// <param name="myLectureNote">The LectureNote to store.</param>
        /// <returns>The new LectureNote id if the LectureNote is new in the database or the existing ID when an record was updated.</returns>
        [DataObjectMethod(DataObjectMethodType.Insert | DataObjectMethodType.Update, false)]
        public static int Save(LectureNote myLectureNote)
        {
            return LectureNoteDA.Save(myLectureNote);
        }

        /// <summary>
        /// Deletes an LectureNote from the database.
        /// </summary>
        /// <param name="lectureID">A unique lecture ID.</param>
        /// <returns>True if the assignement was successfully deleted, or false otherwise.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static bool Delete(int lectureID)
        {
            return LectureNoteDA.Delete(lectureID);
        }

        #endregion

    }
}