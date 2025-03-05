<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Maran_Restaurents.User.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        /* For disappearing alert message */
        window.onload = function ()
        {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lbl_Msg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>

    <script>
        function ImagePreview_reg(input)
        {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#<%=img_user.ClientID%>").prop("src", e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="book_section layout_padding">
        <div class="container">
            <div class="heading_container">
                <div class="align-self-end">
                    <asp:Label ID="lbl_Msg" runat="server" Visible="false"></asp:Label>
                </div>
                <asp:Label ID="lbl_HeaderMsg" runat="server" Text="<h2>User Registration<h2>"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form_container">
                        
                        <div>
                            <asp:RequiredFieldValidator ID="rfv_Name" runat="server" ErrorMessage="Enter the Name" ControlToValidate="txt_Name_NewUser"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev_Name" runat="server" ErrorMessage="Enter the Currect Name" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$" ControlToValidate="txt_Name_NewUser"></asp:RegularExpressionValidator>
                             <asp:TextBox ID="txt_Name_NewUser" runat="server"  CssClass="form-control" placeholder="Enter the Name" ToolTip="Full Name"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfv_Mobile" runat="server" ErrorMessage="Mobile No Required" ControlToValidate="txt_Mobile"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev_Mobile" runat="server" ErrorMessage="Enter the Vaild Mobile" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{10}$" ControlToValidate="txt_Mobile"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txt_Mobile" runat="server"  class="form-control" placeholder="Enter the Mobile" ToolTip="Full Name" TextMode="Number"></asp:TextBox>
                        </div>

                        <div>                           
                            <asp:RequiredFieldValidator ID="rfv_Email" runat="server" ErrorMessage="Email is  Required" ControlToValidate="txt_Email"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                       <asp:TextBox ID="txt_Email" runat="server"  CssClass="form-control" placeholder="Enter the Email" ToolTip="Email" TextMode="Email"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfv_Address" runat="server" ErrorMessage="Address is Required" ControlToValidate="txt_Address"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                             <asp:TextBox ID="txt_Address" runat="server"   CssClass="form-control" placeholder="Enter the Address" ToolTip="Address" ></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="form_container">
                        <div>                         
                            <asp:RequiredFieldValidator ID="rfv_Postcode" runat="server" ErrorMessage="PostCode is Required" ControlToValidate="txt_PostCode"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev_PostCode" runat="server" ErrorMessage="Enter the Vaild PostCode" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[0-9]{6}$" ControlToValidate="txt_PostCode"></asp:RegularExpressionValidator>
                               <asp:TextBox ID="txt_PostCode" runat="server" CssClass="form-control" placeholder="Enter the PostCode" ToolTip="PostCode" TextMode="Number"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfv_Username" runat="server" ErrorMessage="Username is Required" ControlToValidate="txt_UserName"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                             <asp:TextBox ID="txt_UserName" runat="server"  class="form-control" placeholder="Enter Username" ToolTip="Username"></asp:TextBox>
                        </div>

                        <div>
                            <asp:RequiredFieldValidator ID="rfv_Pwd" runat="server" ErrorMessage="Password is Required" ControlToValidate="txt_pwd"
                                ForeColor="Red" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                             <asp:TextBox ID="txt_pwd" runat="server"  CssClass="form-control" placeholder="Enter the Password" ToolTip="Password" TextMode="Password"></asp:TextBox>
                        </div>

                        <div>
                            <asp:FileUpload ID="fu_UserImage" runat="server" class="form-control" ToolTip="UserImage" onchange="ImagePreview_reg(this);" />
                        </div>

                    </div>
                </div>

                <div class="row pl-4">
                    <div class="btn_box">
                        <asp:Button ID="btn_Register" runat="server" Text="Register" class="btn btn-success rounded-pill pl-4 pr-4 text-white"   OnClick="btn_Register_Click"/>

                        <asp:Label ID="lbl_AlredyUser" runat="server" CssClass="pl-3 text-black-100" Text="Already registered? <a href='login.aspx' class='badge badge-info'>Login here... </a>"></asp:Label>
                    </div>
                </div>
                <div class="row p-5">
                    <div style="align-items:center">
                        <asp:Image ID="img_user" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
