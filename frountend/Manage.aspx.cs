using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class Manage : System.Web.UI.Page
    {
        // Service client to interact with your backend service
        private Service1Client client = new Service1Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductData();
            }
        }

        // Method to bind product data to the Repeater
        private void BindProductData()
        {
            var products = client.getProducts(); // Assuming this method gets all products
            ProductRepeater.DataSource = products;
            ProductRepeater.DataBind();
        }

        // Add new product
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            int stock = int.Parse(txtStock.Text);

            client.InsertNewProduct(new Product
            {
                ProductName = productName,
                Price = price,
                
            });

            // Clear the form
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";

            // Rebind the product data
            BindProductData();
        }

        // Handle Edit/Delete actions
        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit")
            {
                // Edit product logic
                Response.Redirect($"EditProduct.aspx?ProductID={productId}");
            }
            else if (e.CommandName == "Delete")
            {
                // Call service to delete the product
                client.deleteProduct(productId);
                BindProductData(); // Rebind the data after deletion
            }
        }
    }
}
