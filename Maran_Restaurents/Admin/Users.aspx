<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Maran_Restaurents.Admin.Users" %>
<%@ Import Namespace="Maran_Restaurents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /* For disappearing alert message */
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Msg.ClientID %>").style.display = "none";
         }, seconds * 1000);
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">
        <div class="align aligin-Self-end">
            <asp:Label ID="lbl_Msg" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header">
                                </div>
                                <div class="card-block">
                                    <div class="row">
                                   
                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Users Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater ID="rep_users" runat="server" OnItemCommand="rep_users_ItemCommand">
                                                        <headertemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">SrNo</th>
                                                                        <th>Name</th>
                                                                        <th>UserName</th>
                                                                        <th>Email</th>
                                                                        <th>JoinDate</th>
                                                                        <th class="datatable-nosort">Delete</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </headertemplate>
                                                        <itemtemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("SRINO") %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Username") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("createDate") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnk_Delete" Text="Delete" runat="server" CommandName="delete" class="badge bg-danger" CommandArgument='<%# Eval("UserId") %>' OnClientClick="return confirm('Do You want to delete this User?');"><i class="ti-trash"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </itemtemplate>
                                                        <footertemplate>
                                                            </tbody>
                                                            </table>
                                                        </footertemplate>
                                                    </asp:Repeater>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
