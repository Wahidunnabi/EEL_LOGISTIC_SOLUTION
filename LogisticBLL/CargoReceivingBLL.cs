using System;
using System.Collections.Generic;
using LOGISTIC.Export.DAL;

namespace LOGISTIC.Export.BLL
{
   
    public class CargoReceivingBLL
    {
        CargoReceivingDAL objDal = new CargoReceivingDAL();

        public string GetCustSLNo(int CustId, DateTime Dat)
        {
            var obj = objDal.GetCustSLNo(CustId,Dat);
            return obj;
        }

        public List<CargoRecieving> GetCargoReceivingList(int custId)
        {
            var listjCR = objDal.GetCargoReceivingList(custId);
            return listjCR;

        }
       
        public CargoRecieving GetCRById(int id)
        {
            var objCR = objDal.GetCRById(id);
            return objCR;

        }

        //Get all CSD GateIn GateOut data

        //public List<CSDContGateInOut> GetAllCSDData()
        //{
        //    List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();          
        //    objlist = objDal.GetAllCSDData();
        //    return objlist;
        //}

        //return CSDContGateInOut objects where only Gate In is true....Gate Out not yet completed
        //public List<CSDContGateInOut> GetAllCSDGateInData()
        //{
        //    List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();          
        //    objlist = objDal.GetAllCSDGateInData();
        //    return objlist;
        //}

        //public CSDContGateInOut GetCSDByContNumber(string contNumber)
        //{
        //    var objCSD = objDal.GetCSDByContNumber(contNumber);
        //    return objCSD;

        //}


        public object Insert(CargoRecieving objCoaroRecieving)
        {          
          var status = objDal.Insert(objCoaroRecieving);
          return status;
        }

       
        public object Update(CargoRecieving objCR)
        {
          
            var status = objDal.Update(objCR);
            return status;
        }
        
        public object Delete(CargoRecieving objCargoReceiving)
        {
            var status = objDal.Delete(objCargoReceiving);
            return status;
        }
    }
}
