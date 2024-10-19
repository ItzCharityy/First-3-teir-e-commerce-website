<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="st1.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
            <h2>Edit Product</h2>

            <div class="form-group">
                <label for="txtProductName">Product Name</label>
                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPrice">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtStock">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:HiddenField ID="hfProductID" runat="server" />

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
</asp:Content>
