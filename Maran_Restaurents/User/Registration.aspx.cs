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
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null) /* && Session["UserId"] != null)*/
                {
                    getUserDetails();
                }
                else if (Session["UserId"]!=null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btn_Register_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int UserId = Convert.ToInt32(Request.QueryString["id"]);
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_User_Curd", conn);
            cmd.Parameters.AddWithValue("@Action", UserId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@Name", txt_Name_NewUser .Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txt_Mobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txt_Email.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txt_Address.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txt_PostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txt_UserName.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txt_pwd.Text.Trim());

            if (fu_UserImage.HasFile)
            {
                if (Utils.IsValidExtension(fu_UserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fu_UserImage.FileName);
                    imagePath = "Images/User/" + obj.ToString() + fileExtension;
                    fu_UserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + obj.ToString() + fileExtension);
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
                    actionName = UserId == 0 ? 
                        "  Registration is Successful ! <b><a href='Login.aspx'>Click here</a></b> to do login" :
                        "  Details Update Successful !! <b><a href='Profile.aspx'>Can check here</a></b> ";
                    lbl_Msg.Visible = true;
                    lbl_Msg.Text = "<b>" + txt_UserName.Text.Trim() + "</b>" + actionName;
                    lbl_Msg.CssClass = "alert alert-success";
                    if (UserId != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    clear();
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lbl_Msg.Visible = true;
                        lbl_Msg.Text = "<b>" + txt_UserName.Text.Trim() + "</b> username already exist,try new one....!";
                        lbl_Msg.CssClass = "alert alert-danger";
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        void getUserDetails()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
                SqlCommand cmd = new SqlCommand("sp_User_Curd", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECTPROFILE");
                cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
               if(dt.Rows.Count > 0)
               {
                    txt_Name_NewUser.Text = dt.Rows[0]["Name"].ToString();
                   txt_Mobile.Text = dt.Rows[0]["Mobile"].ToString();
                   txt_Email.Text = dt.Rows[0]["Email"].ToString();
                   txt_Address.Text = dt.Rows[0]["Address"].ToString();
                   txt_PostCode.Text = dt.Rows[0]["PostCode"].ToString();
                   txt_UserName.Text = dt.Rows[0]["Username"].ToString();
                    txt_pwd.TextMode = TextBoxMode.SingleLine;
                    txt_pwd.ReadOnly = true;
                   txt_pwd.Text = dt.Rows[0]["Password"].ToString();
                    img_user.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                    img_user.Height = 200;
                    img_user.Width = 200;
               }
                lbl_HeaderMsg.Text = "<h2>Edit Profile</h2>";
                btn_Register.Text = "Update";
                lbl_AlredyUser.Text = "";
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Error - " + ex.Message + "');</script>");
            }
        }
  

        private void clear()
        {
            
            txt_Name_NewUser.Text = String.Empty;
            txt_Mobile.Text = String.Empty;
            txt_Email.Text = String.Empty;
            txt_Address.Text = String.Empty;
            txt_PostCode.Text = String.Empty;
            txt_UserName.Text = String.Empty;
            txt_pwd.Text = String.Empty;
            img_user.ImageUrl = String.Empty;
        }
        
    }
}