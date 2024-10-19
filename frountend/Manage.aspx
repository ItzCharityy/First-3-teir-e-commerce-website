<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="st1.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container">
            <h2 class="mt-4">Manage Products</h2>

            <!-- Form to add a new product -->
            <div class="mb-4">
                <h4>Add Product</h4>
                <div class="row">
                    <div class="col-md-4">
                        <label for="txtProductName">Product Name</label>
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label for="txtPrice">Price</label>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4">
                        <label for="txtStock">Stock</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 mt-3">
                        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="btn btn-primary" OnClick="btnAddProduct_Click" />
                    </div>
                </div>
            </div>

            <!-- Product list with Update/Delete actions -->
            <div class="mb-4">
                <h4>Product List</h4>
                <asp:Repeater ID="ProductRepeater" runat="server" OnItemCommand="ProductRepeater_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Product ID</th>
                                    <th>Product Name</th>
                                    <th>Price</th>
                                    <th>Stock</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("ProductID") %></td>
                            <td><%# Eval("ProductName") %></td>
                            <td>R <%# Eval("Price", "{0:F2}") %></td>
                            <td><%# Eval("StockQuantity") %></td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ProductID") %>' CssClass="btn btn-warning" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ProductID") %>' CssClass="btn btn-danger" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
</asp:Content>
