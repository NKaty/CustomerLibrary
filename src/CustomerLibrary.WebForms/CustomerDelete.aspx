<%@ Page Title="CustomerDelete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDelete.aspx.cs" Inherits="CustomerLibrary.WebForms.CustomerDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="col-xs-6 col-xs-offset-3">
                <div class="panel panel-default">
                    <div class="panel-heading">Customer Delete Confirmation</div>
                    <div class="panel-body text-center">
                        <% if (CustomerToDelete == null)
                           { %>
                            <h4 class="text-center">Something went wrong.</h4>
                            <h4 class="text-center">We did not find the customer you want to delete.</h4>
                            <p>
                                <a href="CustomerList">Return to Customer List</a>
                            </p>
                        <% }
                           else
                           { %>
                            <h4>Are you sure you want to delete customer</h4>
                            <h4 class="m-bt"><%= CustomerToDelete.FirstName %> <%= CustomerToDelete.LastName %>?</h4>
                            <div class="text-center">
                                <a class="btn btn-primary" href="CustomerList">
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