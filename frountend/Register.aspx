<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="st1.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        /* Register Section Styling */
        #register {
          background-position: center;
          background-size: cover;
          display: flex;
          align-items: center;
        }

        input[type="text"],
        input[type="email"],
        input[type="password"],
        input[type="tel"] {
          border: 2px solid #ccc;
          padding: 10px;
          font-size: 1.2rem;
          border-radius: 5px;
        }

        input:focus {
          border-color: #000;
          box-shadow: none;
        }

        .btn-lg {
          background-color: #000;
          color: #fff;
          padding: 12px 30px;
          border: none;
          cursor: pointer;
        }

        .btn-lg:hover {
          background-color: #333;
          color: #fff;
        }

        h2 {
          color: #fff;
          font-weight: bold;
        }

        form {
          background-color: rgba(255, 255, 255, 0.9);
          padding: 20px;
          border-radius: 10px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  

    <section id="register" style="background: url('images/background-img.png') no-repeat; background-size: cover; height: 100vh;">
  <div class="container h-100 d-flex justify-content-center align-items-center">
    <div class="col-md-6">
      <h2 class="display-3 fw-normal text-center">Registration Form </h2>
         <asp:Label ID="txtMsg" runat="server" Text="" Visible="false"></asp:Label>
        <div class="row">
          <!-- Name Input -->
          <div class="col-md-6 mb-3">
            <input type="text" class="form-control form-control-lg" name="name" id="Txtname" placeholder="Name" required="required" runat="server">
          </div>
          <!-- Surname Input -->
          <div class="col-md-6 mb-3">
            <input type="text" class="form-control form-control-lg" name="surname" id="Txtsurname" placeholder="Surname" required="required" runat="server">
          </div>
        </div>

        <div class="row">
          <!-- Second Name Input (Optional) -->
          <div class="col-md-6 mb-3">
            <input type="text" class="form-control form-control-lg" name="secondName" id="TxtsecondName" placeholder="Second Name (Optional)" runat="server">
          </div>
          <!-- Phone Number Input -->
          <div class="col-md-6 mb-3">
            <input type="tel" class="form-control form-control-lg" name="phoneNumber" id="TxtphoneNumber" placeholder="Phone Number" required="required" runat="server">
          </div>
        </div>

        <div class="row">
          <!-- Email Input -->
          <div class="col-md-6 mb-3">
            <input type="email" class="form-control form-control-lg" name="email" id="Txtemail" placeholder="Email Address" required="required" runat="server">
          </div>
          <!-- Password Input -->
          <div class="col-md-6 mb-3">
            <input type="password" class="form-control form-control-lg" name="password" id="Txtpassword1" placeholder="Create Password" required="required" runat="server">
          </div>
        </div>

        <div class="row">
          <!-- Confirm Password Input -->
          <div class="col-md-6 mb-3">
            <input type="password" class="form-control form-control-lg" name="confirmPassword" id="Txtpassword2" placeholder="Repeat Password" required="required" runat="server">
          </div>
          <!-- Empty column for spacing -->
          <div class="col-md-6 mb-3"></div>
        </div>

        <!-- Register Button (Centered) -->
        <div class="row">
           
          <div class="col-md-12 text-center">
              <asp:Button ID="btnRegister" CssClass="btn btn-dark btn-lg rounded-1 mt-4 px-5" runat="server" Text="Register" OnClick="btnRegister_Click" />
            
          </div>
        </div>
     
    </div>
  </div>
</section>



</asp:Content>
