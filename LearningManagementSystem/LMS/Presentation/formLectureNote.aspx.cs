using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LMS.BusinessObject.List;
using LMS.BusinessLogic;
using LMS.BusinessObject;

namespace LMS.Presentation
{
    public partial class formLectureNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Helper.ViewLectureNote(dlistLectureNote, Session["CourseID"].ToString());
            }

        }
        
        protected void ViewDownloadLectureNote(object sender, DataListCommandEventArgs e)
        {
            string filePath = ((Label)e.Item.FindControl("lblLFileLocation")).Text;
            DownloadFile(filePath, e.CommandName != "View");
        }

        /// <summary>
        /// Downloads or view the lecture notes.
        /// </summary>
        /// <param name="filePath">File path of the lecture note.</param>
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