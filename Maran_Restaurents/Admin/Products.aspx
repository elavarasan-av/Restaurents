<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Maran_Restaurents.Admin.Products" %>

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
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#<%=img_Products.ClientID%>").prop("src", e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
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
                                        <div class="col-sm-6 col-md-4 col-lg-4">
                                            <h4 class="sub-title">Products</h4>
                                            <div>
                                                <div class="form-group">
                                                    <label>Products Name</label>
                                                    <div>
                                                        <asp:TextBox ID="txt_ProdName" class="form-control" runat="server" placeholder="Enter Products Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Conform Enter The Products Name" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_ProdName"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="hdnId" runat="server" Value="0" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Products Discription</label>
                                                    <div>
                                                        <asp:TextBox ID="txt_Discription" class="form-control" runat="server" placeholder="Enter Products Discription" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Conform Enter The Discription" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Discription"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Products Price</label>
                                                    <div>
                                                        <asp:TextBox ID="txt_Products_Price" class="form-control" runat="server" placeholder="Enter Products Price"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Conform Enter The Products Price" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Products_Price"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Price Must be in decimal" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Products_Price" ValidationExpression="^\d{0,8}(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Products Qunatity</label>
                                                    <div>
                                                        <asp:TextBox ID="txt_Products_Qty" class="form-control" runat="server" placeholder="Enter Products Qunatity"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Conform Enter The Products Qunatity" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Products_Qty"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Products Image</label>
                                                    <div>
                                                        <asp:FileUpload ID="fup_ProdImage" runat="server" class="form-control" onchange="ImagePreview(this);" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Products Category</label>
                                                    <div>

                                                        <asp:DropDownList ID="ddl_Cate_Prod" runat="server" class="form-control" DataSourceID="SqlData_Category" DataTextField="cate_Name" DataValueField="Categories_id" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Select Category</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="select the Categorys" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddl_Cate_Prod" InitialValue="0"></asp:RequiredFieldValidator>
                                                        <asp:SqlDataSource ID="SqlData_Category" runat="server" ConnectionString="<%$ ConnectionStrings:db_maran2ConnectionString %>" ProviderName="<%$ ConnectionStrings:db_maran2ConnectionString.ProviderName %>" SelectCommand="SELECT [Categories_id], [cate_Name] FROM [tbl_Categories]"></asp:SqlDataSource>
                                                    </div>
                                                </div>

                                                <div class="form-check pl-4">
                                                    <asp:CheckBox ID="cb_IsActive_Prod" runat="server" Text="&nbsp; IsActive" class="form-check-input" />
                                                </div>

                                                <div class="pb-5">
                                                    <asp:Button ID="btn_AddOrUpdate_Prod" runat="server" Text="Add" class="btn btn-primary" OnClick="btn_AddOrUpdate_Prod_Click" />
                                                    &nbsp;
                                              <asp:Button ID="btn_Clear_Prod" runat="server" Text="Clear" class="btn btn-warning" CausesValidation="false" OnClick="btn_Clear_Prod_Click" />
                                                </div>
                                                <div>
                                                    <asp:Image ID="img_Products" runat="server" class="img-thumbnail" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-6 col-md-8 col-lg-8 mobile-inputs">
                                            <h4 class="sub-title">Products Lists</h4>
                                            <div class="card-block table-border-style">
                                                <div class="table-responsive">

                                                    <asp:Repeater ID="rep_Products" runat="server" OnItemCommand="rep_Products_ItemCommand" OnItemDataBound="rep_Products_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <table class="table data-table-export table-hover nowrap">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="table-plus">Name</th>
                                                                        <th>Image</th>
                                                                        <th>Price</th>
                                                                        <th>Quantity</th>
                                                                        <th>Category</th>
                                                                        <th>IsActive</th>
                                                                        <th>Discription</th>
                                                                        <th>CreatedDate</th>
                                                                        <th class="datatable-nosort">Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="table-plus"><%# Eval("ProductName") %></td>
                                                                <td>
                                                                    <img alt="" width="50" src="<%# Utils.GetImageURL(Eval("ImageUrl")) %>" />
                                                                </td>
                                                                <td><%# Eval("Price") %></td>
                                                                <td>
                                                                    <asp:label id="lbl_Prod_Qty" runat="server" text=' <%# Eval("Quantity") %>'></asp:label>
                                                                </td>
                                                                <td><%# Eval("CategoryName") %></td>
                                                                <td>
                                                                    <asp:label id="lbl_IsActive_Prod" runat="server" text=' <%# Eval("IsActive") %>'></asp:label>
                                                                </td>
                                                                <td><%# Eval("Description") %></td>
                                                                <td><%# Eval("createDate") %></td>
                                                                <td>

                                                                    <asp:LinkButton ID="lkn_Edit_Prod" Text="Edit" runat="server" class="badge badge-primary" CausesValidation="false" CommandArgument='<%# Eval("productId") %>' CommandName="edit"> <i class="ti-pencil"> </i> </asp:LinkButton>

                                                                    <asp:LinkButton ID="lnk_Delete_Prod" Text="Delete" runat="server" CommandName="delete" class="badge bg-danger" CausesValidation="false" CommandArgument='<%# Eval("productId") %>' OnClientClick="return confirm('Do You want to delete this Products?');"> <i class="ti-trash"> </i> </asp:LinkButton>

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
