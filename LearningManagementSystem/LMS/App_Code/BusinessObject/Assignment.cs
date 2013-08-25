using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Assignment class represents the information about the assignment.
    /// </summary>
    public class Assignment
    {
        #region Private Fields

        private string _aFileLocation = String.Empty;
        private string _aTitle = String.Empty;
        private DateTime _assignedDate = DateTime.Today;
        private int _assignmentId = -1;
        private string _courseId = String.Empty;
        private DateTime _dueDate = DateTime.Today;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id of the assignment.
        /// </summary>
        public int AssignmentId
        {
            get { return _assignmentId; }
            set { _assignmentId = value; }
        }

        /// <summary>
        ///     Gets or sets the id of the course.
        /// </summary>
        public string CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        // Gets or sets the title of the assignment.
        /// <summary>
        /// </summary>
        public string ATitle
        {
            get { return _aTitle; }
            set { _aTitle = value; }
        }

        // Gets or sets the file location of the assignment.
        /// <summary>
        /// </summary>
        public string AFileLocation
        {
            get { return _aFileLocation; }
            set { _aFileLocation = value; }
        }

        // Gets or sets the due date of the assignment.
        /// <summary>
        /// </summary>
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        /// <summary>
        ///     Gets or sets the assigned date of the assignment
        /// </summary>
        public DateTime AssignedDate
        {
            get { return _assignedDate; }
            set { _assignedDate = value; }
        }

        #endregion
    }
}