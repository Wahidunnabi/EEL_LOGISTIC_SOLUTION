using System;
using LOGISTIC.DAL;
using System.Data;
using System.Collections.Generic;

namespace LOGISTIC.BLL
{  
    public class AccounceBLL
    {
        AccounceDAL objDal = new AccounceDAL();

        #region Voucher

        public int GetCurrentVoucherMasterSlNo()
        {
            var result = objDal.GetCurrentVoucherMasterSlNo();
            return result;
        }
        public List<AccVoucherType> GetAllVoucherType()
        {
            var result = objDal.GetAllVoucherType();
            return result;
        }

        public List<VoucherMaster> GetAllVoucherMaster()
        {
            var result = objDal.GetAllVoucherMaster();
            return result;
        }
        public object InsertVoucher(VoucherMaster objVoucher)
        {           
            var status = objDal.InsertVoucher(objVoucher);
            return status;

        }

        public object UpdateVoucher(VoucherMaster objVoucher)
        {           
            var status = objDal.UpdateVoucher(objVoucher);
            return status;
        }

        public object DeleteVoucher(VoucherMaster objVoucher)
        {

            var status = objDal.DeleteVoucher(objVoucher);
            return status;

        }

        #endregion


        #region Chart of Accounte
        public DataTable GetAllChartOfAccount()
        {
            DataTable result = objDal.GetAllChartOfAccount();
            return result;
        }

        public object Insert(ChartOfAccount objCOA)
        {
            var status = objDal.Insert(objCOA);
            return status;
        }


        public object Update(ChartOfAccount objCOA)
        {

            var status = objDal.Update(objCOA);
            return status;
        }

        public object Delete(int Id)
        {
            var status = objDal.Delete(Id);
            return status;
        }

        public List<ChartOfAccount> GetAllTransactionHead()
        {

            var objList = objDal.GetAllTransactionHead();
            return objList;
        }

        #endregion




    }
}
