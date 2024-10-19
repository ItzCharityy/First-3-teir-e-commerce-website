using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserData(); // Bind the users on initial page load
            }
        }

        // Method to bind user data to the Repeater
        private void BindUserData()
        {
            User[] users = client.getAllUsers(); // Fetch the users from the service
            UserRepeater.DataSource = users;
            UserRepeater.DataBind();
        }

        // Handle Repeater commands for changing roles and deleting users
        protected void UserRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "ChangeRole")
            {
                // Find the DropDownList with roles in the current row
                DropDownList ddlRoles = (DropDownList)e.Item.FindControl("ddlRoles");
                string newRole = ddlRoles.SelectedValue;

                // Call the service to change the user's role
                bool isRoleChanged = client.ChangeUserRole(userId);

                if (isRoleChanged)
                {
                    // Reload the user data after the role is changed
                    BindUserData();
                }
                else
                {
                    // Handle the error (display a message, etc.)
                    // e.g., Display an error message or log it
                }
            }
            else if (e.CommandName == "Delete")
            {
                // Call the service to delete the user
                bool isDeleted = client.DeleteUser(userId);

                if (isDeleted)
                {
                    // Reload the user data after the user is deleted
                    BindUserData();
                }
                else
                {
                    // Handle the error (display a message, etc.)
                }
            }
        }
    }
}