using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;
using LOGISTIC.UserDefinedModel;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.CSD.DAL
{

    public class CSDReportDAL
    {

        public DataTable GetDailyInwardMovementSummary( int custId, DateTime fromDate, DateTime toDate, int SizeId, int TypeId)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_MLO_DailyInwardMovementSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@SizeId", SizeId > 0 ? (object)SizeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TypeId", TypeId > 0 ? (object)TypeId : DBNull.Value);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataTable GetDailyOutwardMovementSummary(int custId, DateTime fromDate, DateTime toDate, int SizeId, int TypeId)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_MLO_DailyOutwardMovementSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    cmd.Parameters.AddWithValue("@SizeId", SizeId > 0 ? (object)SizeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TypeId", TypeId > 0 ? (object)TypeId : DBNull.Value);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataTable GetDailyStockSummary(int custId)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_MLO_DailyStockSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);                   
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

    }
}