using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using st1.ServiceReference1; // Assuming ServiceReference1 is the WCF service reference

namespace st1
{
    public partial class ShoppingCart : System.Web.UI.Page
    {

        int currentUserId ;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    currentUserId = (int)Session["UserID"];
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }

                BindCartData();
            }
        }


  


        
        
        private void BindCartData()
        {
            using (Service1Client client = new Service1Client())
            {
               
                int countprod = 0;
                decimal TotalPrice = 0;
                decimal vat = 0;
                decimal grandTotal = 0;

                try
                {
                    // Fetch cart items from the service
                    Product[] cartItems = client.getCartProducts(currentUserId);
                    

                    if (cartItems != null)
                    {
                        List<CartItem> tempCart = new List<CartItem>();

                        // Combine the two datasets
                        foreach (var item in cartItems)
                        {
                            int quantity = client.getCartQuntityfrom(item.ProductID);
                            CartItem cartItem = new CartItem
                            {
                                        ProductID = item.ProductID,
                                        ProductName = item.ProductName,
                                        Price = item.Price,
                                        Quantity = quantity
                            };
                            countprod+= quantity;
                            TotalPrice += cartItem.TotalPrice;
                            tempCart.Add(cartItem);
                               
                        }

                        // Bind to the Repeater
                        CartRepeater.DataSource = tempCart;
                        CartRepeater.DataBind();

                        if (Session["productCount"] == null) {
                            Session["productCount"] = 0;
                        }
                        int getSeCount = (int)Session["productCount"];
                        countprod += getSeCount;

                        Session["productCount"] = countprod;

                        Csubtotal.InnerText = TotalPrice.ToString("F");
                        vat = TotalPrice * 0.15m;
                        grandTotal = TotalPrice + vat;
                        Cvat.InnerText = vat.ToString("F");
                        CgrandTotal.InnerText = grandTotal.ToString("F");

                    }
                    else
                    {
                        // Handle the case where one of the datasets is null
                        Console.WriteLine("cartItems or extadeatail is null.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        public class CartItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            // Calculate the total in the service
            public decimal TotalPrice => Price * Quantity;
        }


        //<input type="number" class="form-control" id="Number1" value='<%# Eval("Quantity") %>' runat="server" />
        protected void Update_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.CommandArgument);

            // Find the Repeater item where the button was clicked
            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            // Find the quantity input field within that item
            TextBox quantityInput = (TextBox)item.FindControl("Number1");

            if (quantityInput != null)
            {
                int newQuantity = Convert.ToInt32(quantityInput.Text); // Get the new quantity value

                Service1Client client = new Service1Client();

                // Call the service to update the quantity with the new value
                bool isUpdated = client.updateQuntity(id, newQuantity);

                if (isUpdated)
                {
                    
                    Response.Redirect(Request.RawUrl);
                }
            }
        }


        protected void Remove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.CommandArgument);

            Service1Client client = new Service1Client();


            bool isupdated = client.removeFromCart(id);

            if (isupdated)
            {
                if (Session["productCount"] != null)
                {
                    int getSeCount = (int)Session["productCount"];
                    getSeCount--;
                    Session["productCount"] = getSeCount;
                }
                Response.Redirect(Request.RawUrl);
            }

          

        }
    }



}
