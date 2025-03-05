using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Users";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getUser();
                }
            }
        }

        private void getUser()
        {
            //SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_User_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECTADMIN");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_users.DataSource = dt;
            rep_users.DataBind();
            //clear();

        }

        protected void rep_users_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                SqlCommand cmd = new SqlCommand("sp_User_Curd", conn);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@UserId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "User Deleted Successfully!";
                    lbl_Msg.CssClass = "alert alert-success";
                    getUser();
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
    }
}