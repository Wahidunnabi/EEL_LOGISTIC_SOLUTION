using System;
using System.Data;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   
    public class ExportReportBLL
    {
        ExportReportDAL objDal = new ExportReportDAL();


        //public DataSet GetMLOWiseDailyReport(int custId, DateTime fromDate, DateTime toDate)
        //{
        //    DataSet result = objDal.GetMLOWiseDailyReport(custId, fromDate, toDate);
        //    return result;
        //}
        public DataTable GetDailyStuffingDetailsConsinee(int clientId, int flg, DateTime fromDate, DateTime toDate, string ContSize, string ContType, string containerNo)
        {

           
            DataTable result = objDal.GetDailyStuffingDetailsConsinee(clientId, flg, fromDate, toDate, ContSize, ContType, containerNo);
            return result;
        }

        


        public DataTable GetConsigneeWiseDailyReceiving(int consigneeId, DateTime fromDate, DateTime toDate, string ContSize,string containerNo)
        {
            DataTable result = objDal.GetConsigneeWiseDailyReceiving(consigneeId, fromDate, toDate, ContSize, containerNo);
            return result;
        }


        public DataTable GetConsigneeWiseDailyStuffing(int consigneeId, int ShipperId, int frieghtForwarderId, string EFRNO, DateTime fromDate, DateTime toDate)
        {
            DataTable result = objDal.GetConsigneeWiseDailyStuffing(consigneeId, ShipperId, frieghtForwarderId, EFRNO, fromDate, toDate);
            return result;
        }
        public DataSet GetConsigneeWiseDailyStatus(int consigneeId, DateTime fromDate, DateTime toDate)
        {
            DataSet result = objDal.GetConsigneeWiseDailyStatus(consigneeId, fromDate, toDate);
            return result;
        }

        public DataTable GetContainerHistory(long CSDId)
        {
            DataTable result = objDal.GetContainerHistory(CSDId);
            return result;
        }
    }
}
