<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddressList.aspx.cs" Inherits="CustomerLibrary.WebForms.AddressList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="col-xs-6">
                <h2 class="text-primary">Address List for customer <%= Customer.FirstName %>  <%= Customer.LastName %></h2>
                <p><a href="CustomerList">Return to Customer List</a></p>
            </div>
            <div class="col-xs-6 text-right">
                <a class="btn btn-primary" href="AddressEdit.aspx">Create New Address</a>
            </div>
        </div>

        <br/><br/>

        <table class="table table-bordered table-striped" style="width: 100%">
            <tr>
                <th class="text-center">Address Line</th>
                <th class="text-center">Address Line2</th>
                <th class="text-center">Address Type</th>
                <th class="text-center">City</th>
                <th class="text-center">Postal code</th>
                <th class="text-center">State</th>
                <th class="text-center">Country</th>
                <th class="text-center">Actions</th>
            </tr>
            <% foreach (var address in Customer.Addresses)
               { %>
                <tr>
                    <td class="text-center"><%= address.AddressLine %></td>
                    <td class="text-center"><%= address.AddressLine2 %></td>
                    <td class="text-center"><%= address.AddressType %></td>
                    <td class="text-center"><%= address.City %></td>
                    <td class="text-center"><%= address.PostalCode %></td>
                    <td class="text-center"><%= address.State %></td>
                    <td class="text-center"><%= address.Country %></td>
                    <td class="text-center">
                        <div class="btn-group" role="group">
                            <a class="btn btn-primary"href="AddressEdit?customerId=<%= address.CustomerId %>&addressId=<%= address.AddressId %>">
                                Edit
                            </a>
                            <a class="btn btn-danger" href="AddressDelete?customerId=<%= address.CustomerId %>&addressId=<%= address.AddressId %>">
                                Delete
                            </a>
                        </div>
                    </td>
                </tr>
            <% } %>
        </table>
    </div>

</asp:Content>