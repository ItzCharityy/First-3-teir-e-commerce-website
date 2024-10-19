<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="st1.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="container">
            <h2>Manage Users</h2>
            <asp:Repeater ID="UserRepeater" runat="server" OnItemCommand="UserRepeater_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>User ID</th>
                                <th>Surname</th>
                                <th>Email</th>
                                <th>Role</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("UserID") %></td>
                        <td><%# Eval("Surname") %></td>
                        <td><%# Eval("Email") %></td>
                        <td>
                            <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Customer" Value="Customer" />
                                <asp:ListItem Text="Manager" Value="Manager" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnChangeRole" runat="server" Text="Change Role" CommandName="ChangeRole" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-primary" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-danger" />
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
