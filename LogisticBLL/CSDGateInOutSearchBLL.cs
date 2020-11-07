using System;
using System.Collections.Generic;
using LOGISTIC.CSD.DAL;
using LOGISTIC.UserDefinedModel;
using System.Linq;
using System.Data;

namespace LOGISTIC.CSD.BLL
{
   
    public class CSDGateInOutSearchBLL
    {
        CSDGateInOutSearchDAL objDal = new CSDGateInOutSearchDAL();


        public DataTable  GetFilteredCSDGateInOut(int custId, DateTime fromDate, DateTime toDate, int SortBy, string contNumber, int size, int type, int comeFrom, int outTo, int trailerIn, int trailerOut)
        {
            DataTable result = objDal.GetFilteredCSDGateInOut(custId, fromDate, toDate, SortBy, contNumber, size, type, comeFrom, outTo, trailerIn, trailerOut);
            return result;
        }

     
    }
}
