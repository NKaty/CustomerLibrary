<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="CustomerLibrary.WebForms.CustomerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="col-xs-6">
                <h2 class="text-primary">Customer List</h2>
            </div>
            <div class="col-xs-6 text-right">
                <a class="btn btn-primary" href="CustomerEdit.aspx">Create New Customer</a>
            </div>
        </div>

        <br/><br/>

        <table class="table table-bordered table-striped" style="width: 100%">
            <tr>
                <th class="text-center">First Name</th>
                <th class="text-center">Last Name</th>
                <th class="text-center">Email</th>
                <th class="text-center">Phone</th>
                <th class="text-center">Total Amount</th>
                <th class="text-center">Addresses link</th>
                <th class="text-center">Notes link</th>
                <th class="text-center">Actions</th>
            </tr>
            <% foreach (var customer in Customers)
               { %>
                <tr>
                    <td class="text-center"><%= customer.FirstName %></td>
                    <td class="text-center"><%= customer.LastName %></td>
                    <td class="text-center"><%= customer.Email %></td>
                    <td class="text-center"><%= customer.PhoneNumber %></td>
                    <td class="text-center"><%= customer.TotalPurchasesAmount %></td>
                    <td class="text-center">
                        <a href="AddressList?customerId=<%= customer.CustomerId %>">Addresses</a>
                    </td>
                    <td class="text-center">
                        <a href="NoteList?customerId=<%= customer.CustomerId %>">Notes</a>
                    </td>
                    <td class="text-center">
                        <div class="btn-group" role="group">
                            <a class="btn btn-primary" href="CustomerEdit?customerId=<%= customer.CustomerId %>">
                                Edit
                            </a>
                            <a class="btn btn-danger" href="CustomerDelete?customerId=<%= customer.CustomerId %>">
                                Delete
                            </a>
                        </div>
                    </td>
                </tr>
            <% } %>
        </table>

        <% if (LastPage > 1)
           { %>
            <div class="row">
                <div class="col-xs-6 col-xs-offset-3">
                    <div class="text-center">
                        <% if (CurrentPage != 1)
                           { %>
                            <a class="btn btn-primary" href="CustomerList.aspx?page=<%= CurrentPage - 1 %>">Prev</a>
                        <% } %>
                        <%
                           else
                           { %>
                            <button class="btn btn-primary disabled" disabled>Prev</button>
                        <% } %>
                        <% if (CurrentPage != LastPage)
                           { %>
                            <a class="btn btn-primary" href="CustomerList.aspx?page=<%= CurrentPage + 1 %>">Next</a>
                        <% } %>
                        <%
                           else
                           { %>
                            <button class="btn btn-primary disabled" disabled>Next</button>
                        <% } %>
                    </div>
                </div>
            </div>
        <% } %>
    </div>

</asp:Content>