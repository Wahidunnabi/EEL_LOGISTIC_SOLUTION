using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.DAL
{
 public class TRDAL
    {
   
     public List<TRReportData> Getall()
     {
         using (var context = new Logisticentities( ))
         {

                var Data = context.TRReportDatas.OrderByDescending(x => x.ID).Take(100).ToList();
                return Data;
         }
     }

     public string GetMLOWiseTRNumber(int custId)
        {
            using (var context = new Logisticentities())
            {

                var count = context.TRReportDatas.Where(x => x.MLOId == custId).Count();
                count = count + 1;
                return "ELL/TR/" + DateTime.Now.Date.Year + "/" + count;
            }
        }
     public object Insert(TRReportData objTR)
     {
              
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.TRReportDatas.Add(objTR);
                    context.SaveChanges();
                    return "Data has been saved successfully.";
                }                
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
     }
     public object Update(TRReportData objTR)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.TRReportDatas.Where(x => x.ID == objTR.ID).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objTR);
                    context.SaveChanges();
                    return "Data has been updated successfully.";
                }
            }           
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                return errorMessages;
            }

        }


     public DataTable GetTRData(int TRId)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("EXPORT_TRReport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TRId", TRId);                  
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public object Delete(TRReportData objTR)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.TRReportDatas.Remove(context.TRReportDatas.Single(x => x.ID == objTR.ID));
                    context.SaveChanges();
                    return "Data has been deleted successfully. !!";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
        }

    }
 
}
