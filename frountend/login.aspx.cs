using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;
using HashPass;

namespace st1
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Service1Client SC = new Service1Client();

            User user = SC.UserLogIn(this.loginEmail.Value, Secrecy.HashPassword(this.loginPassword.Value));
            if(user != null)
            {

                Session["UserID"] = user.UserID;
                Session["IsManager"] = user.IsManager;
                Session["UserSurname"] = user.Surname;
                Session["UserEmail"] = user.Email;
                Session["isLoggedIn"] = true;
                Response.Redirect("Home.aspx");

            }
            else
            {
                this.txtMsg.Text = "Ivalid Email and Password Combinations ";
                this.txtMsg.Visible = true; 
            }
        }
    }
}