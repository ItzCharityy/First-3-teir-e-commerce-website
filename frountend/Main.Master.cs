using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ( Session["IsManager"] != null &&
                 Session["UserSurname"] != null &&
                 Session["UserEmail"] != null )
            {
               
                 

                bool ismag = (bool) Session["IsManager"];
                string surname =  Session["UserSurname"].ToString() ;
                string Email =  Session["UserEmail"].ToString() ;

                this.defaultNav.Visible = false;
                this.UserDetails.Visible = true;
                this.Dname.InnerText = Email;

                RisNotLogged.Visible = false;
                LNotIsLogged.Visible = false;
                Lloggedin.Visible = true;
                



                if (ismag)
                {
                    this.NavManager.Visible = true;
                    this.DType.InnerText = "Manager";
                }
                else
                {
                    this.NavCustomer.Visible = true;
                }
            }

            HandleWishList();

            


            



        }

        private void HandleWishList() 
        {
            if (Session["wishCount"] == null && Session["Wishlist"] == null)
            {
                Session["wishCount"] = 0;
                Session["Wishlist"] = new List<Product>();

                this.wishCountOff.InnerText = Session["wishCount"].ToString();
                this.addToWish2.InnerText = Session["wishCount"].ToString();
            }
            else
            {

                this.wishCountOff.InnerText = Session["wishCount"].ToString();
                this.addToWish2.InnerText = Session["wishCount"].ToString();

                List<Product> items = (List<Product>)Session["Wishlist"];
                foreach (Product i in items)
                {
                    this.WishItems.InnerHtml += "<ul class='list-group mb-3'>"
                                               + "<li class='list-group-item d-flex justify-content-between lh-sm'>"
                                               + $"<div><h6 class='my-0'>{i.ProductName}</h6>"
                                              // + $"<small class='text-body-secondary'>{i.Description}</small>"
                                               + $"</div><span class='text-body-secondary'>R {i.Price}</span></li></ul>";
                }

            }
        }


        protected void  btnLogout_Click(object sender, EventArgs e)
        {
            Session["IsManager"] = null;
            Session["UserSurname"] = null;
            Session["UserEmail"] = null;

            Session["UserID"] = null;
            Session["isLoggedIn"] = true;

            this.defaultNav.Visible = true;
            this.UserDetails.Visible = false;
            RisNotLogged.Visible = true;
            LNotIsLogged.Visible = true;
            Lloggedin.Visible = false;
            btnCart.Visible = false;

            this.NavManager.Visible = false;
            this.NavCustomer.Visible = false;


        }
    }
}