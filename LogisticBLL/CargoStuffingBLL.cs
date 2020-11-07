using System;
using System.Collections.Generic;
using LOGISTIC.Export.DAL;

namespace LOGISTIC.Export.BLL
{
   
    public class CargoStuffingBLL
    {
        CargoStuffingDAL objDal = new CargoStuffingDAL();
        //Get CSD Container Ref No, Size and Type for stuffing entry
        public object GetCSDContInformation(int CSDGateEntryId)
        {
            object result = objDal.GetCSDContInformation(CSDGateEntryId);
            return result;
        }

        public List<CargoRecieving> GetClientwiseAllCargoReceive(int clientId)
        {
            List<CargoRecieving> objlist = new List<CargoRecieving>();
            objlist = objDal.GetClientwiseAllCargoReceive(clientId);
            return objlist;
        }


        //Get Client wise all CSD GateIn container

        public List<CSDContGateInOut> GetClientwiseCSDContainer(int clientId)
        {
            List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();
            objlist = objDal.GetClientwiseCSDContainer(clientId);
            return objlist;
        }

        public CargoRecieving GetCargoRecevingByRefNumber(string refNo)
        {
            CargoRecieving objCR = new CargoRecieving();
            objCR = objDal.GetCargoRecevingByRefNumber(refNo);
            return objCR;
        }

        public CargoRecieving GetCargoRecevingByCargoReceiveId(int CargoReceiveId)
        {
            CargoRecieving objCR = new CargoRecieving();
            objCR = objDal.GetCargoRecevingByCargoReceiveId(CargoReceiveId);
            return objCR;
        }


        public StuffingDetail GetStuffingDetailsByCargoDetailsId(long cargoDetailsId)
        {
            StuffingDetail objSD = new StuffingDetail();
            objSD = objDal.GetStuffingDetailsByCargoDetailsId(cargoDetailsId);
            return objSD;
        }

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


        public object Insert(StuffingDetail objStuffing)
        {
            var status = objDal.Insert(objStuffing);
            return status;
        }


        public object Update(StuffingDetail objStuffing)
        {

            var status = objDal.Update(objStuffing);
            return status;
        }

        public bool CheckDuplicateBookingNo(string bookingNo)
        {

            var status = objDal.CheckDuplicateBookingNo(bookingNo);
            return status;
        }


        //public object Delete(StuffingDetail objStuffing)
        //{
        //    var status = objDal.Delete(objStuffing);
        //    return status;
        //}
    }
}
