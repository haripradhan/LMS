using System;
using System.IO;
using LMS.BusinessLogic;
using LMS.BusinessObject;

namespace LMS.InstructorSite
{
    public partial class UploadAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Assignment assignment = new Assignment();
            assignment.AssignmentId = -1;
            assignment.CourseId = Session["courseID"].ToString();
            //Check whether or not the title is specified.
            if (txtboxAssignmentTitle.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "errorTitle",
                                                   "alert('Please provide title of the assignment!!!');", true);
            }
            else
            {
                if (txtboxDueDate.Text == String.Empty)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorTitle",
                                                       "alert('Please provide due date of the assignment submission!!!');",
                                                       true);
                }
                else
                {
                    assignment.ATitle = txtboxAssignmentTitle.Text;
                    assignment.DueDate = Convert.ToDateTime(txtboxDueDate.Text);
                    //Check whether or not there is valid file to upload.
                    if (fileuploadAssignment.HasFile)
                    {
                        if (fileuploadAssignment.PostedFile.ContentType.ToLower() == "application/pdf"
                            || fileuploadAssignment.PostedFile.ContentType.ToLower() == "application/msword")
                        {
                            //Append date and time in the filename to make it unique.
                            string filename = Path.GetFileNameWithoutExtension(fileuploadAssignment.FileName) +
                                              DateTime.Now.ToString("_yyyy_mm_dd_HH_mm_ss");
                            string extension = Path.GetExtension(fileuploadAssignment.FileName);
                            string directory = Server.MapPath("~/CourseMaterials/" + Session["courseID"] + "/Assignment");
                            //Create directory if no folder to save the lecture note
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }
                            assignment.AFileLocation = directory + "/" + filename + extension;
                            fileuploadAssignment.SaveAs(assignment.AFileLocation);
                            ClientScript.RegisterStartupScript(this.GetType(), "successUpload",
                                                               "alert('Uploaded successfully!!!');", true);
                            AssignmentController.Save(assignment);
                            Response.Redirect("~/InstructorSite/formManageAssignment.aspx");
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
    }


}
