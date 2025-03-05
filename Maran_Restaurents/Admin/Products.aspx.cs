using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Products";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getProducts();
                }
               
            }
            lbl_Msg.Visible = false;
        }

        protected void btn_AddOrUpdate_Prod_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int productId = Convert.ToInt32(hdnId.Value);

            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Product_Curd", conn);
            cmd.Parameters.AddWithValue("@Action", productId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@ProductName", txt_ProdName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txt_Discription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txt_Products_Price.Text.Trim());
            cmd.Parameters.AddWithValue("@Quantity", txt_Products_Qty.Text.Trim());
            cmd.Parameters.AddWithValue("@Categories_id", ddl_Cate_Prod.SelectedValue );
            cmd.Parameters.AddWithValue("@IsActive", cb_IsActive_Prod.Checked);

            if (fup_ProdImage.HasFile)
            {
                if (Utils.IsValidExtension(fup_ProdImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fup_ProdImage.FileName);
                    imagePath = "Images/Products/" + obj.ToString() + fileExtension;
                    fup_ProdImage.PostedFile.SaveAs(Server.MapPath("~/Images/Products/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Please Select .jpg, .jpeg or .png image";
                    lbl_Msg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    actionName = productId == 0 ? "inserted" : "updated";
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Products " + actionName + " successful!!";
                    lbl_Msg.CssClass = "alert alert-success";
                    getProducts();
                    clear();

                }
                catch (Exception ex)
                {
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Error- " + ex.Message;
                    lbl_Msg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void getProducts()
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Product_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_Products.DataSource = dt;
            rep_Products.DataBind();
            clear();

        }

        private void clear()
        {
            txt_ProdName.Text = String.Empty;
            txt_Discription.Text = String.Empty;
            txt_Products_Price.Text = String.Empty;
            txt_Products_Qty.Text = String.Empty;
            ddl_Cate_Prod.ClearSelection();
            cb_IsActive_Prod.Checked = false;
            hdnId.Value = "0";
            btn_AddOrUpdate_Prod.Text = "Add";
            img_Products.ImageUrl = String.Empty;
        }

        protected void btn_Clear_Prod_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rep_Products_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            lbl_Msg.Visible = false;
            if (e.CommandName == "edit")
            {
                SqlCommand cmd = new SqlCommand("sp_Product_Curd", conn);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@productId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                txt_ProdName.Text = dt.Rows[0]["ProductName"].ToString();
                txt_Discription.Text = dt.Rows[0]["Description"].ToString();
                txt_Products_Price.Text = dt.Rows[0]["Price"].ToString();
                txt_Products_Qty.Text = dt.Rows[0]["Quantity"].ToString();
                ddl_Cate_Prod.SelectedValue = dt.Rows[0]["Categories_id"].ToString();
                cb_IsActive_Prod.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                img_Products.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Image/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                img_Products.Height = 200;
                img_Products.Width = 200;
                hdnId.Value = dt.Rows[0]["productId"].ToString();
                btn_AddOrUpdate_Prod.Text = "UPDATE";
                LinkButton btn = e.Item.FindControl("lkn_Edit_Prod") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
            else if (e.CommandName == "delete")
            {
                SqlCommand cmd = new SqlCommand("sp_Product_Curd", conn);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@productId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Products Deleted Successfully!";
                    lbl_Msg.CssClass = "alert alert-success";
                    getProducts();
                }
                catch (Exception ex)
                {
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Error-" + ex.Message;
                    lbl_Msg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void rep_Products_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_IsActive = e.Item.FindControl("lbl_IsActive_Prod") as Label;
                Label lbl_Qty = e.Item.FindControl("lbl_Prod_Qty") as Label;
                if (lbl_IsActive.Text == "True")
                {
                    lbl_IsActive.Text = "Active";
                    lbl_IsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lbl_IsActive.Text = "In-Active";
                    lbl_IsActive.CssClass = "badge badge-danger";
                }
                if (Convert.ToInt32(lbl_Qty.Text) <= 5)
                {
                    lbl_Qty.CssClass = "badge badge-danger";
                    lbl_Qty.ToolTip = "Item about to be 'Out of Stock '!";
                }
            }
        }
    }
}