<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Maran_Restaurents.User.Contact" %>

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

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lbl_MsgCont" runat="server" Text=""></asp:Label>
                </div>
                <h2>Send Your Query</h2>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">

                        <div>
                            <asp:TextBox ID="txt_Name" runat="server" CssClass="form-control" placeholder="Your Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="efv_Name" runat="server" ErrorMessage="Name Is Required" ControlToValidate="txt_Name" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txt_Email" runat="server" CssClass="form-control" placeholder="Your Email" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email Is Required" ControlToValidate="txt_Email" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                        </div>
                        <div>
                            <asp:TextBox ID="txt_Subject" runat="server" CssClass="form-control" placeholder="Subject"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_Subject" runat="server" ErrorMessage="Subject Is Required" ControlToValidate="txt_Subject" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:TextBox ID="txt_Message" runat="server" CssClass="form-control" placeholder="Enter your Query/Feedback"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_Message" runat="server" ErrorMessage="Message Is Required" ControlToValidate="txt_Message" ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>

                        <div class="btn_box">
                            <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-warning rounded-pill pl-4 pr-4 text-white" OnClick="btn_Submit_Click" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="map_container ">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/TemplateFiles/images/988574.gif" />
                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>
