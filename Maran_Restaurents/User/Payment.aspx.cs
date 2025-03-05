using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maran_Restaurents.User
{
    public partial class Payment : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(Connection.GetConnectionString());

        SqlCommand cmd;
        SqlDataReader dr, dr1;
        DataTable dt;
        SqlTransaction transaction = null;

        string _name = string.Empty; string _cardNo = string.Empty; string _expiryDate = string.Empty; string _cvv = string.Empty; string _address = string.Empty; string _paymentMode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
              
            }
        }

        protected void lkbtn_CardSubmit_Click(object sender, EventArgs e)
        {
            _name = txt_Name.Text.Trim();
            _cardNo = txt_CardNo.Text.Trim();
            _cardNo = string.Format("************{0}", txt_CardNo.Text.Trim().Substring(12, 4));
            _expiryDate = txt_ExpMonth.Text.Trim() + "/" + txt_ExpYear.Text.Trim();
            _cvv = txt_Cvv.Text.Trim();
            _address = txt_Address.Text.Trim();
            _paymentMode = "card";
            if (Session["UserId"] != null)
            {
                Orderpaymand(_name, _cardNo, _expiryDate, _cvv, _address, _paymentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void lkbtn_CodSubmit_Click(object sender, EventArgs e)
        {
            _address = txt_CodAddress.Text.Trim();
            _paymentMode = "cod";
            if (Session["UserId"] != null)
            {
                Orderpaymand(_name, _cardNo, _expiryDate, _cvv, _address, _paymentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        void Orderpaymand(string name,string cardNo,string expiryDate,string cvv,string address,string paymentMode)
        {
           
            int paymentId; int productId;int quantity;
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]{
                new DataColumn("OrderNo",typeof(string)),
                new DataColumn("productId",typeof(int)),
                new DataColumn("Quantity",typeof(int)),
                new DataColumn("UserId",typeof(int)),
                new DataColumn("Status",typeof(string)),
                new DataColumn("PaymentID",typeof(int)),
                new DataColumn("OrderDate",typeof(DateTime)),
            });
            conn.Open();
            #region Sql Transaction
             transaction = conn.BeginTransaction();
             cmd = new SqlCommand("sp_Save_Payment", conn,transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@CardNo", cardNo);
            cmd.Parameters.AddWithValue("@ExpriyDate", expiryDate);
            cmd.Parameters.AddWithValue("@CVVno", cvv);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
            cmd.Parameters.Add("@InsertedId", SqlDbType.Int);
            cmd.Parameters["@InsertedId"].Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                paymentId = Convert.ToInt32(cmd.Parameters["@InsertedId"].Value);

                #region Getting Cart Item's
                 cmd = new SqlCommand("sp_Cart_Curd", conn, transaction);
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                cmd.CommandType = CommandType.StoredProcedure;
                 dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    productId = (int)dr["ProductId"];
                    quantity = (int)dr["Quantity"];
                    //update product Quantity
                    UpdateQuantity(productId, quantity, transaction, conn);
                   
                    // delete cart item
                    DeleteCartItem(productId, transaction, conn);
                    
                    dt.Rows.Add(Utils.GetUniqueId(), productId, quantity, (int)Session["UserId"], "Pending", paymentId, Convert.ToDateTime(DateTime.Now));
                }
                dr.Close();
                #endregion Getting Cart Item's

                #region Order Details
                if (dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("sp_Save_Order", conn, transaction);
                    cmd.Parameters.AddWithValue("@tblOrders", dt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                #endregion Order Details

                transaction.Commit();
                lbl_MsgPay.Visible = true;
                lbl_MsgPay.Text = "Your Item Order Successfull!!!!";
                lbl_MsgPay.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Invoice.aspx?id=" + paymentId);
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception esx)
                {
                    Response.Write("<script>alert('" + esx.Message + "')</script>");
                }             
            }
            #endregion Sql Transaction
            finally
            {
                conn.Close();
            }
        }

        void UpdateQuantity(int _productId,int _quantity,SqlTransaction sqlTransaction,SqlConnection sqlConnection)
        {
            int dbQuantity;
             cmd = new SqlCommand("sp_Cart_Curd", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@productId", _productId);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                 dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    dbQuantity = (int)dr1["Quantity"];

                    if (dbQuantity > _quantity && dbQuantity > 2)
                    {
                        dbQuantity = dbQuantity - _quantity;
                         cmd = new SqlCommand("sp_Product_Curd", sqlConnection, sqlTransaction);
                        cmd.Parameters.AddWithValue("@Action", "QTYUPDATE");
                        cmd.Parameters.AddWithValue("@Quantity", _quantity);
                        cmd.Parameters.AddWithValue("@productId", _productId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                dr1.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        void DeleteCartItem(int _productId, SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            cmd = new SqlCommand("sp_Cart_Curd", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@productId", _productId);
            cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}