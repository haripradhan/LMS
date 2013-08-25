using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;

namespace LMS.Presentation
{
    public partial class formAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Helper.ViewAssignment(dlistAssignment, Session["CourseID"].ToString());
            }

        }

        protected void ViewDownloadAssignment(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "GoToAssignment")
            {
                string nextUrl = String.Empty;

                string temp = Session["role"].ToString();

                if (Session["role"].ToString() == "Student")
                {
                    nextUrl = String.Format("~/Presentation/formAssignmentDetail.aspx?AssignmentID={0}",
                                            ((Label) e.Item.FindControl("lblAssignmentID")).Text);
                    
                }
                else if (Session["role"].ToString() == "Instructor")
                {
                    nextUrl = String.Format("~/InstructorSite/formManageSubmission.aspx?AssignmentID={0}",
                                            ((Label)e.Item.FindControl("lblAssignmentID")).Text);
                    
                }
                Response.Redirect(nextUrl);
            }
            else
            {
                string filePath = ((Label)e.Item.FindControl("lblAFileLocation")).Text;
                if (e.CommandName == "View")
                {
                    DownloadFile(filePath, false);
                }
                else
                {
                    DownloadFile(filePath, true);
                }

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

    }
}