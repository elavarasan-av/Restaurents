using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maran_Restaurents.Admin;

namespace Maran_Restaurents.User
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategory();
                getProducts();
            }
        }

        private void getCategory()
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Category_CURD", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "ACTIVECATE");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_MenuCate.DataSource = dt;
            rep_MenuCate.DataBind();           
        }

        private void getProducts()
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Product_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "ACTIVEPROD");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_MenuProducts.DataSource = dt;
            rep_MenuProducts.DataBind();
           
        }

        protected void rep_MenuProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["UserId"] != null)
            {
                bool isCartItemUpdated = false;
                int i = isItemExistInCart(Convert.ToInt32(e.CommandArgument));
                if (i == 0)
                {
                    // Adding new item in cart
                    SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
                    SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@productId", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@Quantity", 1);
                    cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Response.Write("<script>alert('Error - " + ex.Message + "');</script>");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    // Adding Existing item into cart
                    Utils utils = new Utils();
                    isCartItemUpdated = utils.updateCartQty(i + 1, Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["UserId"]));
                   
                }
                lbl_MsgMenu.Visible = true;
                lbl_MsgMenu.Text = "Item Added Successfully in your cart!";
                lbl_MsgMenu.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Cart.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        int isItemExistInCart(int productId)
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int quantity = 0;
            if (dt.Rows.Count > 0)
            {
                quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            }
            return quantity;
        }



        //public string LowerCase(object obj)
        //{
        //    return obj.ToString().ToLower();
        //}
    }
}