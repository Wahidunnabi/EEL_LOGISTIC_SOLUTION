using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;
using System.Data;

namespace LOGISTIC.BLL
{
    public class TRBLL
    {
        TRDAL objDal = new TRDAL();
        public List<TRReportData> Getall()
        {
            List<TRReportData> objlist = new List<TRReportData>();
            objlist = objDal.Getall();
            return objlist;
        }

        public string GetMLOWiseTRNumber(int custId)
        {

            var TRnumber = objDal.GetMLOWiseTRNumber(custId);
            return TRnumber;
        }
        public object Insert(TRReportData objTR)
        {

            var status = objDal.Insert(objTR);
            return status;

        }
        public object Update(TRReportData objTR)
        {

            var status = objDal.Update(objTR);
            return status;
        }

        public DataTable GetTRData(int TRId)
        {
            DataTable result = objDal.GetTRData(TRId);
            return result;
        }

        public object Delete(TRReportData objTR)
        {

            var status = objDal.Delete(objTR);
            return status;
        }

    }
}
