using System;
using System.Collections.Generic;
using LOGISTIC.DAL;
using System.Data;

namespace LOGISTIC.BLL
{
    public class BillingBLL
    {
        BillingDAL objDal = new BillingDAL();

        public List<Service> GetAllService()
        {
            List<Service> objlist = new List<Service>();
            objlist = objDal.GetAllService();
            return objlist;
        }
        public string GetImportInvoicenumber(string Configtype)
        {
            string result = objDal.GetInvoiceNumber(Configtype);
            return result;

        }
        public List<ChartOfService> GetAllChartofService()
        {
            List<ChartOfService> objlist = new List<ChartOfService>();
            objlist = objDal.GetAllChartofService();
            return objlist;
        }
        public List<ChartOfServiceCategory> GetAllServiceCategory()
        {
            List<ChartOfServiceCategory> objlist = new List<ChartOfServiceCategory>();
            objlist = objDal.GetAllServiceCategory();
            return objlist;
        }
        public List<ChartOfService> GetAllServiceByCategoryId(int catId)
        {
            List<ChartOfService> objlist = new List<ChartOfService>();
            objlist = objDal.GetAllServiceByCategoryId(catId);
            return objlist;
        }
        public List<CSDBillSummary> GetAllCSDBillSummary()
        {
            List<CSDBillSummary> objlist = new List<CSDBillSummary>();
            objlist = objDal.GetAllCSDBillSummary();
            return objlist;
        }
        public DataTable GetAllCSDBillShortSummeryByMlOId(int CusId,DateTime fromdate,DateTime Todate)
        {
           
            DataTable dt = objDal.GetAllCSDBillShortSummeryByMlOId(CusId, fromdate, Todate);
            return dt;
        }
        
        public List<CSDBillSummary> GetCSDBillSummaryByMLO(int MLOId, DateTime fromdate,DateTime Todate)
        {
            List<CSDBillSummary> objlist = new List<CSDBillSummary>();
            objlist = objDal.GetCSDBillSummaryByMLO(MLOId,fromdate,Todate);
            return objlist;
        }
        public List<CSDBillSummary> GetCSDBillSummaryByMLO(int MLOId)
        {
            List<CSDBillSummary> objlist = new List<CSDBillSummary>();
            objlist = objDal.GetCSDBillSummaryByMLO(MLOId);
            return objlist;
        }
        public List<CSDBillSummary> GetCSDBillSummaryByRefNo(string refNo)
        {
            List<CSDBillSummary> objlist = new List<CSDBillSummary>();
            objlist = objDal.GetCSDBillSummaryByRefNo(refNo);
            return objlist;
        }

        public List<CSDBillSummary> GetCSDBillSummaryByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<CSDBillSummary> objlist = new List<CSDBillSummary>();
            objlist = objDal.GetCSDBillSummaryByDateRange(fromDate, toDate);
            return objlist;
        }

        public List<CSDBillDetail> GetAllCSDBillDetailsByDateRange()
        {
            List<CSDBillDetail> objlist = new List<CSDBillDetail>();
            objlist = objDal.GetAllCSDBillDetailsByDateRange();
            return objlist;
        }
        public DataTable GetAllCSDBillDetailsByMLOId(int CusId, DateTime fromDate, DateTime Todate)
        {
           
            DataTable dt = objDal.GetAllCSDBillDetailsByMlOId(CusId, fromDate, Todate);
            return dt;
        }
        public List<CSDBillDetail> GetClientCSDBillDetailsByRefNo(int customerId, string refNo)
        {
            List<CSDBillDetail> objlist = new List<CSDBillDetail>();
            objlist = objDal.GetClientCSDBillDetailsByRefNo(customerId, refNo);
            return objlist;
        }
        public DataTable GetAllCSDBillDetails(int CusId, DateTime fromDate, DateTime Todate)
        {
            DataTable result = objDal.GetAllCSDBillDetails(CusId, fromDate, Todate); 
            return result;

           
        }
        public DataSet GetAllCSDBillDetailswithTop(int CusId, DateTime fromDate, DateTime Todate)
        {
            DataSet result = objDal.GetAllCSDBillDetailswithTop(CusId, fromDate, Todate);
            return result;


        }

        #region Service Details(Rate) Setup

        public List<ServiceDetail> GetAllServiceDetails()
        {
            List<ServiceDetail> objlist = new List<ServiceDetail>();
            objlist = objDal.GetAllServiceDetails();
            return objlist;
        }
        public ServiceDetail GetServiceDetailByServiceIdwithSizeId(int serviceId, int sizeid)
        {
           
            ServiceDetail objlist = objDal.GetServiceDetailByServiceIdwithSizeId(serviceId, sizeid);
            return objlist;
        }
        
        public ServiceDetail GetServiceDetailByServiceId(int id)
        {

            var objSetup = objDal.GetServiceDetailByServiceId(id);
            return objSetup;
        }

        public object InsertServiceDetail(ServiceDetail objSetup)
        {

            var status = objDal.InsertServiceDetail(objSetup);
            return status;
        }

        public object UpdateServiceDetail(ServiceDetail objSetup)
        {

            var status = objDal.UpdateServiceDetail(objSetup);
            return status;
        }

        public object DeleteServiceDetail(int Id)
        {

            var status = objDal.DeleteServiceDetail(Id);
            return status;

        }

        #endregion


        #region IMPORT Bill Setup
        //GetAllImportBillById
        public ImportBill GetImportBillById(int id)
        {
            var obj = objDal.GetImportBillById(id);
            return obj;
        }

        public List<Service> GetAllImportServices()
        {
            List<Service> objlist = new List<Service>();
            objlist = objDal.GetAllImportServices();
            return objlist;
        }
        public List<Slab> GetAllSlab()
        {
            List<Slab> lstSlab = new List<Slab>();
            lstSlab = objDal.GetAllSlab();
            return lstSlab;
        }

        public List<ImpDetentionCharg> GetAllDetentionCharg()
        {
            List<ImpDetentionCharg> lstChrg = new List<ImpDetentionCharg>();
            lstChrg = objDal.GetAllDetentionCharg();
            return lstChrg;
        }

        public object InsertImportBill(ImportBill objBill)
        {

            var status = objDal.InsertImportBill(objBill);
            return status;
        }

        public object UpdateImportBill(ImportBill objBill)
        {
            var status = objDal.UpdateImportBill(objBill);
            return status;
        }

        public object DeleteImportBill(int id)
        {
            var status = objDal.DeleteImportBill(id);
            return status;
        }

        public object InsertDetentionCharg(ImpDetentionCharg objChrgSetting)
        {

            var status = objDal.InsertDetentionCharg(objChrgSetting);
            return status;
        }

        public object UpdateDetentionCharg(ImpDetentionCharg objChrgSetting)
        {

            var status = objDal.UpdateDetentionCharg(objChrgSetting);
            return status;
        }

        public object DeleteDetentionCharg(int Id)
        {

            var status = objDal.DeleteDetentionCharg(Id);
            return status;

        }

        public DataTable GetDeliveryPackageCharge(int IGMId, int serviceId,DateTime uptodate, decimal servicerate)
        {
            DataTable result = objDal.GetDeliveryPackageCharge(IGMId, serviceId, uptodate, servicerate);
            return result;
        }
        
        public DataTable GetDetentionCharg(int IGMId, int freeDays, DateTime uptodate, decimal rateInTk)
        {
            DataTable result = objDal.GetDetentionCharg( IGMId, freeDays, uptodate, rateInTk);
            return result;
        }

        #endregion



        #region CSD Bill Setup




        #endregion



        #region EXPORT Bill Setup

        public ExportBill GetExportBillById(int id)
        {
            var obj = objDal.GetExportBillById(id);
            return obj;
        }
        public List<Service> GetAllExportServices()
        {
            List<Service> lstServices = new List<Service>();
            lstServices = objDal.GetAllExportServices();
            return lstServices;
        }

        public List<ExportServiceDetail> GetAllExportServiceDetail()
        {
            List<ExportServiceDetail> lstServiceDtls = new List<ExportServiceDetail>();
            lstServiceDtls = objDal.GetAllExportServiceDetail();
            return lstServiceDtls;
        }
        public object InsertExportServiceDetail(ExportServiceDetail objServiceDetail)
        {

            var status = objDal.InsertExportServiceDetail(objServiceDetail);
            return status;
        }

        public object UpdateExportServiceDetail(ExportServiceDetail objServiceDetail)
        {
            var status = objDal.UpdateExportServiceDetail(objServiceDetail);
            return status;
        }

        public object DeleteExportServiceDetail(int id)
        {
            var status = objDal.DeleteExportServiceDetail(id);
            return status;
        }

        public object InsertExportBill(ExportBill objBill)
        {

            var status = objDal.InsertExportBill(objBill);
            return status;
        }

        public object UpdateExportBill(ExportBill objBill)
        {
            var status = objDal.UpdateExportBill(objBill);
            return status;
        }

        public object DeleteExportBill(int id)
        {
            var status = objDal.DeleteExportBill(id);
            return status;
        }

        public DataTable GetMLOWiseServiceCharg(int MLOId, int serviceId, DateTime fromDate, DateTime toDate)
        {
            DataTable result = objDal.GetMLOWiseServiceCharg(MLOId, serviceId, fromDate, toDate);
            return result;
        }

        public DataTable GetExportStuffingDetails(int billType, int MloFfId, string EFTNO,DateTime fromDate, DateTime toDate)
        {
            DataTable result = objDal.GetExportStuffingDetails(billType, MloFfId, EFTNO, fromDate, toDate);
            return result;
        }

        public DataTable Export_MLO_FFarwarder_SummeryGrid(int MLOID, int FFID, DateTime fromDate, DateTime toDate)
        {
            DataTable result = objDal.Export_MLO_FFarwarder_SummeryGrid(MLOID, FFID, fromDate, toDate);
            return result;
        }
        public DataTable Export_MLO_FFarwarder_SummeryGridSum(int MLOID, int FFID, DateTime fromDate, DateTime toDate)
        {
            DataTable result = objDal.Export_MLO_FFarwarder_SummeryGridSum(MLOID, FFID, fromDate, toDate);
            return result;
        }
        
        public List<EXPORT_EFRWiseBillDetails_Result> GetEFRWiseBillDetails(string EFRNo, DateTime fromDate, DateTime toDate)
        {
            var objlist = objDal.GetEFRWiseBillDetails(EFRNo, fromDate, toDate);
            return objlist;
        }

        #endregion


        // public ChartOfService GetServiceDetailsById(int serviceId)
        // {

        //     var status = objDal.GetServiceDetailsById(serviceId);
        //     return status;

        // }

        //public List<ChartOfServiceCategory> GetAllServiceCategory()
        //{
        //    List<ChartOfServiceCategory> objlist = new List<ChartOfServiceCategory>();          
        //    objlist = objDal.GetAllServiceCategory();
        //    return objlist;
        //}

        // public object Insert(ChartOfService objService)
        //{

        //    var status = objDal.Insert(objService);
        //    return status;
        //}

        // public object Update(ChartOfService objService)
        //{

        //    var status = objDal.Update(objService);
        //    return status;
        //}

        //public object Delete(int serviceId)
        //{

        //     var status = objDal.Delete(serviceId);
        //     return status;

        // }

    }
}
