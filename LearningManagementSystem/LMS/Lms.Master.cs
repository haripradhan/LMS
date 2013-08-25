using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS
{
    public partial class Lms : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                lblUserName.Text = Session["Username"].ToString();
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

        }

        protected void lnkbtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void lnkbtnViewCourses_Click(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("~/FormMyCourse.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}