using Maran_Restaurents.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.User
{
    public partial class Cart : System.Web.UI.Page
    {
        private decimal grandTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getCartItem();
                }
            }

        }
        void getCartItem()
        {

            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_CartItem.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                rep_CartItem.FooterTemplate = null;
                rep_CartItem.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rep_CartItem.DataBind();
        }

        protected void rep_CartItem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Utils utils = new Utils();
            if (e.CommandName == "remove")
            {
                SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@productId",e.CommandArgument);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    getCartItem();
                    Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["UserId"]));
                }
                catch (Exception ex)
                {
                   Response.Write("<script>alert('Error - " + ex.Message + "');</script>");
                }
                finally
                {
                    conn.Close();
                }
            }
            if(e.CommandName == "updateCart")
            {
                bool isUpdated = false;
                for(int item = 0; item < rep_CartItem.Items.Count; item++)
                {
                    if (rep_CartItem.Items[item].ItemType == ListItemType.Item || rep_CartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox quantity = rep_CartItem.Items[item].FindControl("txt_Qty") as TextBox;
                        HiddenField _productId = rep_CartItem.Items[item].FindControl("hdn_ProdId") as HiddenField;
                        HiddenField _quantity = rep_CartItem.Items[item].FindControl("hdn_Qty") as HiddenField;
                        int quantityFromCart = Convert.ToInt32(quantity.Text);
                        int productId = Convert.ToInt32(_productId.Value);
                        int quantityFromDB = Convert.ToInt32(_quantity.Value);
                        bool isTrue = false;
                        int updatedQuantity = 1;
                        if(quantityFromCart > quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }
                        else if (quantityFromCart < quantityFromDB)
                        {
                            updatedQuantity = quantityFromCart;
                            isTrue = true;
                        }
                        if (isTrue)
                        {
                            // update cart item quantity in DB
                            isUpdated = utils.updateCartQty(updatedQuantity, productId , Convert.ToInt32(Session["UserId"]));
                        }
                    }
                }
                getCartItem();
            }

            if(e.CommandName == "checkout")
            {
                bool isTrue = false;
                string pName = string.Empty;
                //first will check item quantity
                for (int item = 0; item < rep_CartItem.Items.Count; item++)
                {
                    if (rep_CartItem.Items[item].ItemType == ListItemType.Item || rep_CartItem.Items[item].ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField _productId = rep_CartItem.Items[item].FindControl("hdn_ProdId") as HiddenField;
                        HiddenField _cartQuantity = rep_CartItem.Items[item].FindControl("hdn_Qty") as HiddenField;
                        HiddenField _prodQuantity = rep_CartItem.Items[item].FindControl("hdn_ProdQty") as HiddenField;
                        Label productName = rep_CartItem.Items[item].FindControl("lbl_NameCart") as Label;
                        int productId = Convert.ToInt32(_productId.Value);
                        int cartQuantity = Convert.ToInt32(_cartQuantity.Value);
                        int prodQuantity = Convert.ToInt32(_prodQuantity.Value);
                      
                        if (prodQuantity > cartQuantity && prodQuantity > 2)
                        {
                            isTrue = true;
                        }
                        else
                        {
                            isTrue = false;
                            pName = productName.Text.ToString();
                            break;
                        }
                    }
                }
                if (isTrue)
                {
                    Response.Redirect("Payment.aspx");
                }
                else
                {
                    lbl_MsgCart.Visible = true;
                    lbl_MsgCart.Text = "Item <b> '" + pName + "'</b> is out of stock!!!";
                    lbl_MsgCart.CssClass = "alert alert-danger";
                }
            }
        }

        protected void rep_CartItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label totalPrice = e.Item.FindControl("lbl_tolPrice") as Label;
                Label productPrice = e.Item.FindControl("lbl_PriceCart") as Label;
                TextBox quantity = e.Item.FindControl("txt_Qty") as TextBox;
                decimal calTotalPrice = Convert.ToDecimal(productPrice.Text) * Convert.ToDecimal(quantity.Text);
                totalPrice.Text = calTotalPrice.ToString();
                grandTotal += calTotalPrice;
            }
            Session["grandTotalPrice"] = grandTotal;
        }

        // Custom template class to add conrols to the repeater's header, item and footer section
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control Container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Your Cart is empty.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Continue Shopping</a></td></tr>");
                        Container.Controls.Add(footer);
                }
            }
        }
    }
}