using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class AboutProduct : System.Web.UI.Page
    {
        // Triggered when the page is first loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the product ID from the query string
            string ProdId = Request.QueryString["ID"];

            // If no product ID is provided, redirect to the Home page
            if (ProdId == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                // Initialize the service client to retrieve product data
                Service1Client Sc = new Service1Client();

                // Retrieve the product by its ID
                Product Prod = Sc.getProductById(Convert.ToInt32(ProdId));

                // Display product details on the page
                this.PDescription.InnerText = Prod.Description;
                this.PImg.Src = Prod.ImageURL;
                this.PName.InnerText = Prod.ProductName;
                this.PPrice.InnerText = "R " + Prod.Price.ToString("F");

                if ((Session["isLoggedIn"] != null) && (bool)Session["isLoggedIn"])
                {
                    //show add to car when they are logged in 
                    btnAddToCart.Visible = true;
                }


                Sc.Close();

                populateReviews(Convert.ToInt32(ProdId));
            }
        }

        private void populateReviews(int id)
        {

            Service1Client Sc = new Service1Client();

            reviewPG.HRef = $"review.aspx?ID={id}";
            dynamic rew = Sc.getReviews(id);
            int i = 0;
            foreach (var r in rew)
            {
                if (i <= 3)
                    i++;
                else
                    break;
                // Generate star rating: Filled stars (★) for the rating value, empty stars (☆) for the rest
                string stars = new string('★', r.Rating) + new string('☆', 5 - r.Rating);

                review.InnerHtml += "<div class='review mb-3 border rounded p-3 ms-3' style='border: 2px solid #ccc; border-radius: 8px; background-color: #f9f9f9;'>"
                  + $"<h5 class='reviewer-name mb-2' style='font-size: 1.25rem; font-weight: bold;'>{r.UserName}</h5>"
                  + $"<p class='review-text mb-2' style='font-size: 1rem;'>{r.ReviewText}</p>"
                  + $"<p class='review-rating mb-2' style='font-size: 1rem;'>Rating: {stars}</p>"  // Display stars
                  + $"<p class='review-date text-muted' style='font-size: 0.875rem;'>Reviewed on: {r.ReviewDate.ToString("MMMM d, yyyy")}</p></div>";

            }

            Sc.Close();
        }


        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            string ProdId = Request.QueryString["ID"];
            Service1Client Sc = new Service1Client();
           
            


           
            int userId = (int)Session["UserID"];
            Service1Client client = new Service1Client();

            try
            {
                // Call the WCF service to add the product to the cart
                bool isAdded = client.addProdToCart(Convert.ToInt32(ProdId), userId, 1);

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

            Sc.Close();

        }
        // Triggered when the "Add to Wishlist" button is clicked
        protected void btnAddToWish_Click(object sender, EventArgs e)
        {
            // Check if the wishlist session variables are not initialized
            if (Session["wishCount"] == null && Session["Wishlist"] == null)
            {
                // Initialize the wishlist session
                Session["wishCount"] = 0;
                Session["Wishlist"] = new List<Product>();
            }

            // Get the product ID from the query string
            string ProdId = Request.QueryString["ID"];

            // If no product ID is provided, redirect to the Home page
            if (ProdId == null)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                // Initialize the service client and retrieve the product
                Service1Client Sc = new Service1Client();
                List<Product> tem = (List<Product>)Session["Wishlist"];
                Product Prod = Sc.getProductById(Convert.ToInt32(ProdId));

                // Check if the product is already in the wishlist
                if (!tem.Any(p => p.ProductID == Prod.ProductID))
                {
                    // Increment the wishlist count and add the product to the session
                    int Count = Convert.ToInt32(Session["wishCount"]) + 1;
                    Session["wishCount"] = Count;
                    tem.Add(Prod);
                    Session["Wishlist"] = tem;

                    // Reload the current page
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    // Product is already in the wishlist, show an alert message
                    Response.Write("<script>alert('Product is already in the wishlist!');</script>");

                    // Reload the current page
                    Response.Redirect(Request.RawUrl);
                }

                // Close the service client after use
                Sc.Close();
            }
        }

        protected void btnAddToCart_Click1(object sender, EventArgs e)
        {

        }
    }
}
