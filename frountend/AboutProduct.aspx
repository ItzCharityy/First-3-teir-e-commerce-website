<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AboutProduct.aspx.cs" Inherits="st1.AboutProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .product-details {
          background-color: #f9f9f9;
        }

        .product-info {
          display: flex;
          flex-direction: column;
          justify-content: center;
        }

        .product-title {
          font-size: 2rem;
          font-weight: bold;
          margin-bottom: 1rem;
        }

        .product-price {
          font-size: 1.5rem;
          font-weight: bold;
          margin-bottom: 1rem;
        }

        .product-description {
          font-size: 1.1rem;
          margin-bottom: 2rem;
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


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="product-details my-5 py-5">
  <div class="container">
    <div class="row">
      <!-- Product Image (Left) -->
      <div class="col-lg-6 mb-4">
        <div class="product-image">
          <img src="path-to-product-image.jpg" alt="Product Image" class="img-fluid rounded" style="height: 450px; width: 100%; object-fit: cover;" id="PImg" runat="server">
        </div>
      </div>

      <!-- Product Info (Right) -->
      <div class="col-lg-6">
        <div class="product-info">
          <h1 class="product-title" id="PName" runat="server"></h1>
          <h3 class="product-price text-primary" id="PPrice" runat="server"></h3>
          <p class="product-description" id="PDescription" runat="server">
      
          </p>
          <div class="d-grid gap-2">
           
              <asp:Button ID="btnAddToCart" CssClass="btn btn-primary btn-lg" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" Visible="false" />
             <asp:Button ID="btnAddToWish" CssClass="btn btn-outline-secondary btn-lg" runat="server" Text="Add to Wishlist" OnClick="btnAddToWish_Click" />
            
          </div>
        </div>
      </div>
    </div>

    <!-- Reviews Section -->
    <div class="reviews-section mt-5" id="review" runat="server">
      <h3 class="mb-4">Customer Reviews</h3>
      <!-- Latest 3 reviews -->
     

      <!-- See More Reviews Button -->
     
    </div>
       <a href="#" class="btn btn-outline-primary" runat="server" id="reviewPG">See More Reviews</a>
  </div>
</section>

</asp:Content>
