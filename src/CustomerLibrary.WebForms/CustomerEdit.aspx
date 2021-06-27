<%@ Page Title="CustomerEdit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="CustomerLibrary.WebForms.CustomerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
    <br/>
    <div class="row">
        <div class="body-content">
            <h2 class="text-primary m-bt">Customer Form</h2>

            <div class="m-bt">
                <div class="form-group">
                    <asp:Label runat="server" Text="First Name"></asp:Label>
                    <asp:TextBox ID="firstName" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="firstName"
                                                    ID="firstNameMaxLengthValidator" ValidationExpression="^[\s\S]{0,50}$"
                                                    runat="server" ErrorMessage="Maximum 50 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Last Name"></asp:Label>
                    <asp:TextBox ID="lastName" MaxLength="50" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="lastNameRequiredFieldValidator" ControlToValidate="lastName"
                                                ErrorMessage="Please enter Last Name." Display="Dynamic"
                                                CssClass="text-danger"
                                                runat="server"/>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="lastName"
                                                    ID="lastNameMaxLengthValidator" ValidationExpression="^[\s\S]{0,50}$"
                                                    runat="server" ErrorMessage="Maximum 50 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="email" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="email" CssClass="text-danger"
                                                    ID="emailValidator" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$" runat="server"
                                                    ErrorMessage="The email is invalid.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Phone Number"></asp:Label>
                    <asp:TextBox ID="phoneNumber" CssClass="form-control" TextMode="Phone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="phoneNumber" CssClass="text-danger"
                                                    ID="phoneValidator" ValidationExpression="^\+[1-9]\d{1,14}$" runat="server"
                                                    ErrorMessage="The phone is invalid.">
                    </asp:RegularExpressionValidator>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Total Purchases Amount"></asp:Label>
                    <asp:TextBox ID="amount" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="amountError" Text="" CssClass="text-danger" runat="server"></asp:Label>
                </div>
            </div>

            <div class="m-bt">
                <asp:Repeater ID="addresses" ItemType="CustomerLibrary.Address" runat="server">
                    <HeaderTemplate>
                        <h4 class="d-inline-block m-rt">Addresses</h4>

                        <asp:Button ID="addAddress" runat="server" CssClass="btn btn-sm btn-primary m-rt"
                                    OnClick="AddAddress"
                                    Text="Add Address"/>

                        <asp:Button ID="deleteAddress" runat="server" CssClass="btn btn-sm btn-danger"
                                    OnClick="DeleteAddress" Enabled="<%# Customer.Addresses.Count > 1 %>"
                                    Text="Delete Address"/>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="m-bt m-lf">
                            <h4 class="text-primary">Address</h4>

                            <asp:hiddenfield ID="addressId" Value='<%# Eval("AddressId") %>' runat="server"></asp:hiddenfield>

                            <div class="form-group">
                                <asp:Label runat="server" Text="Address Line"></asp:Label>
                                <asp:TextBox ID="addressLine" MaxLength="100" Text='<%# Eval("AddressLine") %>' CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="addressLine2" MaxLength="100" Text='<%# Eval("AddressLine2") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="addressLine2"
                                                                ID="addressLine2MaxLengthValidator" ValidationExpression="^[\s\S]{0,100}$"
                                                                runat="server" ErrorMessage="Maximum 100 characters allowed.">
                                </asp:RegularExpressionValidator>
                            </div>

                            <asp:DropDownList id="addressType"
                                              CssClass="form-control"
                                              SelectedValue='<%# Eval("AddressType") == null ? "Billing" : Eval("AddressType") %>'
                                              runat="server">
                                <asp:ListItem Value="Billing">Billing</asp:ListItem>
                                <asp:ListItem Value="Shipping">Shipping</asp:ListItem>
                            </asp:DropDownList>

                            <div class="form-group">
                                <asp:Label runat="server" Text="City"></asp:Label>
                                <asp:TextBox ID="city" MaxLength="50" Text='<%# Eval("City") %>' CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="postalCode" MaxLength="6" Text='<%# Eval("PostalCode") %>' CssClass="form-control" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="state" MaxLength="20" Text='<%# Eval("State") %>' CssClass="form-control" runat="server"></asp:TextBox>
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
                                              CssClass="form-control"
                                              SelectedValue='<%# Eval("Country") == null ? "United States" : Eval("Country") %>'
                                              runat="server">
                                <asp:ListItem Value="United States">United States</asp:ListItem>
                                <asp:ListItem Value="Canada">Canada</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="m-bt">
                <asp:Repeater ID="notes" runat="server">
                    <HeaderTemplate>
                        <div class="m-bt">
                            <h4 class="d-inline-block m-rt">Notes</h4>
                            <asp:Button ID="addNote" runat="server" CssClass="btn btn-sm btn-primary m-rt"
                                        OnClick="AddNote"
                                        Text="Add Note"/>

                            <asp:Button ID="deleteNote" runat="server" CssClass="btn btn-sm btn-danger"
                                        OnClick="DeleteNote" Enabled="<%# Customer.Notes.Count > 1 %>"
                                        Text="Delete Note"/>
                        </div>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="m-bt m-lf">
                            <asp:hiddenfield ID="noteId" Value='<%# Eval("NoteId") %>' runat="server"></asp:hiddenfield>

                            <div class="form-group">
                                <asp:Label runat="server" Text="Note"></asp:Label>
                                <asp:TextBox ID="noteText" Text='<%# Eval("NoteText") %>' TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="noteTextRequiredFieldValidator" ControlToValidate="noteText"
                                                            ErrorMessage="Please enter Note." Display="Dynamic"
                                                            CssClass="text-danger"
                                                            runat="server"/>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="noteText"
                                                                ID="noteTextcValidator" ValidationExpression="^[\s\S]{0,500}$"
                                                                runat="server" ErrorMessage="Maximum 500 characters allowed.">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="m-t">
                <a class="btn btn-danger m-rt" href="CustomerList">
                    Cancel
                </a>

                <asp:Button runat="server" CssClass="btn btn-primary"
                            OnClick="OnSaveClick"
                            Text="Save"/>
            </div>
        </div>
    </div>
</div>
</asp:Content>