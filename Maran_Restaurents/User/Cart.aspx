<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Maran_Restaurents.User.Cart" %>

<%@ Import Namespace="Maran_Restaurents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script>
     /* For disappearing alert message */
     window.onload = function ()
     {
         var seconds = 5;
         setTimeout(function () {
             document.getElementById("<%=lbl_MsgCart.ClientID %>").style.display = "none";
         }, seconds * 1000);
     };
     </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lbl_MsgCart" runat="server" Visible="false"></asp:Label>
                </div>
                <h2>Your Shoping Cart</h2>
            </div>
        </div>

        <div class="container">
            <asp:Repeater ID="rep_CartItem" runat="server" OnItemCommand="rep_CartItem_ItemCommand" OnItemDataBound="rep_CartItem_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-success table-striped-columns">
                        <thead>
                            <tr>
                                <th scope="col">Name</th>
                                <th scope="col">Image</th>
                                <th scope="col">Price</th>
                                <th scope="col">Quantity</th>
                                <th scope="col">Total Price</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_NameCart" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                        </td>
                        <td>
                            <img width="60" src="<%# Utils.GetImageURL(Eval("ImageUrl")) %>" alt="" />
                        </td>
                        <td>
                            ₹ <asp:Label ID="lbl_PriceCart" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                            <asp:HiddenField ID="hdn_ProdId" runat="server" Value='<%# Eval("productId") %>' />
                            <asp:HiddenField ID="hdn_Qty" runat="server" Value='<%# Eval("qty") %>' />
                            <asp:HiddenField ID="hdn_ProdQty" runat="server" Value='<%# Eval("prdQty") %>' />
                        </td>
                        <td>
                            <div class="product__details__option">
                                <div class="quantity">
                                    <div class="pro-qty">
                                        <asp:TextBox ID="txt_Qty" runat="server" TextMode="Number" Text='<%# Eval("Quantity") %>' ></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_QtyValid" runat="server" ErrorMessage="*" ForeColor="Red" Font-Size="Small" ValidationExpression="[1-9]*" ControlToValidate="txt_Qty" Display="Dynamic" SetFocusOnError="true" EnableClientScript="true"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                             ₹ <asp:Label ID="lbl_tolPrice" runat="server" ></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtn_Detel" runat="server" Text="Remove" CommandName="remove" CommandArgument='<%# Eval("productId") %>' OnClientClick="return confirm('Do you want to remove this item from cart?');"><i class="fa fa-close"></i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3"></td>
                        <td class="pl-lg-5">
                            <b>Grand Total:-</b>
                        </td>
                        <td>₹<% Response.Write(Session["grandTotalPrice"]); %></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="continue__btn">
                            <a href="Menu.aspx" class="btn btn-info"> <i class="fa fa-arrow-circle-left mr-2"></i> Continue Shopping</a>
                        </td>
                        <td>
                            <asp:LinkButton ID="lkbtn_UpdateCart" runat="server" CommandName="updateCart" CssClass="btn btn-warning" > <i class="fa fa-refresh mr-2">  Update Cart</i></asp:LinkButton>
                        </td>
                        <td>
                             <asp:LinkButton ID="lkbtn_Checkout" runat="server" CommandName="checkout" CssClass="btn btn-success"> Checkout <i class="fa fa-arrow-circle-right mr-2"></i></asp:LinkButton>
                        </td>
                    </tr>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

    </section>
</asp:Content>
