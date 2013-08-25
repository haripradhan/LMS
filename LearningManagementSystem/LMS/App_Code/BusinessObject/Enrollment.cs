using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Enrollment class represents the information about the enrollment of the student for courses.
    /// </summary>
    public class Enrollment
    {
        #region Private Fields

        private string _courseID = String.Empty;
        private string _sID = String.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ID of the student.
        /// </summary>
        public string SId
        {
            get { return _sID; }
            set { _sID = value; }
        }

        /// <summary>
        ///     Gets or sets the ID of the course, the student is enrolled.
        /// </summary>
        public string CourseId
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        #endregion
    }
}