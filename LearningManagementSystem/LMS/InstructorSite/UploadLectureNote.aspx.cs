using System;
using System.IO;
using AjaxControlToolkit;
using LMS.BusinessLogic;
using LMS.BusinessObject;

namespace LMS.InstructorSite
{
    public partial class UploadLectureNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
        {
            LectureNote lNote = new LectureNote();
            lNote.LectureId = -1;
            lNote.CourseId = Session["courseID"].ToString();
            //Check whether or not the title is specified.
            if (txtboxLectureTitle.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "errorTitle",
                                                   "alert('Please provide title of the lecture note!!!');", true);
            }
            else
            {
                lNote.LTitle = txtboxLectureTitle.Text;
                //Check whether or not there is valid file to upload.
                if (file.ContentType.Contains("pdf") || file.ContentType.Contains("doc")
                    || file.ContentType.Contains("rtf") || file.ContentType.Contains("docx"))
                {
                    //Append date and time in the filename to make it unique.
                    string filename = Path.GetFileNameWithoutExtension(fileuploadLectureNote.FileName) +
                                      DateTime.Now.ToString("_yyyy_mm_dd_HH_mm_ss");
                    string extension = Path.GetExtension(fileuploadLectureNote.FileName);
                    string directory = Server.MapPath("~/CourseMaterials/" + Session["courseID"] + "/LectureNote");
                    //Create directory if no folder toa save the lecture note
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    lNote.LFileLocation = directory + "/" + filename + extension;
                    fileuploadLectureNote.SaveAs(lNote.LFileLocation);
                    ClientScript.RegisterStartupScript(this.GetType(), "successUpload",
                                                       "alert('Uploaded successfully!!!');", true);
                    LectureNoteController.Save(lNote);
                    Response.Redirect("~/InstructorSite/formManageLectureNote.aspx");
                }
                else //If the file format is invalid.
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "fileFormatError",
                                                       "alert('File format error: Only pdf or ppt files are allowed!!!');",
                                                       true);
                }

            }

        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            LectureNote lNote = new LectureNote();
            lNote.LectureId = -1;
            lNote.CourseId = Session["courseID"].ToString();
            //Check whether or not the title is specified.
            if (txtboxLectureTitle.Text == String.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "errorTitle",
                                                   "alert('Please provide title of the lecture note!!!');", true);
            }
            else
            {
                lNote.LTitle = txtboxLectureTitle.Text;
                //Check whether or not there is valid file to upload.

                if (fileuploadLectureNote.HasFile)
                {
                    if (fileuploadLectureNote.PostedFile.ContentType.ToLower() == "application/pdf"
                        || fileuploadLectureNote.PostedFile.ContentType.ToLower() == "application/x-mspowerpoint")
                    {
                        //Append date and time in the filename to make it unique.
                        string filename = Path.GetFileNameWithoutExtension(fileuploadLectureNote.FileName) +
                                          DateTime.Now.ToString("_yyyy_mm_dd_HH_mm_ss");
                        string extension = Path.GetExtension(fileuploadLectureNote.FileName);
                        string directory = Server.MapPath("~/CourseMaterials/" + Session["courseID"] + "/LectureNote");
                        //Create directory if no folder toa save the lecture note
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        lNote.LFileLocation = directory + "/" + filename + extension;
                        fileuploadLectureNote.SaveAs(lNote.LFileLocation);
                        ClientScript.RegisterStartupScript(this.GetType(), "successUpload",
                                                           "alert('Uploaded successfully!!!');", true);
                        LectureNoteController.Save(lNote);
                        Response.Redirect("~/InstructorSite/formManageLectureNote.aspx");
                    }
                    else //If the file format is invalid.
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "fileFormatError",
                                                           "alert('File format error: Only pdf or ppt files are allowed!!!');",
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
