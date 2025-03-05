<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Maran_Restaurents.User.Profile" %>

<%@ Import Namespace="Maran_Restaurents" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%
        string ImageUrl = Session["ImageUrl"].ToString();
    %>

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <h2>User Information</h2>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-title mb-4">
                                <div class="d-flex justify-content-start">
                                    <div class="image-container">

                                        <img id="imgProfile" src="<%= Utils.GetImageURL(ImageUrl) %>" style="width: 150px; height: 150px;" class="img-thumbnail" />
                                        <div class="middle pt-2">
                                            <a href="Registration.aspx?id=<%Response.Write(Session["UserId"]);%>"
                                                class="btn btn-warning">
                                                <i class="fa fa-pencil"></i>Edit Details
                                            </a>
                                        </div>
                                    </div>

                                    <div class="userData ml-3">
                                        <h2 class="d-block" style="font-size: 1.5rem; font-weight: bold">
                                            <a href="javascript:void(0);"><%Response.Write(Session["Name"]); %></a>
                                        </h2>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0)">
                                                <asp:Label ID="lbl_Username" runat="server" ToolTip="Unique Username">
                                                    <%Response.Write(Session["Username"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0)">
                                                <asp:Label ID="lbl_Email" runat="server" ToolTip="Unique Email">
                                               <%Response.Write(Session["Email"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                        <h6 class="d-block">
                                            <a href="javascript:void(0)">
                                                <asp:Label ID="lbl_CreateDate" runat="server" ToolTip="Account Created On">
                                                      <%Response.Write(Session["createDate"]); %>
                                                </asp:Label>
                                            </a>
                                        </h6>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active text-info" id="basicInfo_Tab" data-toggle="tab" href="#basicInfo" role="tab"
                                                aria-controls="basicInfo" aria-selected="true"><i class="fa fa-id-badge mr-2"></i>Basic Info</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link text-info" id="connected_Tab" data-toggle="tab" href="#OrderHis" role="tab"
                                                aria-controls="connected" aria-selected="false"><i class="fa fa-clock mr-2"></i>Purchased History</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content ml-1" id="myTabContent">
                                        <%--Basic User Info Starts--%>
                                        <div class="tab-pane fade show active" id="basicInfo" role="tabpanel" aria-labelledby="basicInfo-tab">
                                            <asp:Repeater ID="rep_UserProfile" runat="server">
                                                <ItemTemplate>

                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">Name</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Name") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">Mobile</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Mobile") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">Email</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Email") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">Address</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Address") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">PostCode</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("PostCode") %>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-3 col-md-3 col-5">
                                                            <label style="font-weight: bold;">Username</label>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <%# Eval("Username") %>
                                                        </div>
                                                    </div>
                                                    <hr />

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <%--Basic User Info End--%>

                                        <%--Order history Starts--%>
                                        <div class="tab-pane fade" id="OrderHis" role="tabpanel" aria-labelledby="connected_Tab">

                                            <asp:Repeater ID="rep_PurchesHistory" runat="server" OnItemDataBound="rep_PurchesHistory_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="container">
                                                        <div class="row pt-1 pb-1" style="background-color: lightgray">
                                                            <div class="col-4">
                                                                <span class="badge badge-pill badge-dark text-white">
                                                                    <%# Eval("SrNo") %>
                                                                </span>
                                                                Payment Mode: <%# Eval("PaymentMode").ToString() == "cod" ? "Cash On Delivery" : Eval("PaymentMode").ToString().ToString().ToUpper() %>
                                                            </div>
                                                            <div class="col-6">
                                                                <%# string.IsNullOrEmpty(Eval("CardNo").ToString()) ? "" : "Card No: " + Eval("CardNo") %>
                                                            </div>
                                                            <div class="col-2" style="text-align:end">
                                                                <a href='Invoice.aspx?id=<%# Eval("PaymentId") %>'><i class="fa fa-download mr-2"></i>Invoice</a>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hdn_PaymentId" runat="server" Value='<%# Eval("PaymentId") %>'/>
                                                        

                                                        <asp:Repeater ID="rep_Order" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table data-table-export table-responsive-sm table-bordered table-hover">
                                                                    <thead class="bg-dark text-white">
                                                                        <tr>
                                                                            <th>Products Name</th>
                                                                            <th>Unit Price</th>
                                                                            <th>Quantity</th>
                                                                            <th>Total Price</th>
                                                                            <th>OrderId</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                         </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><asp:Label ID="lbl_Name" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Price" runat="server" Text='<%# string.IsNullOrEmpty(Eval("Price").ToString()) ? "" : "₹" + Eval("Price") %>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Quantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>

                                                                    </td>
                                                                    <td>₹<asp:Label ID="lbl_TotalPrice" runat="server" Text='<%# Eval("TotalPrice") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="lbl_Order" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("Status") %>' CssClass='<%# Eval("Status").ToString() == "Deliverd" ? "badge badge-success" : "badge badge-warning" %>'></asp:Label></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                               </tbody>
                                                                 </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>

                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                        <%--Order history End--%>
                                    </div>

                                </div>
                            </div>



                        </div>
                    </div>

                </div>
            </div>

        </div>
    </section>

</asp:Content>
