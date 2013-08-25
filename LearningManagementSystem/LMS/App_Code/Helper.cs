using System;
using System.Data;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;
using LMS.BusinessObject;
using LMS.BusinessObject.List;

namespace LMS.BusinessLogic
{
    public class Helper
    {

        /// <summary>
        /// Views the lecture note.
        /// </summary>
        /// <param name="dlistLectureNote">Datalist to display the lecture notes.</param>
        /// <param name="courseID">ID of the course.</param>
        public static void ViewLectureNote(DataList dlistLectureNote, string courseID)
        {
            LectureNoteList llist;
            DataTable dt = new DataTable();
            llist = LectureNoteController.GetItem(courseID);

            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "LectureNote";
            dt.Columns.Add(dc);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = Type.GetType("System.String");
            dc1.ColumnName = "LectureNoteID";
            dt.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = Type.GetType("System.String");
            dc2.ColumnName = "LFileLocation";
            dt.Columns.Add(dc2);

            DataRow dr;
            if (llist != null)
            {
                foreach (LectureNote ln in llist)
                {
                    dr = dt.NewRow();
                    dr["LectureNote"] = ln.LTitle;
                    dr["LectureNoteID"] = ln.LectureId;
                    dr["LFileLocation"] = ln.LFileLocation;
                    dt.Rows.Add(dr);
                }
            }
            dlistLectureNote.DataSource = dt;
            dlistLectureNote.DataBind();
        }


        /// <summary>
        /// Views the Assignments.
        /// </summary>
        /// <param name="dlistLectureNote">Datalist to display the assignments.</param>
        /// <param name="courseID">ID of the course.</param>
        public static void ViewAssignment(DataList dlistAssignment, string courseID)
        {
            AssignmentList alist;
            DataTable dt = new DataTable();
            alist = AssignmentController.GetItem(courseID);

            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "AFileLocation";
            dt.Columns.Add(dc);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = Type.GetType("System.String");
            dc1.ColumnName = "AssignmentID";
            dt.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = Type.GetType("System.String");
            dc2.ColumnName = "AssignmentTitle";
            dt.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn();
            dc3.DataType = Type.GetType("System.String");
            dc3.ColumnName = "DueDate";
            dt.Columns.Add(dc3);

            DataColumn dc4 = new DataColumn();
            dc4.DataType = Type.GetType("System.String");
            dc4.ColumnName = "AssignmentDate";
            dt.Columns.Add(dc4);

            DataRow dr;
            if (alist != null)
            {
                foreach (Assignment assignment in alist)
                {
                    dr = dt.NewRow();
                    dr["AFileLocation"] = assignment.AFileLocation;
                    dr["AssignmentID"] = assignment.AssignmentId;
                    dr["AssignmentTitle"] = assignment.ATitle;
                    dr["DueDate"] = assignment.DueDate.ToShortDateString();
                    dr["AssignmentDate"] = assignment.AssignedDate;
                    dt.Rows.Add(dr);
                }
            }
            dlistAssignment.DataSource = dt;
            dlistAssignment.DataBind();
        }
   


        /// <summary>
        /// Gets the content type of the file.
        /// </summary>
        /// <param name="fileExtension">File extension.</param>
        /// <returns>Content type.</returns>
        public static string GetContentType(string fileExtension)
        {
            string contentType = String.Empty;
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
            return contentType;

        }

    }
}