using System;
using System.IO;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;
using LMS.BusinessObject.List;
using System.Data;
using LMS.BusinessObject;
using LMS.Presentation;

namespace LMS.InstructorSite
{
    public partial class formManageSubmission : System.Web.UI.Page
    {
        private int assignmentId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            assignmentId = Convert.ToInt32(Request.QueryString["AssignmentID"]);
            
            if (!IsPostBack)
            {
                ViewSubmission(dlGradeAssignmentByInstructor, assignmentId);
            }

        }

        /// <summary>
        /// Views information about the assignment submission.
        /// </summary>
        /// <param name="dlistLectureNote">Data list for submission list.</param>
        /// <param name="assignmentId">ID of the assignment.</param>
        public void ViewSubmission(DataList dlistLectureNote, int assignmentId)
        {

            SubmissionList submissionList;
            DataTable dt = new DataTable();
            submissionList = SubmissionController.GetItem(assignmentId);

            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "StudentName";
            dt.Columns.Add(dc);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = Type.GetType("System.String");
            dc1.ColumnName = "ATitle";
            dt.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = Type.GetType("System.String");
            dc2.ColumnName = "SubmissionDate";
            dt.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn();
            dc3.DataType = Type.GetType("System.String");
            dc3.ColumnName = "Grade";
            dt.Columns.Add(dc3);

            DataColumn dc4 = new DataColumn();
            dc4.DataType = Type.GetType("System.String");
            dc4.ColumnName = "StudentID";
            dt.Columns.Add(dc4);

            DataColumn dc5 = new DataColumn();
            dc5.DataType = Type.GetType("System.String");
            dc5.ColumnName = "FileLocation";
            dt.Columns.Add(dc5);

            DataRow dr;
            if (submissionList != null)
            {
                foreach (Submission sb in submissionList)
                {
                    dr = dt.NewRow();
                    dr["StudentName"] = sb.Student.FullName;
                    dr["ATitle"] = sb.Assignment.ATitle;
                    dr["SubmissionDate"] = sb.SubmissionDate.ToShortDateString();
                    dr["Grade"] = sb.Grade;
                    dr["StudentID"] = sb.Student.SId;
                    dr["FileLocation"] = sb.FileLocation;
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                btnSave.Visible = false;
                lblNoAssignmentText.Text = "No files submitted by students.";
                lblNoAssignmentText.Visible = true;
            }
            dlistLectureNote.DataSource = dt;
            dlistLectureNote.DataBind();
        }


        protected void ViewDownloadFile(Object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                DownloadFile(((Label)e.Item.FindControl("lblFileLocation")).Text, true);
            }
                
        }
        
        /// <summary>
        /// Downloads or view the assignment.
        /// </summary>
        /// <param name="filePath">File path of the assignment.</param>
        /// <param name="needDownload">True if file is to be downloaded else false.</param>
        private void DownloadFile(string filePath, bool needDownload)
        {
            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string contentType = "";
            if (File.Exists(filePath))
            {
                if (fileExtension != null)
                {
                    contentType = Helper.GetContentType(fileExtension);
                }
                if (needDownload)
                {
                    Response.AppendHeader("content-disposition",
                                          "attachment; filename=" + fileName);
                }
                if (contentType != "")
                    Response.ContentType = contentType;
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "invalidFileAlert", "alert('File Not Found!!!');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SubmissionList slist = new SubmissionList();
            foreach (DataListItem item in dlGradeAssignmentByInstructor.Items)
            {
                Submission s = new Submission();
                s.Student.SId = ((Label) item.FindControl("lblStudentID")).Text;
                s.Grade = Convert.ToDecimal(((TextBox) item.FindControl("txtBoxGrade")).Text);
                s.Assignment.AssignmentId = assignmentId;
                slist.Add(s);

            }
            SubmissionController.Save(slist);
            slist.Clear();
            Response.Redirect("~/Presentation/formAssignment.aspx");
          }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Presentation/formAssignment.aspx");
        }
    }

}