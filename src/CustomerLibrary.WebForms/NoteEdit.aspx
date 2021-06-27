<%@ Page Title="NoteEdit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoteEdit.aspx.cs" Inherits="CustomerLibrary.WebForms.NoteEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="body-content">
                <h2 class="text-primary m-bt">Note Form</h2>

                <asp:hiddenfield ID="noteId" Value="" runat="server"></asp:hiddenfield>

                <asp:hiddenfield ID="customerId" Value="" runat="server"></asp:hiddenfield>

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


                <a class="btn btn-danger m-rt" href="NoteList?customerId=<%= CustomerId %>">
                    Cancel
                </a>

                <asp:Button runat="server" CssClass="btn btn-primary"
                            OnClick="OnSaveClick"
                            Text="Save"/>
            </div>
        </div>
    </div>

</asp:Content>