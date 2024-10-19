<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="st1.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            margin-top: 20px;
        }

        .table {
            width: 100%;
            table-layout: fixed;
        }

        .table th, .table td {
            word-wrap: break-word;
        }

        .sticky-top {
            position: -webkit-sticky;
            position: sticky;
            top: 20px;
        }

        .cart-summary {
            padding: 20px;
            background-color: #f8f9fa;
            border: 1px solid #ddd;
            border-radius: 5px;
            max-width: 300px;
        }

        /* Ensure the footer stays at the bottom */
        footer {
            clear: both; /* This ensures that the footer moves below floating elements */
        }

        /* Ensure the container is cleared */
        .clearfix::after {
            content: "";
            display: table;
            clear: both;
        }



        .button-container {
            display: flex;
            flex-direction: column; /* Stack buttons vertically */
            align-items: center; /* Center align the buttons horizontally */
        }

        .custom-btn {
            width: 100px;  /* Set a fixed width */
            height: 40px;  /* Set a fixed height */
            margin-bottom: 10px; /* Optional: space between buttons */
            font-size: 14px; /* Adjust font size to fit inside the button */
            padding: 0;  /* Remove extra padding */
            display: flex;  /* Use flexbox for button text alignment */
            justify-content: center;  /* Center text horizontally */
            align-items: center;  /* Center text vertically */
            text-align: center;  /* Ensure text is centered */
            line-height: normal;  /* Reset line height to avoid text overflow */
        }




       

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container clearfix"> <!-- Added clearfix class -->
    <div class="row">
        <!-- Main Content (Products Table) -->
        <div class="col-md-8">
            <h1 class="my-4">Shopping Cart</h1>

            <!-- Shopping Cart Table -->
            <asp:Repeater ID="CartRepeater" runat="server" >
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Product Name</th>
                                <th>Unit Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <ItemTemplate>
                <tr>
                    <!-- Display Product Name -->
                    <td><%# Eval("ProductName") %></td>
        
                    <!-- Display Price -->
                    <td>R <%# Eval("Price", "{0:F2}") %></td>

                    <!-- Quantity input, pre-filled with the current quantity -->
                    <td>
                        <asp:TextBox ID="Number1" CssClass="form-control" Text='<%# Eval("Quantity") %>' runat="server" />

                    </td>
        
                    <!-- Display Total Price -->
                    <td>
                        R <%# Eval("TotalPrice", "{0:F2}") %>
                    </td>

                    <!-- Action Buttons (Update and Remove) -->
                    <td class="button-container">
                        <asp:Button runat="server" CssClass="btn btn-primary custom-btn" Text="Update" CommandName="Update" CommandArgument='<%# Eval("ProductID") %>' ID="btnUpdate" OnClick="Update_Click" />
                        <asp:Button runat="server" CssClass="btn btn-danger custom-btn" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ProductID") %>' ID="btnRemove" OnClick="Remove_Click" />
                    </td>
                </tr>
            </ItemTemplate>

        
                   

        
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Sidebar (Sticky Cart Summary) -->
        <div class="col-md-4">
            <div class="cart-summary sticky-top">
                <p>Subtotal: R <span id="Csubtotal" runat="server">0.00</span></p>
                <p>VAT (15%): R <span id="Cvat"  runat="server">0.00</span></p>
                <h3>Total: R <span id="CgrandTotal"  runat="server">00.00</span></h3>

                <!-- Checkout Button -->
                <button type="button" class="btn btn-success" onclick="proceedToCheckout()">Proceed to Checkout</button>
            </div>
        </div>
    </div>
</div>


    <script type="text/javascript">
    function updateCartItem(productId, quantity, price) {
        // Find the quantity input by ProductID
        var quantityInput = document.getElementById("Qunt_" + productId);
        var totalCell = document.getElementById("Tot_" + productId);

        // Update the quantity field
        if (quantityInput) {
            quantityInput.value = quantity;
        }

        // Update the total field
        if (totalCell) {
            totalCell.innerText = "R " + (quantity * price).toFixed(2);
        }
    }
    </script>

</asp:Content>
