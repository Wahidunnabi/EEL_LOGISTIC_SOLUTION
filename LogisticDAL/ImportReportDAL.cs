using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.DAL
{

    public class ImportReportDAL
    {

        public DataSet GetMLOWiseDailyReport( int custId, DateTime fromDate, DateTime toDate )
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Import_MLOWise_DailyReport", con))
                {

                    //"Import_MLO_Wise_DailyInOutStock"
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId);
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


        public DataSet GetMLOWiseImportSummaryReport(int custId, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Import_MLOWise_ImportSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId);
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


        public DataTable GetContainerSummaryReport(int custId, int sortBy, DateTime fromDate, DateTime toDate, string ContainerNo,string ContainerSize)
        {
            // ok for Load
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Import_ContainetSummary", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                    cmd.Parameters.AddWithValue("@ContainerSize", ContainerSize);
                    cmd.Parameters.AddWithValue("@SortBy", sortBy);
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

    }
}