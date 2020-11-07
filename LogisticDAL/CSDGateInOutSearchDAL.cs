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

    public class CSDGateInOutSearchDAL
    {

        public DataTable GetFilteredCSDGateInOut(int custId, DateTime fromDate, DateTime toDate, int SortBy, string contNumber, int size, int type, int comeFrom, int outTo, int trailerIn, int trailerOut)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSD_DateRange_FilterReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", custId > 0 ? (object)custId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                    cmd.Parameters.AddWithValue("@SortBy", SortBy);
                    cmd.Parameters.AddWithValue("@ContainerNo", contNumber.Length > 0 ? (object)contNumber : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", size > 0 ? (object)size : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", type > 0 ? (object)type : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DepotFrom", comeFrom > 0 ? (object)comeFrom : DBNull.Value);
                    cmd.Parameters.AddWithValue("@DepotTo", outTo > 0 ? (object)outTo : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrailerInId", trailerIn > 0 ? (object)trailerIn : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrailerOutId", trailerOut > 0 ? (object)trailerOut : DBNull.Value);
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