using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Maran_Restaurents.Admin;

namespace Maran_Restaurents
{
    public class Connection
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["LTTE"].ConnectionString;
        }
    }
    public class Utils
    {
        public static bool IsValidExtension(string fileName)
        {
            bool Isvalid = false;
            string[] fileExtension = { ".jpg", ".png", ".jpeg" };
            for (int i = 0; i <= fileExtension.Length - 1; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {
                    Isvalid = true;
                    break;
                }
            }
            return Isvalid;
        }
        public static string GetImageURL(object url)
        {
            string url1 = "";
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "..Images/No_image.png";
            }
            else
            {
                url1 = string.Format("../{0}", url);
            }
            return url1;
        }

        public bool updateCartQty(int quantity,int productId, int UserId)
        {
            bool isUpdated = false;
           
                SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                isUpdated = false;
                System.Web.HttpContext.Current.Response.Write("<script>alert('Error - " + ex.Message + "');</script>");
            }
            finally
            {
                conn.Close();
            }
            return isUpdated;
        }
        public int cartCount(int UserId)
        {
            SqlConnection conn = new SqlConnection(Connection.GetConnectionString());
            SqlCommand cmd = new SqlCommand("sp_Cart_Curd", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT");
            cmd.Parameters.AddWithValue("@UserId", UserId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count;
           
        }

        public static string GetUniqueId()
        {
            Guid guid = Guid.NewGuid();
            string uniquId = guid.ToString();
            return uniquId;
        }
    }

    public class DashboardCount
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;

        public int Count(string name)
        {
            int count = 0;
            conn = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("sp_DashBoard", conn);
            cmd.Parameters.AddWithValue("@Action", name);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
             dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0] == DBNull.Value)
                {
                    count = 0;
                }
                else
                {
                    count = Convert.ToInt32(dr[0]);
                }
            }
            dr.Close();
            conn.Close();
            return count;
        }
    }
}