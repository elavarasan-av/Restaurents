<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="OrderStatus.aspx.cs" Inherits="Maran_Restaurents.Admin.OrderStatus" %>

<%@ Import Namespace="Maran_Restaurents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        /* For disappearing alert message */
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_MsgOderStatus.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">
        <div class="align aligin-Self-end">
            <asp:Label ID="lbl_MsgOderStatus" runat="server" Visible="false"></asp:Label>
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
                                        <div class="col-sm-6 col-md-8 col-lg-8">
                                            <h4 class="sub-title">Order List</h4>

                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater ID="rep_OrderStatus" runat="server" OnItemCommand="rep_OrderStatus_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">OderNo</th>
                                                                        <th>Order Date</th>
                                                                        <th>Status</th>
                                                                        <th>Products Name</th>
                                                                        <th>Total Price</th>
                                                                        <th>Payment Mode</th>
                                                                        <th class="datatable-nosort">Edit</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("OrderNo") %></td>
                                                                <td><%# Eval("OrderDate") %></td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Status" runat="server" Text=' <%# Eval("Status") %>' CssClass=' <%# Eval("Status").ToString()== "Deliverd" ? "badge badge-success" : "badge badge-warning"%>'></asp:Label>
                                                                </td>
                                                                <td><%# Eval("ProductName") %></td>
                                                                <td><%# Eval("TotalPrice") %></td>
                                                                <td><%# Eval("PaymentMode") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lktbn_EditOredDeils" Text="Edit" runat="server" class="badge badge-primary" CommandArgument='<%# Eval("OrderDetialsID") %>' CommandName="edit"><i class="ti-pencil"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-md-4 col-lg-4 mobile-inputs">
                                            <asp:Panel ID="pan_UpdateOrStatus" runat="server">

                                                <h4 class="sub-title">Update Status</h4>
                                                <div>
                                                    <div class="form-group">
                                                        <label>Order Status</label>
                                                        <div>
                                                            <asp:DropDownList ID="ddl_OrderStatus" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                                                                <asp:ListItem>Pending</asp:ListItem>
                                                                <asp:ListItem>Dispatched</asp:ListItem>
                                                                <asp:ListItem>Deliverd</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvd_OrderStatus" runat="server" ForeColor="Red" ControlToValidate="ddl_OrderStatus" ErrorMessage="Order Status is required" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                                        </div>
                                                    </div>
                                                        <div class="pb-5">
                                                        <asp:Button ID="btn_UpdateOrdDeils" runat="server" Text="Update" class="btn btn-primary" OnClick="btn_UpdateOrdDeils_Click"/>
                                                        &nbsp;
                                                        <asp:Button ID="btn_CancelOrdDeils" runat="server" Text="Cancel" class="btn btn-primary" OnClick="btn_CancelOrdDeils_Click"/>
                                                    </div>
                                                  
                                                </div>

                                            </asp:Panel>
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
