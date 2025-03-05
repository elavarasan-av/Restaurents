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
    public partial class Report : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Selling Report";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/Login.aspx");
                }
                
            }
        }

        private void getReportData(DateTime fromDate,DateTime toDate)
        {
            double grandTotal = 0;
            SqlCommand cmd = new SqlCommand("sp_SellingReport", conn);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    grandTotal += Convert.ToDouble(dr["TotalPrice"]);
                }
                lbl_Total.Text = "Sold Cost:" + grandTotal;
                lbl_Total.CssClass = "badge badge-primary";
            }
            rep_Selling.DataSource = dt;
            rep_Selling.DataBind();

        }

        protected void btn_Serch_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime(txt_FromDate.Text);
            DateTime toDate = Convert.ToDateTime(txt_ToDate.Text);
            if (toDate > DateTime.Now)
            {
                Response.Write("<acript>alert('ToDate cannot br greater then current date!');</script>");
            }
            else if (fromDate > toDate)
            {
                Response.Write("<acript>alert('FromDate cannot br greater then Todate!');</script>");
            }
            else
            {
                getReportData(fromDate, toDate);
            }
        }
    }
}