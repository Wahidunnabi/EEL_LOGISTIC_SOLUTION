using System.Collections.Generic;
using LOGISTIC.CSD.DAL;


namespace LOGISTIC.CSD.BLL
{
   
    public class MoneyReceiptBLL
    {
        MoneyReceiptDAL objDal = new MoneyReceiptDAL();

        public MoneyReceipt GetMoneyReceiptById(int id)
        {
            var objMR = objDal.GetMoneyReceiptById(id);
            return objMR;
        }
        public List<Service> GetAllServices()
        {
            var listService = objDal.GetAllServices();
            return listService;
        }

        public int GetMoneyReceiptSLNo()
        {
            var result = objDal.GetMoneyReceiptSLNo();
            return result;
        }

        public object GetMLOAgentData(int custId)
        {
            var result = objDal.GetMLOAgentData(custId);
            return result;
        }

        public object Insert(MoneyReceipt objMR)
        {
            var result = objDal.Insert(objMR);
            return result;
        }

        public object Update(MoneyReceipt objMR)
        {
            var result = objDal.Update(objMR);
            return result;
        }

        public object Delete(int MRId)
        {
            var status = objDal.Delete(MRId);
            return status;
        }

    }
}
