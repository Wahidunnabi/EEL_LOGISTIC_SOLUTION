using System;
using System.Collections.Generic;
using LOGISTIC.CSD.DAL;
using LOGISTIC.UserDefinedModel;
using System.Linq;
using System.Data;

namespace LOGISTIC.CSD.BLL
{
   
    public class CSDReportBLL
    {
        CSDReportDAL objDal = new CSDReportDAL();


        public DataTable  GetDailyInwardMovementSummary(int custId, DateTime fromDate, DateTime toDate, int SizeId, int TypeId )
        {
            DataTable result = objDal.GetDailyInwardMovementSummary(custId, fromDate, toDate, SizeId, TypeId);
            return result;
        }

        public DataTable GetDailyOutwardMovementSummary(int custId, DateTime fromDate, DateTime toDate, int SizeId, int TypeId)
        {
            DataTable result = objDal.GetDailyOutwardMovementSummary(custId, fromDate, toDate, SizeId, TypeId);
            return result;
        }

        public DataTable GetDailyStockSummary(int custId)
        {
            DataTable result = objDal.GetDailyStockSummary(custId);
            return result;
        }

    }
}
