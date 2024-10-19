<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InvoiceManagement.aspx.cs" Inherits="st1.InvoiceManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">
        <h2 class="text-center">Manage My Invoices</h2>
        <asp:Repeater ID="InvoiceRepeater" runat="server">
            <HeaderTemplate>
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>Invoice ID</th>
                            <th>Invoice Date</th>
                            <th>Total Amount</th>
                            <th>Status</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("InvoiceID") %></td>
                    <td><%# Eval("InvoiceDate", "{0:dd/MM/yyyy}") %></td>
                    <td>R <%# Eval("TotalAmount", "{0:F2}") %></td>
                    <td><%# Eval("Status") %></td>
                    <td>
                        <asp:Button runat="server" Text="Download PDF" CommandArgument='<%# Eval("InvoiceID") %>' OnClick="DownloadInvoice_Click" CssClass="btn btn-primary" />
                        <asp:Button runat="server" Text="Delete" CommandArgument='<%# Eval("InvoiceID") %>' OnClick="DeleteInvoice_Click" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
