using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS
{
    public partial class MyCourse : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CourseName"] != null && Session["InstructorName"] != null)
            {
                lblCrName.Text = Session["CourseName"].ToString();
                lblCrInstructor.Text = Session["InstructorName"].ToString();
            }
            else
            {
                Response.Redirect("~/FormMyCourse.aspx");
            }
        }
    }
}