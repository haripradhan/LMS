using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The LectureNote class represents the information about the lecture notes.
    /// </summary>
    public class LectureNote
    {
        #region Private Fields

        private string _courseId = String.Empty;
        private string _lFileLocation = String.Empty;
        private string _lTitle = String.Empty;
        private int _lectureId = -1;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id of the lecture note.
        /// </summary>
        public int LectureId
        {
            get { return _lectureId; }
            set { _lectureId = value; }
        }

        /// <summary>
        ///     Gets or sets the id of the course.
        /// </summary>
        public string CourseId
        {
            get { return _courseId; }
            set { _courseId = value; }
        }

        /// <summary>
        ///     Gets or sets the title of the lecture note.
        /// </summary>
        public string LTitle
        {
            get { return _lTitle; }
            set { _lTitle = value; }
        }

        /// <summary>
        ///     Gets or sets the file location of the lecture note.
        /// </summary>
        public string LFileLocation
        {
            get { return _lFileLocation; }
            set { _lFileLocation = value; }
        }

        #endregion
    }
}