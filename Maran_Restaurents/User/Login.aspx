<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Maran_Restaurents.User.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /* For disappearing alert message */
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Msg_Login.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lbl_Msg_Login" runat="server" Text=""></asp:Label>
                </div>
                <h2>Login</h2>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        <asp:Image ID="user_Login_Img" runat="server" src="../Images/login.jpg" alt="" class="img-thubnail" />
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form_container">
                        <div>
                            <asp:RequiredFieldValidator ID="rfv_uname" runat="server" ErrorMessage="User Name is Required" ControlToValidate="txt_Uname_login" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txt_Uname_login" runat="server" CssClass="form-control" placeholder="Enter the Username"></asp:TextBox>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is Required" ControlToValidate="txt_Pwd" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txt_Pwd" runat="server" CssClass="form-control" placeholder="Enter the Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="btn_box">
                            <asp:Button ID="btn_Login" runat="server" Text="Login" CssClass="btn btn-success rounded-pill pl-4 pr-4 text-white" OnClick="btn_Login_Click" />
                            <span class="pl-3 text-info">New User? <a href="Registration.aspx" class="badge badge-info">Register here</a> </span>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>

</asp:Content>
