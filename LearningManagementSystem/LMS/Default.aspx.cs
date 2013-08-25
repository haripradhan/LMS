using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLmsLogin_Click(object sender, EventArgs e)
        {
            AthenticateAndForward();
        }

        private void AthenticateAndForward()
        {

            //// Initialize FormsAuthentication (reads the configuration and gets
            //// the cookie values and encryption keys for the given application)
            FormsAuthentication.Initialize();

            FormsAuthentication.HashPasswordForStoringInConfigFile(txtboxPassword.Text, "MD5");
            //// you can use the above method for encrypting passwords to be stored in the database
            //// Execute the command
            string[] rolesdata = Roles.GetRolesForUser(txtboxUserName.Text);

            string userData = "";
            foreach (string s in rolesdata)
            {
                userData += s + ",";
            }
            userData.TrimEnd(new char[] { ',', ' ' });

            if (Membership.ValidateUser(txtboxUserName.Text, txtboxPassword.Text))
            {
                //Create a new ticket used for authentication
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                txtboxUserName.Text, // Username to be associated with this ticket
                DateTime.Now, // Date/time issued
                DateTime.Now.AddMinutes(30), // Date/time to expire
                true, // "true" for a persistent user cookie (could be a checkbox on form)
                userData, // User-data (the roles from this user record in our database)
                FormsAuthentication.FormsCookiePath); // Path cookie is valid for

                //// Hash the cookie for transport over the wire
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, // Name of auth cookie (it's the name specified in web.config)
                 hash); // Hashed ticket

                //// Add the cookie to the list for outbound response
                Response.Cookies.Add(cookie);
                Session["role"] = rolesdata[0];
                Session["Username"] = txtboxUserName.Text;
                //// Redirect to requested URL, or homepage if no previous page requested
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null) returnUrl = "FormMyCourse.aspx";

                //// Don't call the FormsAuthentication.RedirectFromLoginPage since it could
                //// replace the authentication ticket we just added...
                Response.Redirect(returnUrl);
                //FormsAuthentication.RedirectFromLoginPage(TextBox1.Text, false);
            }
            else
            {
                // Username and or password not found in our database...
                lblErrorMessage.Visible = true;
            }
       
        }
    }
}