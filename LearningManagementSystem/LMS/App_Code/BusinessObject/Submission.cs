using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Submission class represents the submission information of the assignment.
    /// </summary>
    public class Submission
    {
        #region Private Fields


        private string _fileLocation = String.Empty;
        private decimal _grade = 0;
        private DateTime _submissionDate = DateTime.Today;
        private Student _student = new Student();
        private Assignment _assignment = new Assignment();
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets an instance of the assignment.
        /// </summary>
        public Assignment Assignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }

        /// <summary>
        ///     Gets or sets an instance of the student.
        /// </summary>
        public Student Student
        {
            get { return _student; }
            set { _student = value; }
        }

  
        /// <summary>
        ///     Gets or sets the submission date of the assignment.
        /// </summary>
        public DateTime SubmissionDate
        {
            get { return _submissionDate; }
            set { _submissionDate = value; }
        }

        /// <summary>
        ///     Gets or sets the file location of the assignment.
        /// </summary>
        public string FileLocation
        {
            get { return _fileLocation; }
            set { _fileLocation = value; }
        }

        /// <summary>
        ///     Gets or sets the grade of the assignment.
        /// </summary>
        public decimal Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        #endregion
    }
}