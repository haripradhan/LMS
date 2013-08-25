using System;
using LMS.BusinessObject.List;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Instructor class represents the information about the instructor.
    /// </summary>
    public class Instructor
    {
        #region Private Fields

        private AssignmentList _assignments = new AssignmentList();
        private string _city = String.Empty;
        private CourseList _courses = new CourseList();
        private string _fName = String.Empty;
        private string _iId = String.Empty;
        private string _lName = String.Empty;
        private string _mI = String.Empty;
        private string _password = String.Empty;
        private string _state = String.Empty;
        private string _street = String.Empty;
        private string _zipcode = String.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the unique SID of the instructor.
        /// </summary>
        public string IId
        {
            get { return _iId; }
            set { _iId = value; }
        }

        /// <summary>
        ///     Gets or sets the password of the instructor.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        ///     Gets or sets the first name of the instructor.
        /// </summary>
        public string FName
        {
            get { return _fName; }
            set { _fName = value; }
        }

        /// <summary>
        ///     Gets or sets the middle initial of the instructor.
        /// </summary>
        public string MI
        {
            get { return _mI; }
            set { _mI = value; }
        }

        /// <summary>
        ///     Gets or sets the last name of the instructor.
        /// </summary>
        public string LName
        {
            get { return _lName; }
            set { _lName = value; }
        }

        /// <summary>
        ///     Gets the full name of the instructor (FirstName + MI + LastName)
        /// </summary>
        public string FullName
        {
            get
            {
                string fullName = _fName;
                if (!String.IsNullOrEmpty(_mI))
                {
                    fullName += " " + _mI;
                }
                fullName += " " + _lName;
                return fullName;
            }
        }


        /// <summary>
        ///     Gets or sets the Street name of the instructor's address.
        /// </summary>
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }


        /// <summary>
        ///     Gets or sets the city of the instructor's address.
        /// </summary>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        ///     Gets or sets the state of the instructor's address.
        /// </summary>
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        ///     Gets or sets the zip code of the instructor's address.
        /// </summary>
        public string Zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }

        /// <summary>
        ///     Gets the address of the instructor (street, city, state, zipcode)
        /// </summary>
        public string Address
        {
            get
            {
                string address = _street + ", " + _city + ", " + _state;
                if (!String.IsNullOrEmpty(_zipcode))
                {
                    address += ", " + _zipcode;
                }
                return address;
            }
        }

        /// <summary>
        ///     Gets or sets a list of courses taught by the instructor.
        /// </summary>
        public CourseList Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }

        /// <summary>
        ///     Gets or sets a list of assignments assigned by the instructor.
        /// </summary>
        public AssignmentList Assignments
        {
            get { return _assignments; }
            set { _assignments = value; }
        }

        #endregion
    }
}