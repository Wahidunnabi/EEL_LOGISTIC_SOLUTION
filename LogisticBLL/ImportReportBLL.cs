using System;
using System.Data;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   
    public class ImportReportBLL
    {
        ImportReportDAL objDal = new ImportReportDAL();


        public DataSet GetMLOWiseDailyReport(int custId, DateTime fromDate, DateTime toDate)
        {
            DataSet result = objDal.GetMLOWiseDailyReport(custId, fromDate, toDate);
            return result;
        }

        public DataSet GetMLOWiseImportSummaryReport(int custId, DateTime fromDate, DateTime toDate)
        {
            DataSet result = objDal.GetMLOWiseImportSummaryReport(custId, fromDate, toDate);
            return result;
        }

        public DataTable GetContainerSummaryReport(int custId, int sortBy, DateTime fromDate, DateTime toDate, string ContainerNo, string ContainerSize)
        {

            DataTable result = objDal.GetContainerSummaryReport(custId, sortBy, fromDate, toDate, ContainerNo, ContainerSize);
            return result;
        }


    }
}
