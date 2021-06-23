﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="CustomerLibrary.WebForms.CustomerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <asp:Label runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="firstName" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    
    <div class="form-group">
        <asp:Label runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="lastName" CssClass="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="email" CssClass="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Phone Number"></asp:Label>
        <asp:TextBox ID="phoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="Total Purchases Amount"></asp:Label>
        <asp:TextBox ID="amount" CssClass="form-control" runat="server"></asp:TextBox>
    </div>

    <asp:Repeater ID="addresses" runat="server" OnItemCommand="addresses_ItemCommand">
        <ItemTemplate>
            <div class="form-group" hidden>
                <asp:Label runat="server" Text="AddressId"></asp:Label>
                <asp:TextBox ID="addressId" Text='<%# Eval("AddressId") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Address Line"></asp:Label>
                <asp:TextBox ID="addressLine" Text='<%# Eval("AddressLine") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Address Line2"></asp:Label>
                <asp:TextBox ID="addressLine2" Text='<%# Eval("AddressLine2") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Address Type"></asp:Label>
                <asp:TextBox ID="addressType" Text='<%# Eval("AddressType") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="City"></asp:Label>
                <asp:TextBox ID="city" Text='<%# Eval("City") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Postal Code"></asp:Label>
                <asp:TextBox ID="postalCode" Text='<%# Eval("PostalCode") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="State"></asp:Label>
                <asp:TextBox ID="state" Text='<%# Eval("State") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Country"></asp:Label>
                <asp:TextBox ID="country" Text='<%# Eval("Country") %>' CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <asp:Button runat="server" CssClass="btn btn-primary"
                CommandName="Add"
                Text="Add Address"/>

    <asp:Repeater ID="notes" runat="server">
        <ItemTemplate>
            <div class="border">
                <h4>Your Note:</h4>
                <div class="form-group" hidden>
                    <asp:Label runat="server" Text="NoteId"></asp:Label>
                    <asp:TextBox ID="noteId" Text='<%# Eval("NoteId") %>' CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Note"></asp:Label>
                    <asp:TextBox ID="noteText" Text='<%# Eval("NoteText") %>' CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Button runat="server" CssClass="btn btn-primary"
                OnClick="OnSaveClick"
                Text="Save"/>
</asp:Content>