<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerDelete.aspx.cs" Inherits="CustomerLibrary.WebForms.CustomerDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <% if (CustomerToDelete == null)
               { %>
                <div class="col-xs-6 col-xs-offset-3">
                    <h2>Something went wrong. We did not find the customer you want to delete.</h2>
                </div>
            <% }
               else
               { %>
                <div class="col-xs-6 col-xs-offset-3">
                    <h2>Are you sure you want to delete customer <%= CustomerToDelete.FirstName %> <%= CustomerToDelete.LastName %>?</h2>
                    <div class="text-center">
                        
                        <a class="btn btn-primary" href="CustomerList.aspx">
                            Cancel
                        </a>
                        <asp:Button runat="server" CssClass="btn btn-danger"
                                    OnClick="OnDeleteClick"
                                    Text="Delete"/>
                    </div>
                </div>
            <% } %>
        </div>
    </div>

</asp:Content>