using System;
using System.Data;
using System.Web.UI.WebControls;
using LMS.BusinessObject;
using LMS.BusinessObject.List;
using LMS.Presentation;

namespace LMS.StudentSite
{
    public partial class frmViewStudentGrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string sid = Request.QueryString["sid"];
          
            if (!Page.IsPostBack)
            {
                if (sid != null)
                {
                    ViewAssignmentGradeByStudent(dlGradeAssignment, sid,
                                                     (String)Session["CourseID"]);
                    return;
                }

                if (Session["UserName"] != null)
                {
                    ViewAssignmentGradeByStudent(dlGradeAssignment, (String) Session["UserName"],
                                                 (String) Session["CourseID"]);
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        /// <summary>
        /// Views grades.
        /// </summary>
        /// <param name="dList">Data list for submission list.</param>
        /// <param name="sid">Student ID.</param>
        /// <param name="courseId">Course ID.</param>
        public void ViewAssignmentGradeByStudent(DataList dList, string sid, string courseId)
        {
            DataTable dt = new DataTable();
            SubmissionList submissionList = SubmissionController.GetItem(sid,courseId);

            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "ATitle";
            dt.Columns.Add(dc);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = Type.GetType("System.String");
            dc1.ColumnName = "Grade";
            dt.Columns.Add(dc1);

            DataRow dr;
            if (submissionList != null)
            {
                foreach (Submission sb in submissionList)
                {
                    dr = dt.NewRow();
                    dr["ATitle"] = sb.Assignment.ATitle;
                    dr["Grade"] = sb.Grade;
                    dt.Rows.Add(dr);
                }
            }
            dList.DataSource = dt;
            dList.DataBind();
        }

    }
}