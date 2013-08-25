using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Course class represents the information about the course taken by the student.
    /// </summary>
    public class Course
    {
        #region Private Fields

        private string _cName = String.Empty;
        private string _courseId = String.Empty;
        private int _dNo = -1;
        private string _iId = string.Empty;

        private Instructor _instructor = new Instructor();
        private string _semester = String.Empty;
        private string _year = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the course id of the course.
        /// </summary>
        public string CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        /// <summary>
        ///     Gets or sets the course name of the course.
        /// </summary>
        public string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        /// <summary>
        ///     Gets or sets the credit hours of the course.
        /// </summary>
        public float CreditHours { get; set; }

        /// <summary>
        ///     Gets or sets the level of the course.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///     Gets or sets the course semester.
        /// </summary>
        public string Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }

        /// <summary>
        ///     Gets or sets the course year.
        /// </summary>
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        /// <summary>
        ///     Gets or sets the department id offering the course.
        /// </summary>
        public int DNo
        {
            get { return _dNo; }
            set { _dNo = value; }
        }

        /// <summary>
        ///     Gets or sets the instructor id of the instructor teaching the course.
        /// </summary>
        public string IId
        {
            get { return _iId; }
            set { _iId = value; }
        }


        /// <summary>
        ///     Gets or sets the instructor of the instructor teaching the course.
        /// </summary>
        public Instructor Instructor
        {
            get { return _instructor; }
            set { _instructor = value; }
        }

        #endregion
    }
}