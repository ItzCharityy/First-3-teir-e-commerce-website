<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Wishlist.aspx.cs" Inherits="st1.Wishlist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
        <div class="container my-5">
            <h2 class="text-center">My Wishlist</h2>
            <hr />
            <asp:Repeater ID="wishlistRepeater" runat="server" >
                <HeaderTemplate>
                    <div class="row">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-md-12">
                        <div class="card mb-4">
                            <div class="row g-0">
                                <!-- Left: Image -->
                                <div class="col-md-2">
                                    <img src='<%# Eval("ImageURL") %>' class="img-fluid rounded-start" alt="Product Image" style="height: 200px; width: 200px; object-fit: cover;" />
                                </div>
                                
                                <!-- Right: Product Details -->
                                <div class="col-md-8 m-0">
                                    <div class="card-body d-flex flex-column justify-content-between" style="height: 100%;">
                                        <div>
                                            <h5 class="card-title"><%# Eval("ProductName") %></h5>
                                            <p class="card-text text-primary">Price: R <%# Eval("Price") %></p>
                                        </div>
                                        
                                        <div>
                                            <a href='AboutProduct.aspx?ID=<%# Eval("ProductID") %>' class="">View Details</a>




                                            <asp:Button ID="addToCart" runat="server" Text="Add To cart" CommandArgument='<%# Eval("ProductID") %>'  CssClass="btn btn-outline-secondarr" OnClick="addToCart_Clicks" />
                                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandArgument='<%# Eval("ProductID") %>' OnClick="RemoveFromWishlist" CssClass="btn btn-danger" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <div>
                <asp:Label ID="lblNoItems" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
        </div>
  
</asp:Content>
