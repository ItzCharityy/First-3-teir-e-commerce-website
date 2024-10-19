using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class Wishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load the wishlist when the page is loaded for the first time
                LoadWishlist();
            }
        }

        // Load wishlist items from the session and bind them to the Repeater
        private void LoadWishlist()
        {
            // Check if the Wishlist session and wishCount session exist
            if (Session["Wishlist"] != null && Session["wishCount"] != null)
            {
                List<Product> wishlist = (List<Product>)Session["Wishlist"];

                // If there are items in the wishlist, bind them to the Repeater
                if (wishlist.Count > 0)
                {
                    wishlistRepeater.DataSource = wishlist;
                    wishlistRepeater.DataBind();
                }
                else
                {
                    // If the wishlist is empty, show a message
                    lblNoItems.Text = "Your wishlist is empty.";
                    lblNoItems.Visible = true;
                    wishlistRepeater.Visible = false;
                }
            }
            else
            {
                // If the wishlist session doesn't exist, show the empty message
                lblNoItems.Text = "Your wishlist is empty.";
                lblNoItems.Visible = true;
                wishlistRepeater.Visible = false;
            }
        }

        // Event handler to remove an item from the wishlist
        protected void RemoveFromWishlist(object sender, EventArgs e)
        {
            // Get the Button control that triggered the event and the Product ID from the CommandArgument
            Button btnRemove = (Button)sender;
            int productId = Convert.ToInt32(btnRemove.CommandArgument);

            // Retrieve the wishlist from the session
            List<Product> wishlist = (List<Product>)Session["Wishlist"];

            // Find the product to remove using its ProductID
            Product productToRemove = wishlist.Find(p => p.ProductID == productId);

            // If the product is found, remove it from the wishlist
            if (productToRemove != null)
            {
                wishlist.Remove(productToRemove);

                // Update the session with the modified wishlist
                Session["Wishlist"] = wishlist;

                // Update the wishCount session variable
                Session["wishCount"] = wishlist.Count;

                // Reload the wishlist after removal
                LoadWishlist();
                Response.Redirect(Request.RawUrl);
            }
        }

        // Optionally, if you want to format the Repeater's ItemDataBound event for more control
        protected void wishlistRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Ensure the Repeater is binding correctly to each item
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Product product = (Product)e.Item.DataItem;

                // Find and set the controls inside the Repeater, if necessary
                Button btnRemove = (Button)e.Item.FindControl("btnRemove");
                btnRemove.CommandArgument = product.ProductID.ToString();
            }
        }

        //OnItemCommand="wishlistRepeater_ItemCommand"

        
        protected void addToCart_Clicks(object sender, EventArgs e)
        {

                    Button addToCart = (Button)sender;
                     int productId = Convert.ToInt32(addToCart.CommandArgument);
           
                    // Assuming a hardcoded user ID for demonstration
                    int userId = 2; // You should replace this with the logged-in user's ID
                    Service1Client client = new Service1Client();

                    try
                    {
                        // Call the WCF service to add the product to the cart
                        bool isAdded = client.addProdToCart(productId, userId, 1);

                        if (isAdded)
                        {
                            // Success message or update the UI accordingly
                            Response.Write("<script>alert('Product added to cart successfully!');</script>");
                        }
                        else
                        {
                            // Handle failure case
                            Response.Write("<script>alert('Failed to add the product to cart.');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions
                        Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
                    }
                    finally
                    {
                        // Close the WCF client to avoid resource leaks
                        if (client != null)
                        {
                            client.Close();
                        }
                    }

                    RemoveFromWishlist( sender,  e);
                    Response.Redirect("ShoppingCart.aspx");


        }

       
    }
}
