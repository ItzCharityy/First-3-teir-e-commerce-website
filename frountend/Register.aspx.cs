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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtMsg.Visible = false;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate these inputs
            string name = this.Txtname.Value;
            string secName = this.TxtsecondName.Value; // Optional
            string surname = this.Txtsurname.Value;
            string phone = this.TxtphoneNumber.Value;
            string pass1 = this.Txtpassword1.Value;
            string pass2 = this.Txtpassword2.Value;
            string mail = this.Txtemail.Value;

            // Password hashing or other logic to store passwords securely
            string passHash = Secrecy.HashPassword(pass1); // Assume you have a method for password hashing

            Service1Client SC = new Service1Client();

            // Step 1: Form Validation
            if (IsValidFormDetails(mail, name, surname, phone, pass1, pass2))
            {
                // Step 2: Register user
                dynamic IsSuccess = SC.registerUser(name, secName, surname, passHash, mail, "", phone);

                // Step 3: Handle Registration Success or Failure
                if (IsSuccess is bool && IsSuccess == true)
                {
                    Response.Redirect("login.aspx");
                    Response.Write("<script>alert('Registration successful!'); window.location='Login.aspx';</script>");
                }
                else if (IsSuccess is bool && IsSuccess == false)
                {
                    txtMsg.Visible = true;
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    txtMsg.Text = "Registration failed. Please try again.";
                }
                else if (IsSuccess is string) // User with email already exists
                {
                    txtMsg.Visible = true;
                    txtMsg.ForeColor = System.Drawing.Color.Red;
                    txtMsg.Text = IsSuccess; // Display the error message from the service
                }
            }
            else
            {
                txtMsg.Visible = true;
                txtMsg.ForeColor = System.Drawing.Color.Red;
                txtMsg.Text = "Form validation failed. Please check your inputs.";
            }
        }

       


        // Helper method to validate email
        private bool IsValidFormDetails(string mail ,string  name , string surname , string  phone ,string pass1 ,string pass2)
        {
            if (string.IsNullOrEmpty(name))
            {
                Response.Write("<script>alert('Please enter your first name.');</script>");
                return false;
            }

            if (string.IsNullOrEmpty(surname))
            {
                Response.Write("<script>alert('Please enter your surname.');</script>");
                return false;
            }

            if (string.IsNullOrEmpty(mail))
            {
                Response.Write("<script>alert('Please enter a valid email address.');</script>");
                return false;
            }

            if (string.IsNullOrEmpty(phone) || !IsValidPhone(phone))
            {
                Response.Write("<script>alert('Please enter a valid phone number.');</script>");
                return false;
            }

            if (string.IsNullOrEmpty(pass1) || string.IsNullOrEmpty(pass2))
            {
                Response.Write("<script>alert('Please enter and confirm your password.');</script>");
                return false;
            }

            if (pass1 != pass2)
            {
                Response.Write("<script>alert('Passwords do not match. Please try again.');</script>");
                return false;
            }

            return true;
        }

        // Helper method to validate phone number (basic)
        private bool IsValidPhone(string phone)
        {
            return phone.All(char.IsDigit) && (phone.Length == 9 || (phone.StartsWith("0") && phone.Length == 10)); 
        }

    }
}