using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class EditProduct : System.Web.UI.Page
    {
        // Create a reference to the service client
        private Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if ProductID is in the query string
                if (Request.QueryString["ProductID"] != null)
                {
                    int productId = Convert.ToInt32(Request.QueryString["ProductID"]);
                    LoadProduct(productId);
                }
                else
                {
                    // Redirect to ManageProducts page if no ProductID is provided
                    Response.Redirect("Manage.aspx");
                }
            }
        }

        // Method to load product details for editing
        private void LoadProduct(int productId)
        {
            // Fetch product details from the service using the ProductID
            Product product = client.getProductById(productId);

            if (product != null)
            {
                // Populate form fields with product details
                txtProductName.Text = product.ProductName;
                txtPrice.Text = product.Price.ToString();
                txtStock.Text = product.StockQuantity.ToString();
                hfProductID.Value = product.ProductID.ToString(); // Store the ProductID in a hidden field
            }
            else
            {
                // Handle the case when the product is not found
                Response.Redirect("Manage.aspx");
            }
        }

        // Handle the Save Changes button click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Fetch the updated product details from the form
            int productId = Convert.ToInt32(hfProductID.Value);
            string productName = txtProductName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int stock = int.Parse(txtStock.Text);

            // Create a product object to send back to the service
            Product updatedProduct = new Product
            {
                ProductID = productId,
                ProductName = productName,
                Price = price,
                StockQuantity = stock
            };

            // Call the service to update the product
            client.editProduct(updatedProduct);

            // Redirect back to ManageProducts.aspx after saving changes
            Response.Redirect("ManageProducts.aspx");
        }
    }
}
