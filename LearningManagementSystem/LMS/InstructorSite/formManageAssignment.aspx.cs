using System;
using System.IO;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;
using LMS.Presentation;

namespace LMS.InstructorSite
{
    public partial class formManageAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Helper.ViewAssignment(dlistAssignment, Session["CourseID"].ToString());
            }
           
        }

        protected void dlCommandItem(object sender, DataListCommandEventArgs e)
        {
            string filePath = ((Label)e.Item.FindControl("lblAFileLocation")).Text;
            if (e.CommandName == "View")
            {
                DownloadFile(filePath, true);
            }
            else
            {
                //Call to delete the assignment
                //Get the assignment ID
                Int32 assignmentID = Convert.ToInt32(((Label)e.Item.FindControl("lblAssignmentID")).Text);
                //Get file location of the Assignment.
                string filePathToDelete = ((Label)e.Item.FindControl("lblAFileLocation")).Text;

                if (SubmissionController.GetItem(assignmentID) == null)
                {
                    AssignmentController.Delete(assignmentID);
                    File.Delete(filePath);

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorDelete",
                                                       "alert('This assignment cannot be deleted!!!');",
                                                       true);
                }
                Helper.ViewAssignment(dlistAssignment, Session["CourseID"].ToString());
                

                
            }

        }

        /// <summary>
        /// Downloads the file or views the file.
        /// </summary>
        /// <param name="filePath">File Path.</param>
        /// <param name="needDownload">True if file needs to be downloaded else false.</param>
        private void DownloadFile(string filePath, bool needDownload)
        {
            string fileName = Path.GetFileName(filePath);
            string fileExtension = Path.GetExtension(filePath);
            string contentType = "";
            try
            {
                if (File.Exists(filePath))
                {
                    if (fileExtension != null)
                    {
                        //Check the type of files.
                        switch (fileExtension.ToLower())
                        {
                            case ".txt":
                                contentType = "text/plain";
                                break;

                            case ".ppt":
                            case ".pptx":
                                contentType = "Application/x-mspowerpoint";
                                break;

                            case ".htm":
                            case ".html":
                                contentType = "text/HTML";
                                break;

                            case ".doc":
                            case ".docx":
                            case ".rtf":
                                contentType = "Application/msword";
                                break;

                            case ".pdf":
                                contentType = "Application/pdf";
                                break;
                        }

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
                    //Alert if the file doesn't exist.
                    ClientScript.RegisterStartupScript(this.GetType(), "invalidFileAlert", "alert('File Not Found!!!');",
                                                       true);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/InstructorSite/UploadAssignment.aspx");
        }


    }
}