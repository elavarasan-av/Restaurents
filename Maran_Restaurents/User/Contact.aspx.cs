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
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        //SqlDataAdapter da;
        //DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("sp_Contact", conn);
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@Name", txt_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txt_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@Subject", txt_Subject.Text.Trim());
                cmd.Parameters.AddWithValue("@Message", txt_Message.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                lbl_MsgCont.Visible = true;
                lbl_MsgCont.Text = "Thakns for reaching out will look into your query!";
                lbl_MsgCont.CssClass = "alert alert-success";
                clear();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        private void clear()
        {
            txt_Name.Text = string.Empty;
            txt_Email.Text = string.Empty;
            txt_Subject.Text = string.Empty;
            txt_Message.Text = string.Empty;
        }
    }
}