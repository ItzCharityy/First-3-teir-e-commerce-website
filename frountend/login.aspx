<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="st1.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="login" style="background: url('images/background-img2.png') no-repeat; background-size: cover;">
  <div class="container">
      

    <div class="row my-5 py-5">

      <div class="offset-md-3 col-md-6 my-5">
        <h2 class="display-3 fw-normal text-center">Welcome Back! <span class="text-primary">Login </span></h2>
        
          <div class="mb-3">
            <input type="email" class="form-control form-control-lg" name="email" id="loginEmail" placeholder="Email Address" runat="server">
          </div>
          <div class="mb-3">
            <input type="password" class="form-control form-control-lg" name="password" id="loginPassword" placeholder="Password"  runat="server">
          </div>
          <div class="d-grid gap-2">
              <asp:Label ID="txtMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
               <asp:Button ID="btnLogin" CssClass="btn btn-dark btn-lg rounded-1" runat="server" Text="Login" OnClick="btnLogin_Click" />
          </div>
         
      </div>
    </div>
  </div>
</section>
</asp:Content>
