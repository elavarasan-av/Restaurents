using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Category";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getCategory();
                }               
            }
            lbl_Msg.Visible = false;           
        }

        protected void btn_AddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute=false;
            int categoryId = Convert.ToInt32(hdnId.Value);

            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Category_CURD", conn);
            cmd.Parameters.AddWithValue("@Action", categoryId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@Categories_id", categoryId);
            cmd.Parameters.AddWithValue("@cate_Name", txt_CatName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cb_IsActive.Checked);

            if (fup_CatImage.HasFile)
            {
                if (Utils.IsValidExtension(fup_CatImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fup_CatImage.FileName);
                    imagePath = "Images/Category/" + obj.ToString() + fileExtension;
                    fup_CatImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Please Select .jpg, .jpeg or .png image";
                    lbl_Msg.CssClass="alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if(isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    actionName = categoryId == 0 ? "inserted" : "updated";
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Category " + actionName + " successfully!!";
                    lbl_Msg.CssClass = "alert alert-success";
                    getCategory();
                    clear();
                   
                }
                catch(Exception ex)
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

        private void getCategory()
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Category_CURD", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_Categorys.DataSource = dt;
            rep_Categorys.DataBind();
            clear();

        }

        private void clear()
        {
            txt_CatName.Text = String.Empty;
            cb_IsActive.Checked = false;
            hdnId.Value = "0";
            btn_AddOrUpdate.Text = "Add";
            img_Category.ImageUrl = String.Empty;
        }
   
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            lbl_Msg.Visible = false;
            if (e.CommandName == "edit")
            {
                SqlCommand cmd = new SqlCommand("sp_Category_CURD", conn);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@Categories_id", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                txt_CatName.Text = dt.Rows[0]["cate_Name"].ToString();
                cb_IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                img_Category.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Image/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                img_Category.Height = 200;
                img_Category.Width = 200;
                hdnId.Value = dt.Rows[0]["Categories_id"].ToString();
                btn_AddOrUpdate.Text = "UPDATE";
                LinkButton btn = e.Item.FindControl("lkn_Edit") as LinkButton;
                btn.CssClass = "badge badge-warning";
            } 
            else if (e.CommandName == "delete")
            {
                SqlCommand cmd = new SqlCommand("sp_Category_CURD", conn);
                cmd.Parameters.AddWithValue("@Action","DELETE");
                cmd.Parameters.AddWithValue("@Categories_id", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Category Deleted Successfully!";
                    lbl_Msg.CssClass = "alert alert-success";
                    getCategory();
                }
                catch(Exception ex)
                {
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "Error-"+ex.Message;
                    lbl_Msg.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void rCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //cb_IsActive
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl = e.Item.FindControl("lbl_IsActive") as Label;
                if (lbl.Text == "True")
                {
                    lbl.Text = "Active";
                    lbl.CssClass = "badge badge-success";
                }
                else
                {
                    lbl.Text = "In-Active";
                    lbl.CssClass = "badge badge-danger";
                }
            }
        }
    }
}