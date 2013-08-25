using System;

namespace LMS.BusinessObject
{
    /// <summary>
    ///     The Department class represents the information about the department.
    /// </summary>
    public class Department
    {
        #region Private Fields

        private string _dLocation = String.Empty;
        private string _dName = String.Empty;
        private int _dNo = -1;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the department number of the department.
        /// </summary>
        public int DNo
        {
            get { return _dNo; }
            set { _dNo = value; }
        }

        /// <summary>
        ///     Gets or sets the department name of the department.
        /// </summary>
        public string DName
        {
            get { return _dName; }
            set { _dName = value; }
        }

        /// <summary>
        ///     Gets or sets the department location of the department.
        /// </summary>
        public string DLocation
        {
            get { return _dLocation; }
            set { _dLocation = value; }
        }

        #endregion
    }
}