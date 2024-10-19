<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="review.aspx.cs" Inherits="st1.review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style>
        /* General styles */
        .container {
            max-width: 1200px;
        }

        /* Left column for product details and reviews */
        .left-column {
            padding-right: 15px;
        }

        /* Right column for the "Add Your Review" form */
        .right-column {
            position: sticky;
            top: 20px;
            padding-left: 15px;
        }

        /* Review box styles */
        .review {
            border: 2px solid #ccc;
            border-radius: 8px;
            background-color: #f9f9f9;
            padding: 16px;
            margin: 16px 0;
        }

        .review .reviewer-name {
            font-size: 1.25rem;
            font-weight: bold;
        }

        .review .review-text {
            font-size: 1rem;
        }

        .review .review-rating {
            font-size: 1rem;
        }

        .review .review-date {
            font-size: 0.875rem;
            color: #6c757d;
        }

        /* Product image styling */
        .product-image {
            max-width: 100%;
            border-radius: 8px;
        }

        /* Form styles for "Add Your Review" */
        .form-label {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row">
            <!-- Left Column: Product Details and Reviews -->
            <div class="col-md-8 left-column">
                <h1 class="mb-4">Product Details</h1>

                <!-- Product Information -->
                <div class="product-details mb-5">
                    <img id="PImg" src="" class="product-image mb-3" alt="Product Image" runat="server" />
                    <h2 id="PName" runat="server"></h2>
                    <p id="PDescription" runat="server"></p>
                    <p id="PPrice" class="text-danger" runat="server"></p>
                </div>

                <!-- Review Section -->
                <h3 class="my-4">Customer Reviews</h3>
                <div id="review_section" runat="server">
                    <!-- Reviews will be inserted here dynamically -->
                </div>
            </div>

            <!-- Right Column: Add Review Form -->
            <div class="col-md-4 right-column">
                <h4 class="my-4">Add Your Review</h4>
                
                    <div class="mb-3">
                        <label for="reviewText" class="form-label">Your Review</label>
                        <textarea id="reviewText" runat="server" class="form-control" rows="4"></textarea>
                    </div>
                    <div class="mb-3">
                        <label for="rating" class="form-label">Rating</label>
                        <select id="rating" runat="server" class="form-select">
                            <option value="1">1 Star</option>
                            <option value="2">2 Stars</option>
                            <option value="3">3 Stars</option>
                            <option value="4">4 Stars</option>
                            <option value="5">5 Stars</option>
                        </select>
                    </div>
                    <button type="submit" id="btnSubmitReview" runat="server" onserverclick="SubmitReview_Click" class="btn btn-primary w-100">Submit Review</button>
               
            </div>
        </div>
    </div>
</asp:Content>
