using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.Admin
{
    public partial class OrderStatus : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Order Status";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getOrderStatus();
                }
            }
            lbl_MsgOderStatus.Visible = false;
            pan_UpdateOrStatus.Visible = false;
        }

        private void getOrderStatus()
        {
             conn = new SqlConnection(Connection.GetConnectionString());
             cmd = new SqlCommand("sp_Invoice", conn);
            cmd.Parameters.AddWithValue("@Action", "GETSTATUS");
            cmd.CommandType = CommandType.StoredProcedure;
             da = new SqlDataAdapter(cmd);
             dt = new DataTable();
            da.Fill(dt);
            rep_OrderStatus.DataSource = dt;
            rep_OrderStatus.DataBind();
        }


        protected void rep_OrderStatus_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lbl_MsgOderStatus.Visible = false;
            if (e.CommandName == "edit")
            {
                conn = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("sp_Invoice", conn);
                cmd.Parameters.AddWithValue("@Action", "STATUSBYID");
                cmd.Parameters.AddWithValue("@OrderDetialsID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                ddl_OrderStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                hdnId.Value = dt.Rows[0]["OrderDetialsID"].ToString();
                pan_UpdateOrStatus.Visible = true;
                LinkButton btn = e.Item.FindControl("lktbn_EditOredDeils") as LinkButton;
                btn.CssClass = "badge badge-warning";
            }
        }
        protected void btn_UpdateOrdDeils_Click(object sender, EventArgs e)
        {
            int OrderDetialsID = Convert.ToInt32(hdnId.Value);

             conn = new SqlConnection(Connection.GetConnectionString());
             cmd = new SqlCommand("sp_Invoice", conn);
            cmd.Parameters.AddWithValue("@Action", "UPDSTATUS");
            cmd.Parameters.AddWithValue("@Status", ddl_OrderStatus.Text);
            cmd.Parameters.AddWithValue("@OrderDetialsID", OrderDetialsID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                lbl_MsgOderStatus.Visible = true;
                lbl_MsgOderStatus.Text = "Order Status Updated successfully!!";
                lbl_MsgOderStatus.CssClass = "alert alert-success";
                getOrderStatus();               
            }
            catch (Exception ex)
            {
                lbl_MsgOderStatus.Visible = true;
                lbl_MsgOderStatus.Text = "Error- " + ex.Message;
                lbl_MsgOderStatus.CssClass = "alert alert-danger";
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btn_CancelOrdDeils_Click(object sender, EventArgs e)
        {
            pan_UpdateOrStatus.Visible = false;
        }
    }
}