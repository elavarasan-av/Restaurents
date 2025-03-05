using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.User
{
    public partial class user : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                form1.Attributes.Add("class", "sub_page");
            }
            else
            {
                form1.Attributes.Remove("class");
                Control SliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");
                pnlSliderUC.Controls.Add(SliderUserControl);
            }
            if (Session["UserId"] != null)
            {
                lkbtn_LoginOrout.Text = "Logout";
                Utils utils = new Utils();
                Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["UserId"]));
            }
            else
            {
                lkbtn_LoginOrout.Text = "Login";
                Session["cartCount"] = "0";
            }
        }

        protected void lkbtn_LoginOrout_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        protected void lkbtn_RegOrProf_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                lkbtn_RegOrProf.ToolTip = "User Profile";
                Response.Redirect("Profile.aspx");
               
            }
            else
            {
                lkbtn_RegOrProf.ToolTip = "User Registration";
                Response.Redirect("Registration.aspx");
            }
        }
    }
}