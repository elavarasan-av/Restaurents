<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Contacts_ad.aspx.cs" Inherits="Maran_Restaurents.Admin.Contacts_ad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <script>
      /* For disappearing alert message */
      window.onload = function () {
          var seconds = 5;
          setTimeout(function () {
              document.getElementById("<%=lbl_MsgCont.ClientID %>").style.display = "none";
          }, seconds * 1000);
      };
      </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="pcoded-inner-content pt-0">
       <div class="align aligin-Self-end">
           <asp:Label ID="lbl_MsgCont" runat="server" Visible="false"></asp:Label>
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
                                           <h4 class="sub-title">Contect Lists</h4>
                                           <div class="card-block table-border-style">
                                               <div class="table-responsive">

                                                   <asp:Repeater ID="rep_Contact" runat="server" OnItemCommand="rep_Contact_ItemCommand">
                                                       <headertemplate>
                                                           <table class="table data-table-export table-hover nowrap">
                                                               <thead>
                                                                   <tr>
                                                                       <th class="table-plus">SrNo</th>
                                                                       <th>UserName</th>
                                                                       <th>Email</th>
                                                                       <th>Subject</th>
                                                                       <th>Message</th>
                                                                       <th>Contact Date</th>
                                                                       <th class="datatable-nosort">Delete</th>
                                                                   </tr>
                                                               </thead>
                                                               <tbody>
                                                       </headertemplate>
                                                       <itemtemplate>
                                                           <tr>
                                                               <td class="table-plus"><%# Eval("SrNo") %></td>
                                                               <td><%# Eval("Name") %></td>
                                                               <td><%# Eval("Email") %></td>
                                                               <td><%# Eval("Subject") %></td>
                                                               <td><%# Eval("Message") %></td>
                                                               
                                                               <td><%# Eval("ContactDate") %></td>
                                                               <td>
                                                                   <asp:LinkButton ID="lnk_Delete" Text="Delete" runat="server" CommandName="delete" class="badge bg-danger" CommandArgument='<%# Eval("ContactId") %>' OnClientClick="return confirm('Do You want to delete this Records?');"><i class="ti-trash"></i></asp:LinkButton>
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
