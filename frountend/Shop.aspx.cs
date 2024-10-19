using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class Shop : System.Web.UI.Page
    {
        List<int> idProd = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        // Method to load products dynamically
        private void LoadProducts()
        {
            Service1Client SC = new Service1Client();
            string DogTags = MakeProductTags(SC.getProductsByType("Dog"));
            string ClothesTags = MakeProductTags(SC.getProductsByType("Clothes"));
            string CatTags = MakeProductTags(SC.getProductsByType("Cat"));
            string BirdTags = MakeProductTags(SC.getProductsByType("Bird"));

          
            productsDiv.InnerHtml += DogTags;

            productsDiv.InnerHtml += CatTags;
            productsDiv.InnerHtml += BirdTags;
            ClothingDiv.InnerHtml = ClothesTags;

            SC.Close();
        }

        // Dynamically generate product tags with ASP.NET Button controls
        private string MakeProductTags(dynamic products)
        {
            string tempStr = "";


            foreach (var prod in products)
            {
                idProd.Add(prod.ProductID);
                tempStr += $"<div class='item {prod.Type.ToLower()} col-md-4 col-lg-3 my-4'>"
                           + "<div class='card position-relative'>"
                           + $"<a href='AboutProduct.aspx?ID={prod.ProductID}'><img src='{prod.ImageURL}' class='img-fluid rounded-4' alt='image' style='height: 197.17px; width: 216px;'></a>"
                           + "<div class='card-body p-0'>"
                           + $"<a href='AboutProduct.aspx?ID={prod.ProductID}'>"
                           + $"<h3 class='card-title pt-4 m-0'>{prod.ProductName}</h3>"
                           + "</a><div class='card-text'>"
                           + $"<h3 class='secondary-font text-primary'>R {prod.Price}</h3>"
                           + "<div class='d-flex flex-wrap mt-3'>"
                           //+ $"<a href='AboutProduct.aspx?ID={prod.ProductID}' class='btn-cart me-3 px-4 pt-3 pb-3'>"
                           //+ "</a>"
                          // + $"<a href='AboutProduct.aspx?ID={prod.ProductID}' class='btn-cart me-3 px-4 pt-3 pb-3'>"
                          // + "<iconify-icon icon='fluent:heart-28-filled' class='fs-5'></iconify-icon></a>"
                           + "</div></div></div></div></div>";
                //<h5 class='text-uppercase m-0'>Add to Cart</h5>
            }

            return tempStr;
        }


        protected void AddWishlist(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Product is already in the wishlist!');</script>");
        }

      

        private void LoadDynamicControls()
        {
            // Example: Dynamically create and add buttons
            foreach (int i in idProd)
            {
                Button dynamicButton = new Button
                {
                    ID = "DynamicButton_" + i,
                    Text = "Button " + i,
                    CommandArgument = i.ToString(),
                    CssClass = "btn btn-primary"
                };

                // Attach an event handler
                dynamicButton.Click += DynamicButton_Click;

                // Add the button to the PlaceHolder
                //ControlsPlaceHolder.Controls.Add(dynamicButton);

               




            }
        }

        protected void DynamicButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string commandArgument = btn.CommandArgument;

            // Handle button click event
            Response.Write($"<script>alert('You clicked on Button {commandArgument}!');</script>");
        }





        // Logic to handle adding the product to the wishlist
        private void AddProductToWishlist(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return;

            // Check if wishlist exists in session, otherwise initialize it
            if (Session["Wishlist"] == null)
            {
                Session["Wishlist"] = new List<Product>();
            }

            Service1Client SC = new Service1Client();
            Product prod = SC.getProductById(Convert.ToInt32(productId));
            List<Product> wishlist = (List<Product>)Session["Wishlist"];

            // Add product to wishlist if not already present
            if (!wishlist.Any(p => p.ProductID == prod.ProductID))
            {
                wishlist.Add(prod);
                Response.Write("<script>alert('Product added to wishlist!');</script>");
            }
            else
            {
                Response.Write("<script>alert('Product is already in the wishlist!');</script>");
            }

            SC.Close();
        }
    }
}
