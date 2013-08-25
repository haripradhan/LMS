using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMS.BusinessLogic;
using LMS.BusinessObject;
using LMS.BusinessObject.List;

namespace LMS
{
    public partial class FormMyCourse : System.Web.UI.Page
    {
        
        private string id = String.Empty;
        Course c = new Course();
        CourseList clist = new CourseList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    id = (String)Session["UserName"];
                    bindCourseList(dlCourseList);
                    
                }
                

            }
        }
        
        
        private void bindCourseList(DataList dlCourseList)
        {
            clist = (Session["role"].ToString() == "Student") ? CourseController.GetItem(id, true) 
                : CourseController.GetItem(id, false);
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.String");
            dc.ColumnName = "CourseName";
            dt.Columns.Add(dc);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = Type.GetType("System.String");
            dc2.ColumnName = "InstructorName";
            dt.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn();
            dc3.DataType = Type.GetType("System.String");
            dc3.ColumnName = "ViewInfo";
            dt.Columns.Add(dc3);

            DataColumn dc4 = new DataColumn();
            dc4.DataType = Type.GetType("System.String");
            dc4.ColumnName = "CourseID";
            dt.Columns.Add(dc4);

            DataRow dr;   
            foreach (Course c1 in clist)
            {   
                dr = dt.NewRow();
                dr["CourseName"] = c1.CName;
                dr["InstructorName"] = c1.Instructor.FullName;
                dr["ViewInfo"] = "View Course Info";
                dr["CourseID"] = c1.CourseId;
                dt.Rows.Add(dr);

            }
            dlCourseList.DataSource = dt;
            dlCourseList.DataBind();
        }

        protected void SelectCourseItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName == "courseName" || e.CommandName== "ViewCourse")
            {
                string courseId = ((Label)e.Item.FindControl("lblCourseID")).Text;
                string instructorName = ((Label)e.Item.FindControl("lblInstructorName")).Text;
                string courseName = ((LinkButton)e.Item.FindControl("lbCourseName")).Text;
                if (Session["CourseID"] != null)
                {
                    Session.Remove("CourseID");
                }
                if (Session["CourseName"] != null)
                {
                    Session.Remove("CourseName");
                }
                if (Session["InstructorName"] != null)
                {
                    Session.Remove("InstructorName");
                }
                Session["CourseID"] = courseId;
                Session["InstructorName"] = instructorName;
                Session["CourseName"] = courseName;
                Response.Redirect(String.Format("~/Presentation/formHome.aspx?cid={0}", courseId));

            }

        }
    
    }
}
