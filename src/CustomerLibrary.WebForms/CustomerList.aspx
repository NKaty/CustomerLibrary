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
            <% foreach (var customer in Customers)
               { %>

                <tr>
                    <td><%= customer.FirstName %></td>
                    <td><%= customer.LastName %></td>
                    <td><%= customer.Email %></td>
                    <td><%= customer.PhoneNumber %></td>
                    <td><%= customer.TotalPurchasesAmount %></td>
                    <td><a>Addresses</a></td>
                    <td><a>Notes</a></td>
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
    </div>

</asp:Content>