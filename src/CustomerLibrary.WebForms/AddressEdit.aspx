<%@ Page Title="AddressEdit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddressEdit.aspx.cs" Inherits="CustomerLibrary.WebForms.AddressEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="body-content">
                <h2 class="text-primary m-bt">Address Form</h2>

                <asp:ListView ID="errors" runat="server">
                    <LayoutTemplate>
                        <h5 ID="errorHeader" class="text-danger" runat="server">Error summary:</h5>
                        <ul runat="server" ID="errorList" class="m-bt">
                            <li runat="server" ID="itemPlaceholder"></li>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li runat="server">
                            <asp:Label ID="error" CssClass="text-danger" runat="server" Text='<%# Eval("Error") %>'/>
                        </li>
                    </ItemTemplate>
                </asp:ListView>

                <asp:hiddenfield ID="addressId" Value="" runat="server"></asp:hiddenfield>

                <asp:hiddenfield ID="customerId" Value="" runat="server"></asp:hiddenfield>

                <div class="form-group">
                    <asp:Label runat="server" Text="Address Line"></asp:Label>
                    <asp:TextBox ID="addressLine" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="addressLineRequiredValidator" ControlToValidate="addressLine"
                                                ErrorMessage="Please enter Address Line." Display="Dynamic"
                                                CssClass="text-danger"
                                                runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="addressLine"
                                                    ID="addressLineMaxLengthValidator" ValidationExpression="^[\s\S]{0,100}$"
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Address Line2"></asp:Label>
                    <asp:TextBox ID="addressLine2" MaxLength="100" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="addressLine2"
                                                    ID="addressLine2MaxLengthValidator" ValidationExpression="^[\s\S]{0,100}$"
                                                    runat="server" ErrorMessage="Maximum 100 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <asp:DropDownList id="addressType"
                                  CssClass="form-control"
                                  runat="server">
                    <asp:ListItem Value="Billing" Selected="True">Billing</asp:ListItem>
                    <asp:ListItem Value="Shipping">Shipping</asp:ListItem>
                </asp:DropDownList>

                <div class="form-group">
                    <asp:Label runat="server" Text="City"></asp:Label>
                    <asp:TextBox ID="city" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="cityRequiredFieldValidator" ControlToValidate="city"
                                                ErrorMessage="Please enter City" Display="Dynamic"
                                                CssClass="text-danger"
                                                runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="city"
                                                    ID="cityMaxLengthValidator" ValidationExpression="^[\s\S]{0,50}$"
                                                    runat="server" ErrorMessage="Maximum 50 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Postal Code"></asp:Label>
                    <asp:TextBox ID="postalCode" MaxLength="6" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="postalCodeRequiredFieldValidator" ControlToValidate="postalCode"
                                                ErrorMessage="Please enter Postal Code" Display="Dynamic"
                                                CssClass="text-danger"
                                                runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="postalCode"
                                                    ID="postalCodeMaxLengthValidator" ValidationExpression="^[\s\S]{0,6}$"
                                                    runat="server" ErrorMessage="Maximum 6 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="State"></asp:Label>
                    <asp:TextBox ID="state" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="stateRequiredFieldValidator" ControlToValidate="state"
                                                ErrorMessage="Please enter State." Display="Dynamic"
                                                CssClass="text-danger"
                                                runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="state"
                                                    ID="stateMaxLengthValidator" ValidationExpression="^[\s\S]{0,20}$"
                                                    runat="server" ErrorMessage="Maximum 20 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <asp:DropDownList id="country"
                                  CssClass="form-control m-bt"
                                  runat="server">
                    <asp:ListItem Value="United States" Selected="True">United States</asp:ListItem>
                    <asp:ListItem Value="Canada">Canada</asp:ListItem>
                </asp:DropDownList>

                <a class="btn btn-danger m-rt" href="AddressList?customerId=<%= CustomerId %>">
                    Cancel
                </a>

                <asp:Button runat="server" CssClass="btn btn-primary"
                            OnClick="OnSaveClick"
                            Text="Save"/>
            </div>
        </div>
    </div>

</asp:Content>