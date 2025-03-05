using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.User
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Response.Redirect("Dashboard.aspx");
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_Uname_login.Text.Trim() == "Admin" && txt_Pwd.Text.Trim() == "LTTE")
            {
                Session["admin"] = txt_Uname_login.Text.Trim();
                Response.Redirect("../Admin/Dashboard.aspx");
            }
            else
            {
                SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("sp_User_Curd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECTLOGIN");
                cmd.Parameters.AddWithValue("@Username", txt_Uname_login.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txt_Pwd.Text.Trim());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Session["Username"] = txt_Uname_login.Text.Trim();
                    Session["UserId"] = dt.Rows[0]["UserId"];
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lbl_Msg_Login.Visible = true;
                    lbl_Msg_Login.Text = "Invalid Credentials";
                    lbl_Msg_Login.CssClass = "alert alert-danger";
                }
            }
        }
    }
}