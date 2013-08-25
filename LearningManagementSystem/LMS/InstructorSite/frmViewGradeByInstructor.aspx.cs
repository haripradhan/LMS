using System;
using System.Data;
using System.Web.UI.WebControls;
using LMS.BusinessObject;
using LMS.BusinessObject.List;
using LMS.Presentation;

namespace LMS.InstructorSite
{
    public partial class frmViewGradeByInstructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    ViewAssignmentGradeByStudent(dlGradeAssignment,(String)Session["CourseID"]);
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }


        public void DlCommandItem(object server, DataListCommandEventArgs e)
        {  
            if (e.CommandName == "StudentDetailInfo")
            {
                Response.Redirect(String.Format("~/InstructorSite/frmViewStudentGrade.aspx?sid={0}", ((Label)e.Item.FindControl("lblStudentID")).Text));
            }

        }

        /// <summary>
        /// Views grades.
        /// </summary>
        /// <param name="dList">Data list for submission list.</param>
        /// <param name="sid">Student ID.</param>
        /// <param name="courseId">Course ID.</param>
        public void ViewAssignmentGradeByStudent(DataList dList, string courseId)
        {
            DataTable dt = new DataTable();
            SubmissionList submissionList = SubmissionController.GetItemByCourse(courseId);
            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "StudentName";
            dt.Columns.Add(dc);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = Type.GetType("System.String");
            dc1.ColumnName = "AggregateGrade";
            dt.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = Type.GetType("System.String");
            dc2.ColumnName = "StudentID";
            dt.Columns.Add(dc2);

            DataRow dr;
            if (submissionList != null)
            {
                foreach (Submission sb in submissionList)
                {
                    dr = dt.NewRow();
                    dr["StudentID"] = sb.Student.SId;
                    dr["AggregateGrade"] = sb.Grade.ToString(".00");
                    dr["StudentName"] = sb.Student.FullName;
                    dt.Rows.Add(dr);
                }
            }
            dList.DataSource = dt;
            dList.DataBind();
        }
    }
}