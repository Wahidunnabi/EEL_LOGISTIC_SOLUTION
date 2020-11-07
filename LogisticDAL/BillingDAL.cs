using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Configuration;

namespace LOGISTIC.DAL
{
    public class BillingDAL
    {

        public List<Service> GetAllService()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.Services.OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public string GetInvoiceNumber(string Configtype)
        {


            string exist = null;
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constring);

            conn.Open();
            SqlCommand comm = new SqlCommand("sp_GetPatternConfig", conn);
            comm.Parameters.AddWithValue("ConfigType", Configtype);
            comm.CommandType = CommandType.StoredProcedure;

            //returning the output

            exist = (string)comm.ExecuteScalar();

            return exist;

        }
        public List<ChartOfService> GetAllChartofService()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServices.Where(x=>x.IsTransaction == true).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public List<ChartOfServiceCategory> GetAllServiceCategory()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServiceCategories.OrderBy(x => x.CategoryName).ToList();
                return objService;
            }
        }


        public List<ChartOfService> GetAllServiceByCategoryId(int catid)
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServices.Where(x=>x.CateId == catid && x.IsTransaction==true).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }


        public List<CSDBillSummary> GetAllCSDBillSummary()
        {
            using (var context = new Logisticentities())
            {
                var listBillSummary = context.CSDBillSummaries.OrderByDescending(x => x.Id).ThenBy(x=>x.CustId).ThenBy(x=>x.Size).Take(300).ToList();
                return listBillSummary;
            }
        }
     
        public List<CSDBillSummary> GetCSDBillSummaryByMLO(int MLOId, DateTime fromdate, DateTime Todate )
        {
            using (var context = new Logisticentities())
            {
                var listBillSummary = context.CSDBillSummaries.Where(x=>x.CustId== MLOId && x.BillFrom >= fromdate.Date && x.BillTo <= Todate.Date).ToList();
                return listBillSummary;
            }
        }
        public List<CSDBillSummary> GetCSDBillSummaryByMLO(int MLOId)
        {
            using (var context = new Logisticentities())
            {
                var listBillSummary = context.CSDBillSummaries.Where(x => x.CustId == MLOId).ToList();
                return listBillSummary;
            }
        }

        public List<CSDBillSummary> GetCSDBillSummaryByRefNo(string refNo)
        {
            using (var context = new Logisticentities())
            {
                var listBillSummary = context.CSDBillSummaries.Where(x => x.SummaryRefNo == refNo).ToList();
                return listBillSummary;
            }
        }

        public List<CSDBillSummary> GetCSDBillSummaryByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (var context = new Logisticentities())
            {
                var listBillSummary = context.CSDBillSummaries.Where(x => x.BillFrom >= fromDate.Date && x.BillTo <= toDate.Date).ToList();
                return listBillSummary;
            }
        }

        public List<CSDBillDetail> GetAllCSDBillDetailsByDateRange()
        {
            using (var context = new Logisticentities())
            {
                var listBillDetails = context.CSDBillDetails.OrderBy(x => x.CustId).ToList();
                return listBillDetails;
            }
        }
        public DataTable GetAllCSDBillDetailsByMlOId(int CusId, DateTime fromDate, DateTime Todate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBillDetailsExport", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CusId",CusId > 0 ? (object)CusId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", Todate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataTable GetAllCSDBillShortSummeryByMlOId(int CusId, DateTime fromDate, DateTime Todate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBillDetailsExportToGetSummery", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CusId", CusId > 0 ? (object)CusId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", Todate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public DataTable GetAllCSDBillDetails(int CusId, DateTime fromDate, DateTime Todate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBillDetailsExport", con))
                {
                    
                    //CSDMonthlyBillDetailsExport
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CusId", CusId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", Todate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataSet GetAllCSDBillDetailswithTop(int CusId, DateTime fromDate, DateTime Todate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("CSDMonthlyBillDetailsExportwithTop", con))
                {

                    //CSDMonthlyBillDetailsExport
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@ConsigneeId", ConsigneeId > 0 ? (object)ConsigneeId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CusId", CusId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", Todate);
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
        public List<CSDBillDetail> GetClientCSDBillDetailsByRefNo(int customerId, string refNo)
        {
            using (var context = new Logisticentities())
            {
                var listBillDetails = context.CSDBillDetails.Where(x=>x.CustId==customerId).Where(x=>x.SummaryRefNo == refNo).ToList();
                return listBillDetails;
            }
        }


        #region Service Details(Rate) Setup

        public List<ServiceDetail> GetAllServiceDetails()
        {
            using (var context = new Logisticentities())
            {
                var objList = context.ServiceDetails.Include("Service").ToList();
                return objList;
            }         
        }

        public ServiceDetail GetServiceDetailById( int id)
        {
            using (var context = new Logisticentities())
            {
                var objServiceDetail = context.ServiceDetails.Where(x=>x.ServiceDetailsId == id).SingleOrDefault();
                return objServiceDetail;
            }
        }

        public ServiceDetail GetServiceDetailByServiceId(int id)
        {
            // for those service that does not depen on size or others
            using (var context = new Logisticentities())
            {
                var objServiceDetail = context.ServiceDetails.Where(x => x.ServiceId == id).SingleOrDefault();
                return objServiceDetail;
            }
        }
        public ServiceDetail GetServiceDetailByServiceIdwithSizeId(int serviceId,int SizeId)
        {
            using (var context = new Logisticentities())
            {
                var objServiceDetail = context.ServiceDetails.Where(x => x.ServiceId == serviceId && x.ContSizeId == SizeId).SingleOrDefault();
                return objServiceDetail;
            }
        }
        public object InsertServiceDetail(ServiceDetail objServiceDetail)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ServiceDetails.Add(objServiceDetail);
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

        public object UpdateServiceDetail(ServiceDetail objServiceDetail)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var obj = context.ServiceDetails.Where(x => x.ServiceDetailsId == objServiceDetail.ServiceDetailsId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objServiceDetail);
                    context.SaveChanges();
                    return "Data has been updated successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }

        public object DeleteServiceDetail(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ServiceDetails.Remove(context.ServiceDetails.Single(x => x.ServiceDetailsId == Id));
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


        #endregion


        #region IMPORT Bill Setup

        public ImportBill GetImportBillById(int id)
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ImportBills.Include("ImportBillDetails").Where(s => s.ID == id).SingleOrDefault();
                return objService;
            }
        }


        public List<Service> GetAllImportServices()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.Services.Where(s=>s.ServiceCategory==1).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public List<Slab> GetAllSlab()
        {
            using (var context = new Logisticentities())
            {
                var lstSlab = context.Slabs.ToList();
                return lstSlab;
            }
        }

        public List<ImpDetentionCharg> GetAllDetentionCharg()
        {
            using (var context = new Logisticentities())
            {
                var objList = context.ImpDetentionChargs.ToList();
                return objList;
            }
        }

        public object InsertImportBill(ImportBill objBill)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ImportBills.Add(objBill);
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

        public object UpdateImportBill(ImportBill objBill)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldBillObj = context.ImportBills.Include("ImportBillDetails").Where(x => x.ID == objBill.ID).SingleOrDefault();

                    context.Entry(oldBillObj).CurrentValues.SetValues(objBill);

                    foreach (var item in oldBillObj.ImportBillDetails.ToList())
                    {
                        //Delete IGM Details that has been removed from IGM Import

                        if (!objBill.ImportBillDetails.Any(s => s.ID == item.ID))
                            context.ImportBillDetails.Remove(item);
                    }
                    foreach (var item in objBill.ImportBillDetails)
                    {

                        //New item ...so add this item
                        if (item.ID == 0)
                        {
                            context.ImportBillDetails.Add(item);
                        }
                        else
                        {
                            //Existing item ...then update this item
                            var oldMRDetails = oldBillObj.ImportBillDetails.Where(s => s.ID == item.ID).SingleOrDefault();
                            context.Entry(oldMRDetails).CurrentValues.SetValues(item);
                        }
                    }

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

        public object DeleteImportBill(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ImportBills.Remove(context.ImportBills.Single(x => x.ID == Id));
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

        public object InsertDetentionCharg(ImpDetentionCharg objChrgSetting)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ImpDetentionChargs.Add(objChrgSetting);
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

        public object UpdateDetentionCharg(ImpDetentionCharg objChrgSetting)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var obj = context.ImpDetentionChargs.Where(x => x.ID == objChrgSetting.ID).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objChrgSetting);
                    context.SaveChanges();
                    return "Data has been updated successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }

        public object DeleteDetentionCharg(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ImpDetentionChargs.Remove(context.ImpDetentionChargs.Single(x => x.ID == Id));
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


        public DataTable GetDeliveryPackageCharge(int IgmIdAgainestBl, int serviceId, DateTime UptoDate,decimal servicerate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("ImportDeliveryPackageCalculation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IGMIdAgainstBl", IgmIdAgainestBl);
                    cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                    cmd.Parameters.AddWithValue("@RateBDT", servicerate);
                    cmd.Parameters.AddWithValue("@UptoDate", UptoDate);
                   

                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataTable GetDetentionCharg(int IGMId, int freeDays, DateTime uptodate,  decimal rateInTk)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("ImportDetentionCalculation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IGMId", IGMId);
                    cmd.Parameters.AddWithValue("@freeday", freeDays);
                    cmd.Parameters.AddWithValue("@UptoDate", uptodate);
                    cmd.Parameters.AddWithValue("@RateInTk", rateInTk);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        #endregion



        #region EXPORT Bill Setup

        public ExportBill GetExportBillById(int id)
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ExportBills.Include("ExportBillDetails").Where(s => s.ID == id).SingleOrDefault();
                return objService;
            }
        }
        public List<Service> GetAllExportServices()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.Services.Where(s => s.ServiceCategory == 3).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public List<ExportServiceDetail> GetAllExportServiceDetail()
        {
            using (var context = new Logisticentities())
            {
                var lstServiceDtls = context.ExportServiceDetails.ToList();
                return lstServiceDtls;
            }
        }

        public object InsertExportServiceDetail(ExportServiceDetail objServiceDetail)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ExportServiceDetails.Add(objServiceDetail);
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

        public object UpdateExportServiceDetail(ExportServiceDetail objServiceDetail)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var obj = context.ExportServiceDetails.Where(x => x.ServiceDetailsId == objServiceDetail.ServiceDetailsId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objServiceDetail);
                    context.SaveChanges();
                    return "Data has been updated successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }

        public object DeleteExportServiceDetail(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ExportServiceDetails.Remove(context.ExportServiceDetails.Single(x => x.ServiceDetailsId == Id));
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

        public object InsertExportBill(ExportBill objBill)
        {
            objBill.RefNo = "EXP/" + DateTime.Now.ToString("dd") + '-' + DateTime.Now.ToString("MM") + '-' + DateTime.Now.ToString("yy") + '/' + DateTime.Now.Hour + DateTime.Now.Minute;

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ExportBills.Add(objBill);
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

        public object UpdateExportBill(ExportBill objBill)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldBillObj = context.ExportBills.Include("ExportBillDetails").Where(x => x.ID == objBill.ID).SingleOrDefault();

                    context.Entry(oldBillObj).CurrentValues.SetValues(objBill);

                    foreach (var item in oldBillObj.ExportBillDetails.ToList())
                    {
                        //Delete IGM Details that has been removed from IGM Import

                        if (!objBill.ExportBillDetails.Any(s => s.ID == item.ID))
                            context.ExportBillDetails.Remove(item);
                    }
                    foreach (var item in objBill.ExportBillDetails)
                    {

                        //New item ...so add this item
                        if (item.ID == 0)
                        {
                            context.ExportBillDetails.Add(item);
                        }
                        else
                        {
                            //Existing item ...then update this item
                            var oldMRDetails = oldBillObj.ExportBillDetails.Where(s => s.ID == item.ID).SingleOrDefault();
                            context.Entry(oldMRDetails).CurrentValues.SetValues(item);
                        }
                    }

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

        public object DeleteExportBill(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ExportBills.Remove(context.ExportBills.Single(x => x.ID == Id));
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

        public DataTable GetMLOWiseServiceCharg(int MLOId, int serviceId, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("ExportMLOWiseServiceBillCalculation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MLOId", MLOId);
                    cmd.Parameters.AddWithValue("@ServiceId", serviceId);
                    cmd.Parameters.AddWithValue("@Fromdate", fromDate);
                    cmd.Parameters.AddWithValue("@Todate", toDate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public DataTable GetExportStuffingDetails(int billType, int MloFfId,string EFRNO, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("ExportStuffingDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillType", billType);
                    cmd.Parameters.AddWithValue("@MloFordId", MloFfId);
                    cmd.Parameters.AddWithValue("@EFRNo", EFRNO);
                    
                    cmd.Parameters.AddWithValue("@Fromdate", fromDate);
                    cmd.Parameters.AddWithValue("@Todate", toDate);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public DataTable Export_MLO_FFarwarder_SummeryGrid(int MLOId, int FFID,  DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Export_MLO_FFarwarder_SummeryGrid ", con))
                {

                    //"@ClientId", custId > 0 ? (object)custId : DBNull.Value
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MLOID", MLOId > 0 ? (object)MLOId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FFId", FFID > 0 ? (object)FFID : DBNull.Value);
                 

                    cmd.Parameters.AddWithValue("@fromStuffingdate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@Tostuffingdate", toDate.Date);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public DataTable Export_MLO_FFarwarder_SummeryGridSum(int MLOId, int FFID, DateTime fromDate, DateTime toDate)
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Export_MLO_FFarwarder_SummeryGridSum", con))
                {

                    //"@ClientId", custId > 0 ? (object)custId : DBNull.Value
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MLOID", MLOId > 0 ? (object)MLOId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FFId", FFID > 0 ? (object)FFID : DBNull.Value);


                    cmd.Parameters.AddWithValue("@fromStuffingdate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@Tostuffingdate", toDate.Date);
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public List<EXPORT_EFRWiseBillDetails_Result> GetEFRWiseBillDetails(string EFRNo, DateTime fromDate, DateTime toDate)
        {
            using (var context = new Logisticentities())
            {
                var listCSD = context.EXPORT_EFRWiseBillDetails(EFRNo, fromDate, toDate).ToList();
                return listCSD;
            }
        }

        #endregion


    }

}
