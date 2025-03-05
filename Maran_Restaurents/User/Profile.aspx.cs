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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    getUserDetails();
                     getPurchaseHistory();

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
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rep_UserProfile.DataSource = dt;
                rep_UserProfile.DataBind();
                if (dt.Rows.Count == 1)
                {
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    Session["Email"] = dt.Rows[0]["Email"].ToString();
                    Session["ImageUrl"] = dt.Rows[0]["ImageUrl"].ToString();
                    Session["createDate"] = dt.Rows[0]["createDate"].ToString();                   
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:- " + ex.Message);
            }
        }

        void getPurchaseHistory()
        {
            int sr = 1;
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Invoice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "ODRHISTORY");
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("SrNo", typeof(Int32));
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dataRow in dt.Rows)
                {
                    dataRow["SrNo"] = sr;
                    sr++;
                }
            }
            if (dt.Rows.Count == 0)
            {
                rep_PurchesHistory.FooterTemplate = null;
                rep_PurchesHistory.FooterTemplate = new CustomTemplate(ListItemType.Footer);
            }
            rep_PurchesHistory.DataSource = dt;
            rep_PurchesHistory.DataBind();
        }
        protected void rep_PurchesHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {         
            double granTotal = 0;
            HiddenField paymentId = e.Item.FindControl("hdn_PaymentId") as HiddenField;
            Repeater rep_Order = e.Item.FindControl("rep_Order") as Repeater;

            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Invoice", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "INVOICBYID");
            cmd.Parameters.AddWithValue("@PaymentId", Convert.ToInt32(paymentId.Value));
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    granTotal += Convert.ToDouble(dataRow["TotalPrice"]);
                }
            }
            DataRow dr = dt.NewRow();
            dr["TotalPrice"] = granTotal;
            dt.Rows.Add(dr);
            rep_Order.DataSource = dt;
            rep_Order.DataBind();
            }
        }

        // Custom template class to add conrols to the repeater's header, item and footer section
        private sealed class CustomTemplate : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public CustomTemplate(ListItemType type)
            {
                ListItemType = type;
            }
            public void InstantiateIn(Control Container)
            {
                if (ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td><b>Hungry! Why not order food for you.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Click to Order</a></td></tr></tbody></table>");
                    Container.Controls.Add(footer);
                }
            }
        }

       
    }
}