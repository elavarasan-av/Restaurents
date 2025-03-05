using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                else
                {
                    DashboardCount dsbord = new DashboardCount();
                    Session["category"] = dsbord.Count("CATEGORY");
                    Session["products"] = dsbord.Count("PRODUCT");
                    Session["order"] = dsbord.Count("ORDER");
                    Session["delevery"] = dsbord.Count("DELIVERED");
                    Session["pending"] = dsbord.Count("PENDING");
                    Session["user"] = dsbord.Count("USER");
                    Session["soldAmount"] = dsbord.Count("SOLDAMOUNT");
                    Session["contact"] = dsbord.Count("CONTACT");
                }
            }
        }
    }
}