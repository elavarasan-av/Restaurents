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
    public partial class Contacts_ad : System.Web.UI.Page
    {
         
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlConnection conn = new SqlConnection(Connection.GetConnectionString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Contact Users";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    getContact();
                }
            }
        }

        private void getContact()
        {
            SqlCommand cmd = new SqlCommand("sp_Contact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rep_Contact.DataSource = dt;
            rep_Contact.DataBind();
          
        }

        protected void rep_Contact_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                cmd = new SqlCommand("sp_Contact", conn);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@ContactId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lbl_MsgCont.Visible = true;
                    lbl_MsgCont.Text = "Record Deleted Successfully!";
                    lbl_MsgCont.CssClass = "alert alert-success";
                    getContact();
                }
                catch (Exception ex)
                {
                    lbl_MsgCont.Visible = true;
                    lbl_MsgCont.Text = "Error-" + ex.Message;
                    lbl_MsgCont.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}