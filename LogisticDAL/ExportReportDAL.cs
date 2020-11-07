using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.DAL
{

    public class ExportReportDAL
    {
     
        public DataTable GetConsigneeWiseDailyReceiving(int ConsigneeId, DateTime fromDate, DateTime toDate,string ContSize, string containerNo)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("EXPORT_ConsigneeWise_DailyReceiving", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value );
                    cmd.Parameters.AddWithValue("@ContainerNo", containerNo);
                    cmd.Parameters.AddWithValue("@ContainerSize", ContSize);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="ConsigneeId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public DataTable GetDailyStuffingDetailsConsinee(int clientId,int flg, DateTime fromDate, DateTime toDate, string ContSize, string ContType, string containerNo)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Export_DaililyContainerStuffingSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", clientId > 0 ? (object)clientId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@SortBy", flg);
                    cmd.Parameters.AddWithValue("@ContainerNo", containerNo);
                    cmd.Parameters.AddWithValue("@ContainerSize", ContSize);
                    cmd.Parameters.AddWithValue("@ContainerType", ContType);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        
        public DataTable GetConsigneeWiseDailyStuffing(int ConsigneeId,int ShipperId,int frieghtForwarderId,string EFRNO, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("EXPORT_ConsigneeWise_DailyStuffing", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ShipperId", ShipperId > 0 ? (object)ShipperId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@frieghtForwarderId", frieghtForwarderId > 0 ? (object)frieghtForwarderId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EFRNO", EFRNO);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataSet GetConsigneeWiseDailyStatus(int ConsigneeId, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("EXPORT_ConsigneeWise_DailyStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    return ds;
                }
            }
        }


        public  DataTable GetContainerHistory(long CSDId)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("GetContainerHistory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CSDId", CSDId);                   
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