<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Maran_Restaurents.Admin.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pcoded-inner-content pt-0">

        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="container">
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label>Form Date</label>
                                                <asp:RequiredFieldValidator ID="rfv_FromDate" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txt_FromDate"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txt_FromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>To Date</label>
                                                <asp:RequiredFieldValidator ID="rfv_ToDate" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txt_ToDate"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txt_ToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:Button ID="btn_Serch" runat="server" Text="Serch" CssClass="btn btn-primary mt-md-4" OnClick="btn_Serch_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="card-block">
                                    <div class="row">

                                        <div class="col-12 mobile-inputs">
                                            <h4 class="sub-title">Selling Reports</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater ID="rep_Selling" runat="server">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">SrNo</th>
                                                                        <th>Name</th>
                                                                        <th>Email</th>
                                                                        <th>Item Order</th>
                                                                        <th>Total Cost</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("SrNo") %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("TotalOrders") %></td>
                                                                <td><%# Eval("TotalPrice") %></td>
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

                                    </div>

                                    <div class="row pl-4">
                                        <asp:Label ID="lbl_Total" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
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
