<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddressDelete.aspx.cs" Inherits="CustomerLibrary.WebForms.AddressDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="col-xs-6 col-xs-offset-3">
                <div class="panel panel-default">
                    <div class="panel-heading">Address Delete Confirmation</div>
                    <div class="panel-body text-center">
                        <% if (AddressToDelete == null)
                           { %>
                            <h4 class="text-center">Something went wrong.</h4>
                            <h4 class="text-center">We did not find the address you want to delete.</h4>
                            <p>
                                <a href="CustomerList">Return to Customer List</a>
                            </p>
                        <% }
                           else
                           { %>
                            <h4>Are you sure you want to delete <%= AddressToDelete.AddressType %> Address:</h4>
                            <h4 class="p-bt"><%= AddressToDelete.AddressLine %> in <%= AddressToDelete.City %>?</h4>
                            <div class="text-center">
                                <a class="btn btn-primary" href="AddressList?customerId=<%= AddressToDelete.CustomerId %>">
                                    Cancel
                                </a>
                                <asp:Button runat="server" CssClass="btn btn-danger"
                                            OnClick="OnDeleteClick"
                                            Text="Delete"/>
                            </div>
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>