<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="st1.Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <!-- / products-carousel foodies -->
  <section id="foodies" class="my-5">
    <div class="container my-5 py-5">

      <div class="section-header d-md-flex justify-content-between align-items-center">
        <h2 class="display-3 fw-normal">Pet Foodies</h2>
        <div class="mb-4 mb-md-0">
          <p class="m-0">



            <button class="filter-button me-4  active" data-filter="*" onclick="return false;">ALL</button>
            <button class="filter-button me-4 " data-filter=".cat" onclick="return false;">CAT</button>
            <button class="filter-button me-4 " data-filter=".dog" onclick="return false;">DOG</button>
            <button class="filter-button me-4 " data-filter=".bird" onclick="return false;">BIRD</button>
             
          </p>
            
        </div>
        <div>
   
        </div>
      </div>

     <div class="isotope-container row" id="productsDiv" runat="server">

     
      </div>


    </div>
  </section>




    

<section id="clothing" class="my-5 overflow-hidden">
  <div class="container pb-5">

    <div class="section-header d-md-flex justify-content-between align-items-center mb-3">
      <h2 class="display-3 fw-normal">Pet Clothing</h2>
    </div>

    <div class="products-carousel swiper">
      <div class="isotope-container row"   id="ClothingDiv" runat="server">

       




      </div> 
    </div> 

  </div>
</section>















</asp:Content>
