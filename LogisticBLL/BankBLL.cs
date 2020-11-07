using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class BankBLL
    {
        BankDAL objDal = new BankDAL();
        public List<Bank> Getall()
        {
            List<Bank> objlist = new List<Bank>();
            objlist = objDal.Getall();
            return objlist;
        }
        public object Insert(Bank objBank)
        {

            var status = objDal.Insert(objBank);
            return status;

        }
        public object Update(Bank objBank)
        {

            var status = objDal.Update(objBank);
            return status;
        }
        public object Delete(Bank objBank)
        {

            var status = objDal.Delete(objBank);
            return status;
        }
    }
}
