using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using st1.ServiceReference1;

namespace st1
{
    public partial class review : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Fetch the product ID from the query string
                string ProdId = Request.QueryString["ID"];
                if (ProdId == null)
                {
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    // Fetch product details and populate the page
                    Service1Client Sc = new Service1Client();
                    Product Prod = Sc.getProductById(Convert.ToInt32(ProdId));
                    this.PName.InnerText = Prod.ProductName;
                    this.PDescription.InnerText = Prod.Description;
                    this.PImg.Src = Prod.ImageURL;
                    this.PPrice.InnerText = "R " + Prod.Price.ToString("F");

                    // Load reviews for this product
                    LoadReviews(Convert.ToInt32(ProdId));

                    Sc.Close();
                }
            }
        }

        // Load reviews for the product
        private void LoadReviews(int productId)
        {
            Service1Client Sc = new Service1Client();
            dynamic reviews = Sc.getReviews(productId);

            if (reviews != null )
            {
                foreach (var r in reviews)
                {
                    string stars = new string('★', r.Rating) + new string('☆', 5 - r.Rating);
                    review_section.InnerHtml += "<div class='review'>"
                                      + $"<h5 class='reviewer-name'>{r.UserName}</h5>"
                                      + $"<p class='review-text'>{r.ReviewText}</p>"
                                      + $"<p class='review-rating'>{stars}</p>"
                                      + $"<p class='review-date text-muted'>Reviewed on: {r.ReviewDate.ToString("MMMM d, yyyy")}</p>"
                                      + "</div>";
                }
            }
            else
            {
                review_section.InnerHtml = "<p>No reviews yet.</p>";
            }

            Sc.Close();
        }

        // Handle review submission
        protected void SubmitReview_Click(object sender, EventArgs e)
        {
            // Ensure that the user is logged in
            if (Session["UserID"] == null)
            {
                Response.Write("<script>alert('You need to be logged in to submit a review.');</script>");
                return;
            }

            string ProdId = Request.QueryString["ID"];
            if (ProdId != null)
            {
                // Get the submitted review text and rating
                string reviewTextt = reviewText.Value;
                int ratingg = Convert.ToInt32(rating.Value);

                // Create a new Review object
                Review newReview = new Review
                {
                    ProductID = Convert.ToInt32(ProdId),
                    UserID = (int)Session["UserID"],  // Assuming user session has UserID stored
                    ReviewText = reviewTextt,
                    Rating = ratingg,
                    ReviewDate = DateTime.Now
                };

                // Call service to submit the review
                Service1Client Sc = new Service1Client();
                bool success = /*Sc.SubmitReview(newReview)*/ false;

                if (success)
                {
                    Response.Write("<script>alert('Review submitted successfully!');</script>");
                    // Reload the reviews
                    LoadReviews(Convert.ToInt32(ProdId));
                }
                else
                {
                    Response.Write("<script>alert('There was an error submitting your review.');</script>");
                }

                Sc.Close();
            }
        }
    }
}