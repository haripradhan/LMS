using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;
using LMS.BusinessObject;
using LMS.BusinessObject.List;

namespace LMS.Presentation
{
    public partial class formAssignmentDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sid = Session["UserName"].ToString();
            int assignmentId = Convert.ToInt32(Request.QueryString["AssignmentID"]);
            Submitted(sid, assignmentId);
        }

        private void Submitted(string sid, int assignmentId)
        {
            Submission stdAssignmentSubmission = null;
            stdAssignmentSubmission = SubmissionController.GetItemByStudentAssignment(sid, assignmentId);
            if (stdAssignmentSubmission != null)
            {
                lblFileLocation.Text = stdAssignmentSubmission.FileLocation;
                lbtnAssignment.Text = stdAssignmentSubmission.Assignment.ATitle;
                lblSubmittedDate.Text = stdAssignmentSubmission.SubmissionDate.ToLongDateString();
                lblGrade.Text = stdAssignmentSubmission.Grade.ToString();
            }
        }

        protected void ViewDownloadFile(Object sender, EventArgs e)
        {
            DownloadFile(lblFileLocation.Text,true);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sname = Session["Username"].ToString();
            int assignmentId = Convert.ToInt32(Request.QueryString["AssignmentID"]);

            Submission studentassignment = new Submission();
            studentassignment.Student.SId = sname;
            studentassignment.Assignment.AssignmentId = assignmentId;
            studentassignment.SubmissionDate = DateTime.Now;

            if (fileuploadSubmission.HasFile)
            {
                if (fileuploadSubmission.PostedFile.ContentType.ToLower() == "application/pdf"
                    || fileuploadSubmission.PostedFile.ContentType.ToLower() == "application/msword")
                {
                    
                    string filename = sname;
                    string extension = Path.GetExtension(fileuploadSubmission.FileName);
                    string directory = Server.MapPath("~/CourseMaterials/" + Session["courseID"] + "/Assignment/Assignment" + assignmentId);
                    //Create directory if no folder to save the lecture note
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    studentassignment.FileLocation = directory + "/" + filename + extension;
                    fileuploadSubmission.SaveAs(studentassignment.FileLocation);
                    ClientScript.RegisterStartupScript(this.GetType(), "successUpload",
                                                       "alert('Uploaded successfully!!!');", true);
                    SubmissionController.Save(studentassignment);
                    Response.Redirect(String.Format("~/Presentation/formAssignmentDetail.aspx?AssignmentID={0}", assignmentId));
                }
                else  //If the file format is invalid.
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fileFormatError",
                                                       "alert('File format error: Only pdf or doc files are allowed!!!');",
                                                       true);
                }

            }
            else //If the path doesn't contain any file
            {
                ClientScript.RegisterStartupScript(this.GetType(), "failUpload",
                                                   "alert('Please specify the file to upload.!!!');", true);
            }
        }
    }
}