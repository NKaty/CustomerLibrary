<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoteList.aspx.cs" Inherits="CustomerLibrary.WebForms.NoteList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br/>
        <div class="row">
            <div class="col-xs-6">
                <h2 class="text-primary">Note List for customer <%= Customer.FirstName %> <%= Customer.LastName %></h2>
                <p>
                    <a href="CustomerList">Return to Customer List</a>
                </p>
            </div>
            <div class="col-xs-6 text-right">
                <a class="btn btn-primary" href="NoteEdit">Create New Note</a>
            </div>
        </div>

        <br/><br/>

        <table class="table table-bordered table-striped" style="width: 100%">
            <tr>
                <th class="text-center col-xs-9">Note</th>
                <th class="text-center">Actions</th>
            </tr>
            <% foreach (var note in Customer.Notes)
               { %>
                <tr>
                    <td class="text-center"><%= note.NoteText %></td>
                    <td class="text-center">
                        <div class="btn-group" role="group">
                            <a class="btn btn-primary"href="NoteEdit?customerId=<%= note.CustomerId %>&noteId=<%= note.NoteId %>">
                                Edit
                            </a>
                            <% if (Customer.Notes.Count > 1)
                               { %>
                                <a class="btn btn-danger" href="NoteDelete?customerId=<%= note.CustomerId %>&noteId=<%= note.NoteId %>">
                                    Delete
                                </a>
                            <% } %>
                            <%
                               else
                               { %>
                                <button class="btn btn-danger disabled" disabled>Delete</button>
                            <% } %>
                        </div>
                    </td>
                </tr>
            <% } %>
        </table>
    </div>

</asp:Content>